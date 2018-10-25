using System;
using UIKit;

namespace com.b_velop.OneMediPlan.iOS
{
    public partial class BrowseItemDetailViewController : UIViewController
    {
        public MediDetailViewModel ViewModel { get; set; }
        public BrowseItemDetailViewController(IntPtr handle) : base(handle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = ViewModel.Title;
            ItemNameLabel.Text = ViewModel.Medi.Name;
            ItemNextDatePanel.Text = ViewModel.Medi.NextDate.ToString();
        }
    }
}
