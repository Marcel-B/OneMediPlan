using Foundation;
using System;
using UIKit;
using System.Collections.Generic;

namespace OneMediPlan.iOS
{
    public partial class WeekdayViewController : UIViewController
    {
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