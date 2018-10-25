using System;
using Android.OS;
using Android.Views;
using Android.Widget;
using Ninject;
using System.Linq;
using Android.Media;
using com.b_velop.OneMediPlan.ViewModels;
using com.b_velop.OneMediPlan.Models;

namespace OneMediPlan.Droid
{
    public class AboutFragment : Android.Support.V4.App.Fragment, IFragmentVisible
    {
        void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is AppSettingsViewModel viewModel)
            {
                if (e.PropertyName.Equals("CurrentSettings"))
                {
                    timePicker.Hour = viewModel.CurrentSettings.Hour;
                    timePicker.Minute = viewModel.CurrentSettings.Minute;
                }
            }
        }


        public static AboutFragment NewInstance() =>
            new AboutFragment { Arguments = new Bundle() };

        public AppSettingsViewModel ViewModel { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            //base.OnCreate(savedInstanceState);
            //ViewModel = App.Container.Get<AppSettingsViewModel>();
        }

        MediSettings Setting;
        Button saveSettingsButton;
        TimePicker timePicker;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_about, container, false);
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            saveSettingsButton = view.FindViewById<Button>(Resource.Id.button_save_settings);
            timePicker = view.FindViewById<TimePicker>(Resource.Id.timePickerStdTime);
            return view;
        }

        public override void OnStart()
        {
            base.OnStart();
            saveSettingsButton.Click += SaveSettingsButtonn_Click;
        }

        public override void OnStop()
        {
            base.OnStop();
            saveSettingsButton.Click -= SaveSettingsButtonn_Click;
        }

        public void BecameVisible()
        {

        }

        void SaveSettingsButtonn_Click(object sender, System.EventArgs e)
        {
            var now = DateTime.Now;
            var dt = new DateTime(now.Year, now.Month, now.Day, timePicker.Hour, timePicker.Minute, 0);
            ViewModel.SaveSettingsCommand.Execute(dt);
        }
    }
}
