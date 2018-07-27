using System;

using Foundation;
using UIKit;

namespace OneMediPlan.iOS.CustomCells
{
    public partial class MeditableViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("MeditableViewCell");
        public static readonly UINib Nib;

        public string Name { get => LabelName.Text; set => LabelName.Text = value; }
        //public string ShortInfo { get => LabelShortInfo.Text; set => LabelShortInfo.Text = value; }
        //public string Next { get => LabelNext.Text; set => LabelNext.Text = value; }
        //public string Last { get => LabelLast.Text; set => LabelLast.Text = value; }
        //public string StockInfo { get => LabelStock.Text; set => LabelStock.Text = value; }
        //public string Dosage { get => LabelUnit.Text; set => LabelUnit.Text = value; }

        static MeditableViewCell()
        {
            Nib = UINib.FromName("MeditableViewCell", NSBundle.MainBundle);
        }

        protected MeditableViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
