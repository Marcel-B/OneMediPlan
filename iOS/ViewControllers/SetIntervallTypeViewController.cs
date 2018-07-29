using Foundation;
using System;
using UIKit;
using OneMediPlan.Models;

namespace OneMediPlan.iOS
{
    public partial class SetIntervallTypeViewController : UIViewController
    {
        public Medi CurrentMedi { get; set; }

        public SetIntervallTypeViewController(IntPtr handle) : base(handle)
        {
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if(segue.DestinationViewController is SetDependencyViewController viewController){
                viewController.CurrentMedi = CurrentMedi;
            }
        }
    }
}