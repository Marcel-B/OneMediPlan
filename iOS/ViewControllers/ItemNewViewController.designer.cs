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
    [Register ("ItemNewViewController")]
    partial class NewMediViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonNext { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TextFieldMinimumStock { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TextFieldName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TextFieldStock { get; set; }

        [Action ("ButtonNext_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ButtonNext_TouchUpInside (UIKit.UIButton sender);

        [Action ("TextFieldEditingBegin:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void TextFieldEditingBegin (UIKit.UITextField sender);

        [Action ("TextFieldEditingChanged:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void TextFieldEditingChanged (UIKit.UITextField sender);

        void ReleaseDesignerOutlets ()
        {
            if (ButtonNext != null) {
                ButtonNext.Dispose ();
                ButtonNext = null;
            }

            if (TextFieldMinimumStock != null) {
                TextFieldMinimumStock.Dispose ();
                TextFieldMinimumStock = null;
            }

            if (TextFieldName != null) {
                TextFieldName.Dispose ();
                TextFieldName = null;
            }

            if (TextFieldStock != null) {
                TextFieldStock.Dispose ();
                TextFieldStock = null;
            }
        }
    }
}