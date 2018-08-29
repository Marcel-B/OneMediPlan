using Foundation;
using System;
using UIKit;
using System.ComponentModel;
using OneMediPlan.ViewModels;
using Ninject;
using OneMediPlan.Helpers;
using OneMediPlan.iOS.Helper;

namespace OneMediPlan.iOS
{
    public partial class SetStartViewController : UIViewController
    {
        partial void StartPickerValueChanged(UIDatePicker sender)
        {
            if (sender is UIDatePicker picker)
            {
                ViewModel.StartDate = sender.Date.NSDateToDateTime();
            }
        }

        SetStartViewModel ViewModel { get; set; }

        public SetStartViewController(IntPtr handle) : base(handle)
        {
            ViewModel = App.Container.Get<SetStartViewModel>();
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ViewModel.Init();
            ViewModel.StartDate = PickerStartTime.Date.NSDateToDateTime();
        }

        partial void ButtonNext_TouchUpInside(UIButton sender)
        {
            ViewModel.NextCommand.Execute(null);
        }

        void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is SetStartViewModel viewModel)
            {
                if (e.PropertyName.Equals(Strings.CURRENT_MEDI))
                {
                    if (viewModel.CurrentMedi.DependsOn != Guid.Empty ||
                        viewModel.CurrentMedi.DailyAppointments != null)
                    {
                        PickerStartTime.Enabled = false;
                        PerformSegue("ToSaveMediViewController", this);
                    }
                    else
                    {

                        // Vom Medi ausfüllen lassen
                        PickerStartTime.MinimumDate = NSDate.Now;

                    }
                }
            }
        }

        //public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        //{
        //    if (segue.DestinationViewController is SaveMediViewController saveMediViewController)
        //    {
        //        if (CurrentMedi.DependsOn != Guid.Empty)
        //        {
        //            CurrentMedi.NextDate = DateTimeOffset.MinValue;
        //        }
        //        else
        //        {
        //            DateTime newDate = TimeZone.CurrentTimeZone.ToLocalTime(
        //                new DateTime(2001, 1, 1, 0, 0, 0));
        //            var seconds = PickerStartTime.Date.SecondsSinceReferenceDate;
        //            newDate = newDate.AddSeconds(seconds);
        //            var foo = new DateTimeOffset(newDate);
        //            CurrentMedi.NextDate = foo;
        //        }
        //        saveMediViewController.CurrentMedi = CurrentMedi;
        //    }
        //}

    }
}