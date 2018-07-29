using Foundation;
using System;
using UIKit;

namespace OneMediPlan.iOS
{
    public partial class SetStartViewController : UIViewController
    {
        public SetStartViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            PickerStartTime.MinimumDate = NSDate.Now;
        }

    }
}