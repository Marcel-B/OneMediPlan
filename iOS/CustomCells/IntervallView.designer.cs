// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using UIKit;

namespace com.b_velop.OneMediPlan.iOS
{
    [Register ("IntervallView")]
    partial class IntervallView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        com.b_velop.OneMediPlan.iOS.IntervallView Intervall { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TetxtFieldIntervall { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (Intervall != null) {
                Intervall.Dispose ();
                Intervall = null;
            }

            if (TetxtFieldIntervall != null) {
                TetxtFieldIntervall.Dispose ();
                TetxtFieldIntervall = null;
            }
        }
    }
}