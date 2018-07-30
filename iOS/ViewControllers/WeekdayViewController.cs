using System;
using UIKit;
using OneMediPlan.Models;

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

        public WeekdayViewController(IntPtr handle) : base(handle)
        {
        }
    }
}