// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace OneMediPlan.iOS.CustomCells
{
    [Register ("MeditableViewCell")]
    partial class MeditableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LabelName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LabelShortInfo { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (LabelName != null) {
                LabelName.Dispose ();
                LabelName = null;
            }

            if (LabelShortInfo != null) {
                LabelShortInfo.Dispose ();
                LabelShortInfo = null;
            }
        }
    }
}