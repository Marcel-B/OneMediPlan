using System;

using Foundation;
using UIKit;

namespace OneMediPlan.iOS.CustomCells
{
    public partial class MyMediTableViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("MyMediTableViewCell");
        public static readonly UINib Nib;

        public string Name { get => LabelName.Text; set => LabelName.Text = value; }


        static MyMediTableViewCell()
        {
            Nib = UINib.FromName("MyMediTableViewCell", NSBundle.MainBundle);
        }

        protected MyMediTableViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
