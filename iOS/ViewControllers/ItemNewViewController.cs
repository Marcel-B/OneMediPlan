using System;
using OneMediPlan.Models;
using UIKit;
using System.Runtime.Remoting.Channels;
using Foundation;

namespace OneMediPlan.iOS
{
    public partial class ItemNewViewController : UIViewController
    {
        //public ItemsViewModel ViewModel { get; set; }
        public MediViewModel ViewModel { get; set; }

        public ItemNewViewController(IntPtr handle) : base(handle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override bool ShouldPerformSegue(string segueIdentifier, NSObject sender)
            => !string.IsNullOrWhiteSpace(TextFieldName.Text);

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.DestinationViewController is MediStockViewController mediStockViewController)
            {
                mediStockViewController.CurrentMedi = new Medi
                {
                    Id = Guid.NewGuid(),
                    Name = TextFieldName.Text
                };
            }
        }
    }
}
