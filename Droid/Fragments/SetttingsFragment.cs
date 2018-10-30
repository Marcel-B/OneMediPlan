using System;
using Android.OS;
using Android.Views;
using Android.Widget;

using com.b_velop.OneMediPlan.Meta;
using com.b_velop.OneMediPlan.Services;
using com.b_velop.OneMediPlan.ViewModels;
using Ninject;
using OneMediPlan.Droid;
using com.b_velop.OneMediPlan.Meta.Interfaces;

namespace com.b_velop.OneMediPlan.Droid
{
    public class SettingsFragment : Android.Support.V4.App.Fragment, IFragmentVisible
    {
        public static SettingsFragment NewInstance()
           => new SettingsFragment { Arguments = new Bundle() };

        protected SettingsFragment()
        {
            _logger = App.Container.Get<ILogger>();
            ViewModel = App.Container.Get<AppSettingsViewModel>();
        }

        private readonly ILogger _logger;
        public AppSettingsViewModel ViewModel { get; set; }
        public Button SaveSettings { get; set; }
        public TimePicker Time { get; set; }
        public TextView StandardTime { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            => inflater.Inflate(Resource.Layout.settingsLayout, container, false);

        public override void OnStart()
        {
            base.OnStart();
            GetViews();
            Localize();
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            SaveSettings.Click += SaveSettingsButtonn_Click;
            Time.TimeChanged += Time_TimeChanged;
        }

        public override void OnStop()
        {
            base.OnStop();
            ViewModel.PropertyChanged -= ViewModel_PropertyChanged;
            SaveSettings.Click -= SaveSettingsButtonn_Click;
            Time.TimeChanged -= Time_TimeChanged;
        }

        public void BecameVisible() 
        {
        }

        private void GetViews()
        {
            SaveSettings = View.FindViewById<Button>(Resource.Id.buttonSaveSettings);
            Time = View.FindViewById<TimePicker>(Resource.Id.timePickerStdTime);
            StandardTime = View.FindViewById<TextView>(Resource.Id.textViewStandardTime);
        }

        private void Localize()
        {
            StandardTime.Text = Strings.STANDARD_TIME;
            SaveSettings.Text = Strings.SAVE;
        }

        void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is AppSettingsViewModel viewModel)
            {
                if (e.PropertyName.Equals("CurrentSettings"))
                {
                    Time.Hour = viewModel.CurrentSettings.Hour;
                    Time.Minute = viewModel.CurrentSettings.Minute;
                }
            }
        }

        void SaveSettingsButtonn_Click(object sender, System.EventArgs e)
        {
            ViewModel.SaveSettingsCommand.Execute(null);
            Toast.MakeText(Context, $"Saved Settings", ToastLength.Long).Show();
        }

        void Time_TimeChanged(object sender, TimePicker.TimeChangedEventArgs e)
        {
            ViewModel.Hour = e.HourOfDay;
            ViewModel.Minute = e.Minute;
        }
    }
}
