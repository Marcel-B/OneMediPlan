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
    partial class SetDosageViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField LabelDosage { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (LabelDosage != null) {
                LabelDosage.Dispose ();
                LabelDosage = null;
            }
        }
    }
}