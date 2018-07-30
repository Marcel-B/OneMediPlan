using Foundation;
using System;
using UIKit;
using OneMediPlan.Models;

namespace OneMediPlan.iOS
{
    public partial class SetDosageViewController : UIViewController
    {
        public Medi CurrentMedi { get; set; }

        public SetDosageViewController (IntPtr handle) : base (handle)
        {
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.DestinationViewController is SetStartViewController setStartViewController)
            {
                CurrentMedi.Dosage = double.Parse(LabelDosage.Text);
                setStartViewController.CurrentMedi = CurrentMedi;
            }
        }
    }
}