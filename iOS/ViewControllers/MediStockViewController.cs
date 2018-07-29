using Foundation;
using System;
using UIKit;
using OneMediPlan.Models;

namespace OneMediPlan.iOS
{
    public partial class MediStockViewController : UIViewController
    {
        public Medi CurrentMedi { get; set; }

        public MediStockViewController (IntPtr handle) : base (handle){}

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if(segue.DestinationViewController is SetIntervallTypeViewController viewController){
                CurrentMedi.MinimumStock = double.Parse(LabelMinimumStock.Text);
                CurrentMedi.Stock = double.Parse(LabelCurrentStock.Text);
                viewController.CurrentMedi = CurrentMedi;
            }
        }
    }
}