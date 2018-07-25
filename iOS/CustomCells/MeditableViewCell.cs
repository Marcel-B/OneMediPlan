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
        public string Subtitle { get => LabelSubtitle.Text; set => LabelSubtitle.Text = value; }

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
