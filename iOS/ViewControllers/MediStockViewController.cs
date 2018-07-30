using Foundation;
using System;
using UIKit;
using OneMediPlan.Models;
using AVFoundation;

namespace OneMediPlan.iOS
{
    public partial class MediStockViewController : UIViewController
    {
        public Medi CurrentMedi { get; set; }

        public MediStockViewController(IntPtr handle) : base(handle) { }

        private double _minimumStock;
        private double _stock;

        public override bool ShouldPerformSegue(string segueIdentifier, NSObject sender)
        {
            if (double.TryParse(LabelMinimumStock.Text, out _minimumStock))
                return double.TryParse(LabelCurrentStock.Text, out _stock);
            return false;
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.DestinationViewController is SetIntervallTypeViewController viewController)
            {
                CurrentMedi.MinimumStock = _minimumStock;
                CurrentMedi.Stock = _stock;
                viewController.CurrentMedi = CurrentMedi;
            }
        }
    }
}