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
    [Register ("SetDailyViewController")]
    partial class SetDailyViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIDatePicker PickerTime { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView TableViewDates { get; set; }

        [Action ("UIButton44379_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton44379_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (PickerTime != null) {
                PickerTime.Dispose ();
                PickerTime = null;
            }

            if (TableViewDates != null) {
                TableViewDates.Dispose ();
                TableViewDates = null;
            }
        }
    }
}