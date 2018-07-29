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
    [Register ("SetDependencyViewController")]
    partial class SetDependencyViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIPickerView PickerDependency { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (PickerDependency != null) {
                PickerDependency.Dispose ();
                PickerDependency = null;
            }
        }
    }
}