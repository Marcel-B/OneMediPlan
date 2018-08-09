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

namespace OneMediPlan.iOS
{
    [Register ("SetStartViewController")]
    partial class SetStartViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonNext { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIDatePicker PickerStartTime { get; set; }

        [Action ("ButtonNext_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ButtonNext_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (ButtonNext != null) {
                ButtonNext.Dispose ();
                ButtonNext = null;
            }

            if (PickerStartTime != null) {
                PickerStartTime.Dispose ();
                PickerStartTime = null;
            }
        }
    }
}