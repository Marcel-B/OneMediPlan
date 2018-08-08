using Foundation;
using System;
using UIKit;
using OneMediPlan.Models;
using System.Collections.Generic;

namespace OneMediPlan.iOS
{
    public partial class SetStartViewController : UIViewController
    {

        public Medi CurrentMedi { get; set; }

        public SetStartViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            if (CurrentMedi.DependsOn != Guid.Empty)
            {
                PickerStartTime.Enabled = false;
                PerformSegue("ToSaveMediViewController", this);
            }
            else
            {
                PickerStartTime.Enabled = true;
            }
            PickerStartTime.MinimumDate = NSDate.Now;
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.DestinationViewController is SaveMediViewController saveMediViewController)
            {
                if (CurrentMedi.DependsOn != Guid.Empty)
                {
                    CurrentMedi.NextDate = DateTimeOffset.MinValue;
                }
                else
                {
                    DateTime newDate = TimeZone.CurrentTimeZone.ToLocalTime(
                        new DateTime(2001, 1, 1, 0, 0, 0));
                    var seconds = PickerStartTime.Date.SecondsSinceReferenceDate;
                    newDate = newDate.AddSeconds(seconds);
                    var foo = new DateTimeOffset(newDate);
                    CurrentMedi.NextDate = foo;
                }
                saveMediViewController.CurrentMedi = CurrentMedi;
            }
        }

    }
}