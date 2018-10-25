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
    [Register ("SetDependencyViewController")]
    partial class DependencyViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIPickerView PickerDependency { get; set; }

        [Action ("UIButton35489_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton35489_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (PickerDependency != null) {
                PickerDependency.Dispose ();
                PickerDependency = null;
            }
        }
    }
}