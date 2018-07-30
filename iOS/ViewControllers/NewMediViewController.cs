using System;
using OneMediPlan.Models;
using UIKit;
using System.Runtime.Remoting.Channels;
using Foundation;
using OneMediPlan.ViewModels;

namespace OneMediPlan.iOS
{
    public partial class NewMediViewController : UIViewController
    {
        public NewMediViewModel ViewModel { get; set; }
        public Medi CurrentMedi { get; set; }

        public NewMediViewController(IntPtr handle) : base(handle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ViewModel = new NewMediViewModel();

            Title = ViewModel.Title;
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
                    Name = TextFieldName.Text,
                    Create = DateTimeOffset.Now
                };
            }
        }
    }
}
