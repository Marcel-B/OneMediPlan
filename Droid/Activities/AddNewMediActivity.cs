using System;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Widget;
using com.b_velop.OneMediPlan.Droid.Activities;
using com.b_velop.OneMediPlan.Meta;
using com.b_velop.OneMediPlan.ViewModels;
using Ninject;

namespace com.b_velop.OneMediPlan.Droid
{
    [Activity(Label = "AddNewMediActivity")]
    public class AddNewMediActivity : BaseActivity
    {
        protected override int LayoutResource => Resource.Layout.fragmentNameAndStockLayout;
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
            IntervallType = FindViewById<Spinner>(Resource.Id.spinnerIntervallType);
            Next = FindViewById<FloatingActionButton>(Resource.Id.buttonNewMediNextButton);
            Name = FindViewById<EditText>(Resource.Id.editTextNewMediName);
            Stock = FindViewById<EditText>(Resource.Id.editTextNewMediStock);
            StockMinimum = FindViewById<EditText>(Resource.Id.editTextNewMediNewMediStockMinimum);

            Next.Click += (sender, e) =>
            {
                ViewModel.SaveNameCommand.Execute(null);
                StartActivity(typeof(SetIntervallActivity));
            };
            IntervallType.ItemSelected += (sender, e) =>
            {
                ViewModel.IntervallType = e.Position;
                Next.Enabled = ViewModel.SaveNameCommand.CanExecute(null);
            };
            Name.TextChanged += (sender, e) =>
            {
                ViewModel.Name = e.Text.ToString();
                Next.Enabled = ViewModel.SaveNameCommand.CanExecute(null);
            };
            Stock.TextChanged += (sender, e) =>
            {
                ViewModel.Stock = e.Text.ToString();
                Next.Enabled = ViewModel.SaveNameCommand.CanExecute(null);
            };
            StockMinimum.TextChanged += (sender, e) =>
            {
                ViewModel.StockMinimum = e.Text.ToString();
                Next.Enabled = ViewModel.SaveNameCommand.CanExecute(null);
            };
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
        }
    }
}
