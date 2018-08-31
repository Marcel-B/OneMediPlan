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
    [Register ("SetDosageViewController")]
    partial class DosageViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonNext { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField LabelDosage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.NSLayoutConstraint TextFieldDosage { get; set; }

        [Action ("ButtonNext_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ButtonNext_TouchUpInside (UIKit.UIButton sender);

        [Action ("TextFieldDosageChanged:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void TextFieldDosageChanged (UIKit.UITextField sender);

        void ReleaseDesignerOutlets ()
        {
            if (ButtonNext != null) {
                ButtonNext.Dispose ();
                ButtonNext = null;
            }

            if (LabelDosage != null) {
                LabelDosage.Dispose ();
                LabelDosage = null;
            }

            if (TextFieldDosage != null) {
                TextFieldDosage.Dispose ();
                TextFieldDosage = null;
            }
        }
    }
}