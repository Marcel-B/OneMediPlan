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
    [Register ("AboutViewController")]
    partial class AppSettingsViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonSave { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LabelStandardzeit { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIDatePicker PickerDefaultTime { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel VersionLabel { get; set; }

        [Action ("ButtonSave_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ButtonSave_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (ButtonSave != null) {
                ButtonSave.Dispose ();
                ButtonSave = null;
            }

            if (LabelStandardzeit != null) {
                LabelStandardzeit.Dispose ();
                LabelStandardzeit = null;
            }

            if (PickerDefaultTime != null) {
                PickerDefaultTime.Dispose ();
                PickerDefaultTime = null;
            }

            if (VersionLabel != null) {
                VersionLabel.Dispose ();
                VersionLabel = null;
            }
        }
    }
}