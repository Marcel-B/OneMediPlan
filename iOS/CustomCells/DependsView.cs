using System;
using UIKit;

namespace com.b_velop.OneMediPlan.iOS
{
    public partial class DependsView : UIView
    {
        public string Dosage
        {
            get => TextFieldDosage.Text;
            set => TextFieldDosage.Text = value;
        }

        public DependsView(IntPtr handle) : base(handle)
        {
        }

        public override void AwakeFromNib()
        {
            // Initialize the view here.
        }
    }
}