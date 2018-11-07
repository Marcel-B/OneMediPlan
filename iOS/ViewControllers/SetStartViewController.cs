using System;
using com.b_velop.OneMediPlan.Domain;
using com.b_velop.OneMediPlan.Helpers;
using com.b_velop.OneMediPlan.iOS.Helper;
using com.b_velop.OneMediPlan.Meta;
using com.b_velop.OneMediPlan.ViewModels;
using Foundation;
using Ninject;
using UIKit;

namespace com.b_velop.OneMediPlan.iOS
{
    public partial class SetStartViewController : UIViewController
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
            PickerStartTime.Date = DateTime.Now.ToNSDate();
            ViewModel.FirstApplication = PickerStartTime.Date.ToDateTime();
        }

        public override void ViewDidUnload()
        {
            base.ViewDidUnload();
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
            ViewModel.SaveNameCommand.Execute(null);
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
