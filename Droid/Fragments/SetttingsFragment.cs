using System;
using Android.OS;
using Android.Views;
using Android.Widget;

using com.b_velop.OneMediPlan.Meta;
using com.b_velop.OneMediPlan.Services;
using com.b_velop.OneMediPlan.ViewModels;
using Ninject;
using OneMediPlan.Droid;

namespace com.b_velop.OneMediPlan.Droid
{
    public class SettingsFragment : Android.Support.V4.App.Fragment, IFragmentVisible
    {
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

        public static SettingsFragment NewInstance() =>
            new SettingsFragment { Arguments = new Bundle() };

        public AppSettingsViewModel ViewModel { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ViewModel = App.Container.Get<AppSettingsViewModel>();
        }

        public Button SaveSettings { get; set; }
        public TimePicker Time { get; set; }

        private void GetViews(View view)
        {
            SaveSettings = view.FindViewById<Button>(Resource.Id.buttonSaveSettings);
            Time = view.FindViewById<TimePicker>(Resource.Id.timePickerStdTime);
        }
        private void Localize()
        {
            SaveSettings.Text = Strings.SAVE;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.settingsLayout, container, false);
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            GetViews(view);
            Localize();
            return view;
        }

        public override void OnStart()
        {
            base.OnStart();
            SaveSettings.Click += SaveSettingsButtonn_Click;
        }

        public override void OnStop()
        {
            base.OnStop();
            SaveSettings.Click -= SaveSettingsButtonn_Click;
        }

        public void BecameVisible(){}

        void SaveSettingsButtonn_Click(object sender, System.EventArgs e)
        {
            ViewModel.Hour = Time.Hour;
            ViewModel.Minute = Time.Hour;
            ViewModel.SaveSettingsCommand.Execute(null);
            Toast.MakeText(Context, $"Saved Settings", ToastLength.Long).Show();
        }
    }
}
