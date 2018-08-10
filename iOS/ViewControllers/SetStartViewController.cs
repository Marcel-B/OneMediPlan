using Foundation;
using System;
using UIKit;
using OneMediPlan.Models;
using System.Collections.Generic;
using System.ComponentModel;
using OneMediPlan.ViewModels;
using Ninject;

namespace OneMediPlan.iOS
{
    public partial class SetStartViewController : UIViewController
    {
        SetStartViewModel ViewModel { get; set; }

        public SetStartViewController(IntPtr handle) : base(handle)
        {
            ViewModel = App.Container.Get<SetStartViewModel>();
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();
            await ViewModel.Init();
        }

        partial void ButtonNext_TouchUpInside(UIButton sender)
        {
            ViewModel.NextCommand.Execute(null);
        }

        void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is SetStartViewModel viewModel)
            {
                if (e.PropertyName.Equals("CurrentMedi"))
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