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
    [Register ("SetIntervallTypeViewController")]
    partial class IntervallTypeViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton Depends { get; set; }

        [Action ("SetIntervallTouched:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SetIntervallTouched (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (Depends != null) {
                Depends.Dispose ();
                Depends = null;
            }
        }
    }
}