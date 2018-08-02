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
        public UIColor ImageColor { get => Img.BackgroundColor; set => Img.BackgroundColor = value; }
        public UIColor Background { get => ContentView.BackgroundColor; set => ContentView.BackgroundColor = value; }

        public string Next { get => LabelNext.Text; set => LabelNext.Text = value; }
        public string Last { get => LabelLast.Text; set => LabelLast.Text = value; }
        public string Stock { get => LabelStock.Text; set => LabelStock.Text = value; }
        public string Dosage { get => LabelDosage.Text; set => LabelDosage.Text = value; }


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
