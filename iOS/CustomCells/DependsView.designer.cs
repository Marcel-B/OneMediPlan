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
    [Register ("DependsView")]
    partial class DependsView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LabelAfter { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LabelTake { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LabelUnits { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIPickerView PickerViewMedis { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TextFieldDosage { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (LabelAfter != null) {
                LabelAfter.Dispose ();
                LabelAfter = null;
            }

            if (LabelTake != null) {
                LabelTake.Dispose ();
                LabelTake = null;
            }

            if (LabelUnits != null) {
                LabelUnits.Dispose ();
                LabelUnits = null;
            }

            if (PickerViewMedis != null) {
                PickerViewMedis.Dispose ();
                PickerViewMedis = null;
            }

            if (TextFieldDosage != null) {
                TextFieldDosage.Dispose ();
                TextFieldDosage = null;
            }
        }
    }
}