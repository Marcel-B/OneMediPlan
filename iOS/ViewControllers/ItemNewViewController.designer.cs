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
    [Register ("ItemNewViewController")]
    partial class ItemNewViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonNext { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TextFieldName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ButtonNext != null) {
                ButtonNext.Dispose ();
                ButtonNext = null;
            }

            if (TextFieldName != null) {
                TextFieldName.Dispose ();
                TextFieldName = null;
            }
        }
    }
}