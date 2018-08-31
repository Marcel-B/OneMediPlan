using System;
using UIKit;
using OneMediPlan.ViewModels;
using Ninject;
using OneMediPlan.Helpers;
using Foundation;

namespace OneMediPlan.iOS
{
    public partial class NewMediViewController : UIViewController
    {
        NewMediViewModel ViewModel { get; set; }

        void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is NewMediViewModel viewModel)
            {
                if (e.PropertyName.Equals(Strings.CURRENT_MEDI))
                {
                    TextFieldName.Text = viewModel.CurrentMedi.Name;
                }
                else if (e.PropertyName.Equals(Strings.NAME))
                {
                    TextFieldName.Text = viewModel.Name;
                    TextFieldEditingChanged(TextFieldName);
                }
            }
        }

        partial void TextFieldEditingChanged(UITextField sender)
        {
            ViewModel.Name = sender.Text;
            var result = ViewModel.SaveNameCommand.CanExecute(sender.Text);
            ButtonNext.Hidden = !result;
        }

        partial void TextFieldEditingBegin(UITextField sender)
        {
            var result = ViewModel.SaveNameCommand.CanExecute(sender.Text);
            ButtonNext.Hidden = !result;
        }

        partial void ButtonNext_TouchUpInside(UIButton sender)
            => ViewModel.SaveNameCommand.Execute(TextFieldName.Text);

        public NewMediViewController(IntPtr handle) : base(handle)
        {
            ViewModel = App.Container.Get<NewMediViewModel>();
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ViewModel.Init();
            Title = NSBundle.MainBundle.GetLocalizedString(Strings.NEW_MEDI);
        }
    }
}
