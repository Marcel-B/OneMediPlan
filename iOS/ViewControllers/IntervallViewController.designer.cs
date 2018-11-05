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
    [Register ("SetIntervallViewController")]
    partial class IntervallViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonNext { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.NSLayoutConstraint DependsSpace { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LabelAfter { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LabelEvery { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LabelTake { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LabelUnits { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIPickerView PickerIntervallType { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIPickerView PickerViewMedis { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIStackView StackView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TextFieldDosage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TextFieldIntervall { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ButtonNext != null) {
                ButtonNext.Dispose ();
                ButtonNext = null;
            }

            if (DependsSpace != null) {
                DependsSpace.Dispose ();
                DependsSpace = null;
            }

            if (LabelAfter != null) {
                LabelAfter.Dispose ();
                LabelAfter = null;
            }

            if (LabelEvery != null) {
                LabelEvery.Dispose ();
                LabelEvery = null;
            }

            if (LabelTake != null) {
                LabelTake.Dispose ();
                LabelTake = null;
            }

            if (LabelUnits != null) {
                LabelUnits.Dispose ();
                LabelUnits = null;
            }

            if (PickerIntervallType != null) {
                PickerIntervallType.Dispose ();
                PickerIntervallType = null;
            }

            if (PickerViewMedis != null) {
                PickerViewMedis.Dispose ();
                PickerViewMedis = null;
            }

            if (StackView != null) {
                StackView.Dispose ();
                StackView = null;
            }

            if (TextFieldDosage != null) {
                TextFieldDosage.Dispose ();
                TextFieldDosage = null;
            }

            if (TextFieldIntervall != null) {
                TextFieldIntervall.Dispose ();
                TextFieldIntervall = null;
            }
        }
    }
}