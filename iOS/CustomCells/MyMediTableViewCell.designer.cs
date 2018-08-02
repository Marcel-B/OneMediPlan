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
    [Register ("MyMediTableViewCell")]
    partial class MyMediTableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView Img { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LabelDosage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LabelLast { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LabelName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LabelNext { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LabelStock { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (Img != null) {
                Img.Dispose ();
                Img = null;
            }

            if (LabelDosage != null) {
                LabelDosage.Dispose ();
                LabelDosage = null;
            }

            if (LabelLast != null) {
                LabelLast.Dispose ();
                LabelLast = null;
            }

            if (LabelName != null) {
                LabelName.Dispose ();
                LabelName = null;
            }

            if (LabelNext != null) {
                LabelNext.Dispose ();
                LabelNext = null;
            }

            if (LabelStock != null) {
                LabelStock.Dispose ();
                LabelStock = null;
            }
        }
    }
}