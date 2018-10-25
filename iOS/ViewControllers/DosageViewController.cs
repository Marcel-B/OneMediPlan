using Foundation;
using System;
using UIKit;
using System.ComponentModel;
using Ninject;

using com.b_velop.OneMediPlan.ViewModels;
using com.b_velop.OneMediPlan.Meta;

namespace com.b_velop.OneMediPlan.iOS
{
    public partial class DosageViewController : UIViewController
    {
        public DosageViewModel ViewModel { get; set; }
        public DosageViewController(IntPtr handle) : base(handle)
        {
            ViewModel = App.Container.Get<DosageViewModel>();
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is DosageViewModel viewModel)
            {
                if (e.PropertyName.Equals(Strings.CURRENT_MEDI))
                {
                    if (viewModel.CurrentMedi.Dosage > 0)
                        LabelDosage.Text = viewModel.CurrentMedi.Dosage.ToString();
                }
            }
        }

        partial void ButtonNext_TouchUpInside(UIButton sender)
            => ViewModel.NextCommand.Execute(null);

        partial void TextFieldDosageChanged(UITextField sender)
        {
            ButtonNext.Hidden = !ViewModel.NextCommand.CanExecute(sender.Text);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ViewModel.Init();
            Title = NSBundle.MainBundle.GetLocalizedString(Strings.DOSAGE);
        }
    }
}