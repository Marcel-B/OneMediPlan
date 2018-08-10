using System;
using UIKit;
using OneMediPlan.ViewModels;
using Ninject;

namespace OneMediPlan.iOS
{
    public partial class NewMediViewController : UIViewController
    {
        NewMediViewModel ViewModel { get; set; }

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
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = ViewModel.Title;
        }
    }
}
