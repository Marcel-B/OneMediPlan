using System;
using UIKit;
using OneMediPlan.Models;
using Foundation;
using System.Linq;

namespace OneMediPlan.iOS
{
    public partial class WeekdayViewController : UIViewController
    {
        public Medi CurrentMedi { get; set; }

        bool[] days = new[]{
            false,
            false,
            false,
            false,
            false,
            false,
            false
        };

        partial void WeekdayValueChanged(UISwitch sender)
        {
            if (sender == null) return;
            var i = sender.Tag - 1;
            days[i] = sender.On;
        }

        public override bool ShouldPerformSegue(string segueIdentifier, NSObject sender)
            => days.Contains(true);

        public WeekdayViewController(IntPtr handle) : base(handle) { }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.DestinationViewController is SetDosageViewController dosageViewController)
            {
                var weekdays = new Weekdays
                {
                    Id = Guid.NewGuid(),
                    MediFk = CurrentMedi.Id,
                    Days = days
                };
                dosageViewController.CurrentMedi = CurrentMedi;
            }
        }
    }
}