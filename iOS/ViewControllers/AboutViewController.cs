using System;
using Ninject;
using UIKit;
using OneMediPlan.Services;
using System.Linq;
using OneMediPlan.iOS.Helper;

namespace OneMediPlan.iOS
{
    public partial class AboutViewController : UIViewController
    {
        partial void ButtonSave_TouchUpInside(UIButton sender)
        {
            var d = PickerDefaultTime.Date.NSDateToDateTime();
            ViewModel.SaveSettingsCommand.Execute(d);
        }

        public AppSettingsViewModel ViewModel { get; set; }

        public AboutViewController(IntPtr handle) : base(handle)
        {
            ViewModel = new AppSettingsViewModel();
            ViewModel.PropertyChanged += (object sender, System.ComponentModel.PropertyChangedEventArgs e) =>
            {
                if (sender is AppSettingsViewModel vm)
                {
                    if (!base.IsViewLoaded) return;
                    var timeSettings = ViewModel.CurrentSettings;
                    var now = DateTimeOffset.Now;
                    var dt = new DateTime(now.Year, now.Month, now.Day, timeSettings.Hour, timeSettings.Minute, 0);
                    PickerDefaultTime.SetDate(dt.DateTimeToNSDate(), true);
                }

            };
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = ViewModel.Title;
        }
    }
}
