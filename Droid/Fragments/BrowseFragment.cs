using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

using com.b_velop.OneMediPlan.ViewModels;
using Ninject;
using OneMediPlan.Droid;
using com.b_velop.OneMediPlan.Helpers;
using com.b_velop.OneMediPlan.Services;
using com.b_velop.OneMediPlan.Meta;
using System.Threading.Tasks;
using com.b_velop.OneMediPlan.Domain;

namespace com.b_velop.OneMediPlan.Droid
{
    public class BrowseFragment : Android.Support.V4.App.Fragment, IFragmentVisible
    {
        public static BrowseFragment NewInstance() =>
            new BrowseFragment { Arguments = new Bundle() };

        public BrowseItemsAdapter Adapter {get;set;}
        public SwipeRefreshLayout Refresher { get; set; }
        public ProgressBar Progress { get; set; }

        public static MainViewModel ViewModel { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            ViewModel = App.Container.Get<MainViewModel>();
            var view = inflater.Inflate(Resource.Layout.fragment_browse, container, false);
            var recyclerView =
                view.FindViewById<RecyclerView>(Resource.Id.recyclerView);

            recyclerView.HasFixedSize = true;
            recyclerView.SetAdapter(Adapter = new BrowseItemsAdapter(Activity, ViewModel));

            Refresher = view.FindViewById<SwipeRefreshLayout>(Resource.Id.refresher);
            Refresher.SetColorSchemeColors(Resource.Color.accent);

            Progress = view.FindViewById<ProgressBar>(Resource.Id.progressbar_loading);
            Progress.Visibility = ViewStates.Gone;

            return view;
        }

        public override void OnStart()
        {
            base.OnStart();

            Refresher.Refresh += Refresher_Refresh;
            Adapter.ItemClick += Adapter_ItemClick;

            if (ViewModel.Medis.Count == 0)
                ViewModel.LoadItemsCommand.Execute(null);
        }

        public override void OnStop()
        {
            base.OnStop();
            Refresher.Refresh -= Refresher_Refresh;
            Adapter.ItemClick -= Adapter_ItemClick;
        }

        private async void  Adapter_ItemClick(object sender, RecyclerClickEventArgs e)
        {
            var medi = ViewModel.Medis[e.Position];
            AppStore.Instance.CurrentMedi = medi;

            var waring = Strings.WARNING;
            var cancel = Strings.CANCEL;
            //var noLeft = NSBundle.MainBundle.GetLocalizedString(Strings.NO_JOKER_LEFT);
            //var notEnough = NSBundle.MainBundle.GetLocalizedString(Strings.NOT_ENOUGH_JOKER_LEFT);
            //var takeLast = NSBundle.MainBundle.GetLocalizedString(Strings.TAKE_LAST_JOKER_UNITS);

            if (medi.Stock <= 0)
            {
                //Create Alert
                //var okAlertController = UIAlertController.Create(waring, string.Format(noLeft, medi.Name), UIAlertControllerStyle.Alert);

                //Add Action
                //okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                // Present Alert
                //ParentController.PresentViewController(okAlertController, true, null);
                Toast.MakeText(Context, $"No more {medi.Name} left.", ToastLength.Long);
                return;
            }
            else if (medi.Stock - medi.Dosage < 0)
            {
                //Create Alert
                //var okCancelAlertController = UIAlertController.Create(waring, string.Format(notEnough, medi.Name), UIAlertControllerStyle.Alert);

                //Add Actions
                //okCancelAlertController.AddAction(UIAlertAction.Create(string.Format(takeLast, medi.Name), UIAlertActionStyle.Default, async alert =>
                //{
                //    await UpdateList(medi);
                //}));

                //okCancelAlertController.AddAction(UIAlertAction.Create(cancel, UIAlertActionStyle.Cancel, null));

                // Present Alert
                //ParentController.PresentViewController(okCancelAlertController, true, null);
                Toast.MakeText(Context, $"No more {medi.Name} left.", ToastLength.Long);
                return;
            }
            await UpdateList(medi);

            //var intent = new Intent(Activity, typeof(MediDetailActivity));

            //intent.PutExtra("data", Newtonsoft.Json.JsonConvert.SerializeObject(item));
            //Activity.StartActivity(intent);
        }

        public async Task UpdateList(Medi medi)
        {
            var s = App.Container.Get<ISomeLogic>();
            await s.HandleIntoke(medi);
            ViewModel.LoadItemsCommand.Execute(this);
            return;
        }


        void Refresher_Refresh(object sender, EventArgs e)
        {
            ViewModel.LoadItemsCommand.Execute(null);
            Refresher.Refreshing = false;
        }

        public void BecameVisible()
        {

        }
    }

    public class BrowseItemsAdapter : BaseRecycleViewAdapter
    {
        public MainViewModel ViewModel { get; set; }
        Activity activity;

        public BrowseItemsAdapter(Activity activity, MainViewModel viewModel)
        {
            this.ViewModel = viewModel;
            this.activity = activity;

            this.ViewModel.Medis.CollectionChanged += (sender, args) =>
            {
                this.activity.RunOnUiThread(NotifyDataSetChanged);
            };
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.item_browse;
            itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);

            var vh = new MyViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = ViewModel.Medis[position];

            // Replace the contents of the view with that element
            var myHolder = holder as MyViewHolder;
            myHolder.Name.Text = item.Name;
			myHolder.Stock.Text = item.GetStockInfo();
            myHolder.NextDate.Text = item.GetNextDate();
            myHolder.LastDate.Text = item.GetLastDate();
            myHolder.Dosage.Text = item.GetDosage();

        }

        public override int ItemCount => ViewModel.Medis.Count;
    }

    public class MyViewHolder : RecyclerView.ViewHolder
    {
        public TextView Name { get; set; }
        public TextView Stock { get; set; }
        public TextView Dosage { get; set; }
        public TextView LastDate { get; set; }
        public TextView NextDate { get; set; }


        public MyViewHolder(View itemView,
                            Action<RecyclerClickEventArgs> clickListener,
                            Action<RecyclerClickEventArgs> longClickListener) : base(itemView)
        {
            Name = itemView.FindViewById<TextView>(Resource.Id.textViewMediName);
            Stock = itemView.FindViewById<TextView>(Resource.Id.textViewMediStock);
            Dosage = itemView.FindViewById<TextView>(Resource.Id.textViewMediDosage);
            LastDate = itemView.FindViewById<TextView>(Resource.Id.textViewMediLastDate);
            NextDate = itemView.FindViewById<TextView>(Resource.Id.textViewMediNextDate);
            itemView.Click += (sender, e) => clickListener(new RecyclerClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) =>
                longClickListener(new RecyclerClickEventArgs
                {
                    View = itemView,
                    Position = AdapterPosition
                });
        }
    }
}
