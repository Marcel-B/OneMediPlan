using Foundation;
using System;
using UIKit;
using OneMediPlan.Models;
using System.ComponentModel;
using OneMediPlan.ViewModels;
using Ninject;
using OneMediPlan.Helpers;

namespace OneMediPlan.iOS
{
    public partial class SetDosageViewController : UIViewController
    {
        SetDosageViewModel ViewModel { get; set; }
        public SetDosageViewController(IntPtr handle) : base(handle)
        {
            ViewModel = App.Container.Get<SetDosageViewModel>();
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is SetDosageViewModel viewModel)
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
            Title = ViewModel.Title;
        }
    }
}