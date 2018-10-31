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
    [Register ("SetIntervallViewController")]
    partial class IntervallViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonNext { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LabelDependencyInfo { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField LabelIntervallCount { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIPickerView PickerIntervallType { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ButtonNext != null) {
                ButtonNext.Dispose ();
                ButtonNext = null;
            }

            if (LabelDependencyInfo != null) {
                LabelDependencyInfo.Dispose ();
                LabelDependencyInfo = null;
            }

            if (LabelIntervallCount != null) {
                LabelIntervallCount.Dispose ();
                LabelIntervallCount = null;
            }

            if (PickerIntervallType != null) {
                PickerIntervallType.Dispose ();
                PickerIntervallType = null;
            }
        }
    }
}