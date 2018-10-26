using System;
using System.ComponentModel;
using com.b_velop.OneMediPlan.iOS.Helper;
using com.b_velop.OneMediPlan.Meta;
using com.b_velop.OneMediPlan.ViewModels;
using Foundation;
using Ninject;
using UIKit;

namespace com.b_velop.OneMediPlan.iOS
{
    public partial class AppSettingsViewController : UIViewController
    {
        partial void ButtonSave_TouchUpInside(UIButton sender)
        {
            var d = PickerDefaultTime.Date.ToDateTime();
            ViewModel.SaveSettingsCommand.Execute(d);
        }

        public AppSettingsViewModel ViewModel { get; set; }

        public AppSettingsViewController(IntPtr handle) : base(handle)
        {
            ViewModel = App.Container.Get<AppSettingsViewModel>();
            ViewModel.PropertyChanged += (object sender, PropertyChangedEventArgs e) =>
            {
                if (sender is AppSettingsViewModel viewModel)
                {
                    if (!base.IsViewLoaded) return;
                    var timeSettings = viewModel.CurrentSettings;
                    var now = DateTimeOffset.Now;
                    var dt = new DateTime(now.Year, now.Month, now.Day, timeSettings.Hour, timeSettings.Minute, 0);
                    PickerDefaultTime.SetDate(dt.ToNSDate(), true);
                }
            };
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ViewModel.LoadSettings();
            var localizedString = NSBundle.MainBundle.GetLocalizedString(Strings.SETTINGS);
            Title = localizedString;
        }
    }
}
