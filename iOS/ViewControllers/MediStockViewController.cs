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
    }
}