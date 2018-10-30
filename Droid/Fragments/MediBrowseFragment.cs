using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using com.b_velop.OneMediPlan.Domain;
using com.b_velop.OneMediPlan.Helpers;
using com.b_velop.OneMediPlan.Meta;
using com.b_velop.OneMediPlan.Meta.Interfaces;
using com.b_velop.OneMediPlan.Services;
using com.b_velop.OneMediPlan.ViewModels;
using Ninject;
using OneMediPlan.Droid;

namespace com.b_velop.OneMediPlan.Droid
{
    public class MediBrowseFragment : Android.Support.V4.App.ListFragment, IFragmentVisible
    {
        public static MediBrowseFragment NewInstance() =>
            new MediBrowseFragment { Arguments = new Bundle() };

        protected MediBrowseFragment()
        {
            _logger = App.Container.Get<ILogger>();
            ViewModel = App.Container.Get<MainViewModel>();
        }

        private ILogger _logger;

        public BrowseItemsAdapter Adapter { get; set; }
        public static MainViewModel ViewModel { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ListAdapter = new BrowseItemsAdapter(Context, Activity, ViewModel);
        }

        public override void OnStart()
        {
            base.OnStart();
            //if (ViewModel.Medis.Count == 0)
            ViewModel.LoadItemsCommand.Execute(null);
        }

        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            base.OnCreateContextMenu(menu, v, menuInfo);
            if (v == ListView)
            {
                var info = (AdapterView.AdapterContextMenuInfo)menuInfo;
                menu.SetHeaderTitle(ViewModel.Medis[info.Position].Name);
                menu.Add(Menu.None, 0, 0, Strings.EDIT);
                menu.Add(Menu.None, 1, 1, Strings.STOCK);
                menu.Add(Menu.None, 2, 2, Strings.DELETE);
            }
            _logger.Log("Context menu method here", GetType());
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            RegisterForContextMenu(ListView);
        }
        public override bool OnContextItemSelected(IMenuItem item)
        {
            var info = (AdapterView.AdapterContextMenuInfo)item.MenuInfo;
            var menuItemIndex = item.ItemId;
            AppStore.Instance.CurrentMedi = ViewModel.Medis[info.Position];
            switch (menuItemIndex)
            {
                case 0: // Edit
                    _logger.Log($"Edit Medi '{ViewModel.Medis[info.Position]}'", GetType());
                    //Activity.StartActivity(typeof());
                    break;
                case 1: // Fill Stock
                    _logger.Log($"Edit stock from Medi'{ViewModel.Medis[info.Position]}'", GetType());
                    ShowAlertWindow();
                    break;
                case 2: // Delete selected
                    _logger.Log($"Delete Medi '{ViewModel.Medis[info.Position]}'", GetType());
                    //Task.Run(() => ViewModel.DeleteMedi(info.Position));
                    break;
            }
            Toast.MakeText(Activity, $"Selected '{menuItemIndex}'", ToastLength.Short).Show();
            return true;
        }

        public override void OnStop()
        {
            base.OnStop();
        }

        private void ShowAlertWindow()
        {
            var medi = AppStore.Instance.CurrentMedi;
            AlertDialog.Builder builder = new AlertDialog.Builder(Activity);
            builder.SetTitle("Neuen Vorrat eingeben");

            TextView label = new TextView(Activity);
            label.Text = $"Neuen Vorrat für '{medi.Name}' eingeben";
            // Set up the input
            EditText input = new EditText(Activity);
            input.Text = medi.Stock.ToString();
            // Specify the type of input expected; this, for example, sets the input as a password, and will mask the text
            input.InputType = InputTypes.ClassNumber;// | InputTypes.TextVariationPassword;
            builder.SetView(input);
            //builder.SetView(label);

            builder.SetPositiveButton("Ok", (sender, e) =>
            {
                Toast.MakeText(Context, $"Ok '{input.Text}'", ToastLength.Long).Show();
            });

            builder.SetNegativeButton(Strings.CANCEL, (sender, e) =>
            {
                Toast.MakeText(Context, "Cancel Button pressed", ToastLength.Long).Show();
            });
            builder.Show();
        }

        public async override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var medi = ViewModel.Medis[position];
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
                Toast.MakeText(Context, $"No more '{medi.Name}' left.", ToastLength.Long).Show();
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
                Toast.MakeText(Context, $"No more '{medi.Name}' left.", ToastLength.Long);
                return;
            }

            await Task.Run(async () =>
           {
               var s = App.Container.Get<ISomeLogic>();
               await s.HandleIntoke(medi);
               ViewModel.LoadItemsCommand.Execute(this);
               return;
           });
            //var intent = new Intent(Activity, typeof(MediDetailActivity));
            //intent.PutExtra("data", Newtonsoft.Json.JsonConvert.SerializeObject(item));
            //Activity.StartActivity(intent);
        }



        public void BecameVisible()
        {
        }
    }

    public class BrowseItemsAdapter : BaseAdapter// BaseRecycleViewAdapter
    {
        public MainViewModel ViewModel { get; set; }
        public Activity activity;
        public Context Context;

        public BrowseItemsAdapter(Context content, Activity activity, MainViewModel viewModel)
        {
            this.Context = content;
            this.ViewModel = viewModel;
            this.activity = activity;
            this.ViewModel.Medis.CollectionChanged += (sender, args) =>
                this.activity.RunOnUiThread(NotifyDataSetChanged);
        }

        public override Java.Lang.Object GetItem(int position)
            => position;

        public override long GetItemId(int position)
            => position;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = ViewModel.Medis[position];
            var view = convertView;
            MediViewHolder holder = null;
            if (view != null)
                holder = view.Tag as MediViewHolder;
            if (holder == null)
            {
                holder = new MediViewHolder();
                var inflater = Context.GetSystemService(Context.LayoutInflaterService)
                              .JavaCast<LayoutInflater>();
                view = inflater.Inflate(Resource.Layout.fragmentMediItemLayout, parent, false);
                holder.Name = view.FindViewById<TextView>(Resource.Id.textViewMediName);
                holder.Stock = view.FindViewById<TextView>(Resource.Id.textViewMediStock);
                holder.Dosage = view.FindViewById<TextView>(Resource.Id.textViewMediDosage);
                holder.LastDate = view.FindViewById<TextView>(Resource.Id.textViewMediLastDate);
                holder.NextDate = view.FindViewById<TextView>(Resource.Id.textViewMediNextDate);
                view.Tag = holder;
            }
            holder.Name.Text = item.Name;
            holder.Stock.Text = item.GetStockInfo();
            holder.NextDate.Text = item.GetNextDate();
            holder.LastDate.Text = item.GetLastDate();
            holder.Dosage.Text = item.GetDosage();
            return view;
        }
        public override int Count => ViewModel.Medis.Count;
    }

    public class MediViewHolder : Java.Lang.Object
    {
        public TextView Name { get; set; }
        public TextView Stock { get; set; }
        public TextView Dosage { get; set; }
        public TextView LastDate { get; set; }
        public TextView NextDate { get; set; }
    }
}
