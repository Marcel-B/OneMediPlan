using Foundation;
using System;
using UIKit;
using System.ComponentModel;
using OneMediPlan.ViewModels;
using Ninject;
using OneMediPlan.Helpers;
using OneMediPlan.iOS.Helper;
using Security;
using OneMediPlan.Models;
using GameKit;

namespace com.b_velop.OneMediPlan.iOS
{
    public partial class StartViewController : UIViewController
    {
        partial void StartPickerValueChanged(UIDatePicker sender)
        {
            if (sender is UIDatePicker picker)
            {
                ViewModel.StartDate = sender.Date.ToDateTime();
            }
        }

        StartViewModel ViewModel { get; set; }

        public StartViewController(IntPtr handle) : base(handle)
        {
            ViewModel = App.Container.Get<StartViewModel>();
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ViewModel.Init();
            ViewModel.StartDate = PickerStartTime.Date.ToDateTime();
        }

        partial void ButtonNext_TouchUpInside(UIButton sender)
        {
            ViewModel.NextCommand.Execute(null);
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

        void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is StartViewModel viewModel)
                SetProperty(e.PropertyName, viewModel);
        }
    }
}