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
    [Register ("MediStockViewController")]
    partial class MediStockViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField LabelCurrentStock { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField LabelMinimumStock { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (LabelCurrentStock != null) {
                LabelCurrentStock.Dispose ();
                LabelCurrentStock = null;
            }

            if (LabelMinimumStock != null) {
                LabelMinimumStock.Dispose ();
                LabelMinimumStock = null;
            }
        }
    }
}