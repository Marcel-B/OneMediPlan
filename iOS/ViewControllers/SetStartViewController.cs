using System;
using com.b_velop.OneMediPlan.Domain;
using com.b_velop.OneMediPlan.Helpers;
using com.b_velop.OneMediPlan.iOS.Helper;
using com.b_velop.OneMediPlan.iOS.ViewControllers;
using com.b_velop.OneMediPlan.Meta;
using com.b_velop.OneMediPlan.Services;
using com.b_velop.OneMediPlan.ViewModels;
using Foundation;
using Ninject;
using UIKit;

namespace com.b_velop.OneMediPlan.iOS
{
    public partial class SetStartViewController : BaseViewController
    {

        public SetStartViewController(IntPtr handle) : base(handle)
        {
            ViewModel = App.Container.Get<NewMediViewModel>();
        }

        public NewMediViewModel ViewModel { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ButtonNext.TouchUpInside += ButtonNext_TouchUpInside;
            PickerStartTime.ValueChanged += PickerStartTime_ValueChanged;

            var settings = AppStore.Instance.AppSettings;

            var now = DateTimeOffset.Now;
            var time = new DateTime(now.Year, now.Month, now.Day, settings.Hour, settings.Minute, 0);
            PickerStartTime.Date = time.ToNSDate();
            ViewModel.FirstApplication = PickerStartTime.Date.ToDateTime();
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            ButtonNext.TouchUpInside -= ButtonNext_TouchUpInside;
            PickerStartTime.ValueChanged -= PickerStartTime_ValueChanged;
        }

        void PickerStartTime_ValueChanged(object sender, EventArgs e)
        {
            if (sender is UIDatePicker picker)
            {
                ViewModel.FirstApplication = picker.Date.ToDateTime();
            }
        }

        void ButtonNext_TouchUpInside(object sender, EventArgs e)
        {
        }

        void SetProperty(string propertyName, StartViewModel viewModel)
        {
            if (propertyName.Equals(Strings.CURRENT_MEDI))
                SetCurrentMedi(viewModel.CurrentMedi);
        }

        void SetCurrentMedi(Medi medi)
        {
            if (medi.NeedsNoStartDate())
                NavigateForward();
            else
                SetStartTime(medi);
        }

        public void SetStartTime(Medi medi)
        {
            if (medi.NextDate != DateTimeOffset.MinValue)
                PickerStartTime.Date = medi.NextDate.DateTime.ToNSDate();
            else
                PickerStartTime.MinimumDate = NSDate.Now;
        }

        public void NavigateForward()
        {
            PickerStartTime.Enabled = false;
            PerformSegue("ToSaveMediViewController", this);
        }
    }
}
