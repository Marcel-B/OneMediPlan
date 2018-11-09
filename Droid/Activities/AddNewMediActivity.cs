using System;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;

using com.b_velop.OneMediPlan.Domain.Enums;
using com.b_velop.OneMediPlan.Droid.Activities;
using com.b_velop.OneMediPlan.Meta;
using com.b_velop.OneMediPlan.ViewModels;
using Ninject;

namespace com.b_velop.OneMediPlan.Droid
{
    [Activity(Label = "AddNewMediActivity")]
    public class AddNewMediActivity : BaseActivity
    {
        protected override int LayoutResource
            => Resource.Layout.activityNameAndStockLayout;

        public FloatingActionButton Next { get; set; }
        public EditText Name { get; set; }
        public EditText Stock { get; set; }
        public EditText StockMinimum { get; set; }
        public Spinner IntervallType { get; set; }
        public NewMediViewModel ViewModel { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ViewModel = App.Container.Get<NewMediViewModel>();
            ViewModel.CurrentViewType = NewMediViewModel.ViewType.NameAndStock;

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.home)
            {
                OnBackPressed(); 
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }

   

        public override void GetViews()
        {
            IntervallType = FindViewById<Spinner>(Resource.Id.spinnerIntervallType);
            Next = FindViewById<FloatingActionButton>(Resource.Id.buttonNewMediNextButton);
            Name = FindViewById<EditText>(Resource.Id.editTextNewMediName);
            Stock = FindViewById<EditText>(Resource.Id.editTextNewMediStock);
            StockMinimum = FindViewById<EditText>(Resource.Id.editTextNewMediNewMediStockMinimum);
        }

        public override void InitViews()
        {
            base.InitViews();
            ViewModel.CurrentViewType = NewMediViewModel.ViewType.NameAndStock;
            var arraySpinner = new[]{
                Strings.INTERVALL,
                Strings.WEEKDAYS,
                Strings.DEPENDS,
                Strings.IF_NEEDED,
                Strings.DAILY_APPOINTMENTS
            };
            var adapter = new ArrayAdapter<String>(this,
                    Android.Resource.Layout.SimpleSpinnerItem, arraySpinner);
            IntervallType.Adapter = adapter;

            ViewModel.Stock = Stock.Text;
            ViewModel.StockMinimum = StockMinimum.Text;
            ViewModel.Name = Name.Text;
            SetButtonState(false);
        }

        private void SetButtonState(bool isActive){
            Next.Enabled = isActive;
            if (isActive)
                Next.SetBackgroundColor(Android.Graphics.Color.AliceBlue);
            else
                Next.SetBackgroundColor(Android.Graphics.Color.Red);
        }

        public override void Localize()
        {
            Name.Hint = Strings.ENTER_NAME;
            StockMinimum.Hint = Strings.MINIMUM_STOCK_WARNING;
            Stock.Hint = Strings.STOCK;
        }

        public override void SetEvents()
        {
            Next.Click += Next_Click;
            IntervallType.ItemSelected += IntervallType_ItemSelected;
            Name.TextChanged += Name_TextChanged;
            Stock.TextChanged += Stock_TextChanged;
            StockMinimum.TextChanged += StockMinimum_TextChanged;
        }

        public override void DestroyEvents()
        {
            Next.Click -= Next_Click;
            IntervallType.ItemSelected -= IntervallType_ItemSelected;
            Name.TextChanged -= Name_TextChanged;
            Stock.TextChanged -= Stock_TextChanged;
            StockMinimum.TextChanged -= StockMinimum_TextChanged;
        }

        void Next_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SetIntervallActivity));
            Finish();
        }

        void IntervallType_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            ViewModel.IntervallType = (IntervallType)e.Position;
            SetButtonState();
        }

        void Name_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            ViewModel.Name = e.Text.ToString();
            SetButtonState();
        }

        void Stock_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            ViewModel.Stock = e.Text.ToString();
            SetButtonState();
        }

        void StockMinimum_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            ViewModel.StockMinimum = e.Text.ToString();
            SetButtonState();
        }

        private void SetButtonState()
        {
            SetButtonState(ViewModel.SaveNameCommand.CanExecute(null));
        }
    }
}
