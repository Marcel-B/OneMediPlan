using System;
using OneMediPlan.Models;
using UIKit;

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

            btnSaveItem.TouchUpInside += (sender, e) =>
            {
                var item = new Medi
                {
                    Name = txtTitle.Text,
                };
                ViewModel.AddItemCommand.Execute(item);
                NavigationController.PopToRootViewController(true);
            };
        }
    }
}
