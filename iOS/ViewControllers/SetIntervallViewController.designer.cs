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
    [Register ("SetIntervallViewController")]
    partial class SetIntervallViewController
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

        [Action ("ButtonNextTouched:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ButtonNextTouched (UIKit.UIButton sender);

        [Action ("TextFieldIntervallChanged:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void TextFieldIntervallChanged (UIKit.UITextField sender);

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