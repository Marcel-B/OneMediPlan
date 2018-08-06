using Foundation;
using System;
using UIKit;
using OneMediPlan.Models;

namespace OneMediPlan.iOS
{
    public partial class SetDosageViewController : UIViewController
    {
        public Medi CurrentMedi { get; set; }
        private double _dosage;

        public SetDosageViewController(IntPtr handle) : base(handle) { }

        public override bool ShouldPerformSegue(string segueIdentifier, NSObject sender)
            => double.TryParse(LabelDosage.Text, out _dosage);

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