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
    [Register ("SetDailyViewController")]
    partial class DailyViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonAdd { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonNext { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIDatePicker PickerTime { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView TableViewDates { get; set; }

        [Action ("ButtonAdd_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ButtonAdd_TouchUpInside (UIKit.UIButton sender);

        [Action ("ButtonNext_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ButtonNext_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (ButtonAdd != null) {
                ButtonAdd.Dispose ();
                ButtonAdd = null;
            }

            if (ButtonNext != null) {
                ButtonNext.Dispose ();
                ButtonNext = null;
            }

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