// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace com.b_velop.OneMediPlan.iOS
{
    [Register ("WeekdayViewController")]
    partial class WeekdayViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonWeiter { get; set; }

        [Action ("ButtonWeiter_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ButtonWeiter_TouchUpInside (UIKit.UIButton sender);

        [Action ("WeekdayValueChanged:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void WeekdayValueChanged (UIKit.UISwitch sender);

        void ReleaseDesignerOutlets ()
        {
            if (ButtonWeiter != null) {
                ButtonWeiter.Dispose ();
                ButtonWeiter = null;
            }
        }
    }
}