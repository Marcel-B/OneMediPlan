using System;
using System.ComponentModel;
using UIKit;
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
            ViewModel.PropertyChanged += (object sender, PropertyChangedEventArgs e) =>
            {
                if (sender is AppSettingsViewModel viewModel)
                {
                    if (!base.IsViewLoaded) return;
                    var timeSettings = viewModel.CurrentSettings;
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
