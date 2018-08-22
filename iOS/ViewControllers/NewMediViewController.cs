using System;
using UIKit;
using OneMediPlan.ViewModels;
using Ninject;

namespace OneMediPlan.iOS
{
    public partial class NewMediViewController : UIViewController
    {
        NewMediViewModel ViewModel { get; set; }

        void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is NewMediViewModel viewModel)
            {
                if (e.PropertyName.Equals("CurrentMedi"))
                {
                    TextFieldName.Text = viewModel.CurrentMedi.Name;
                }
                else if (e.PropertyName.Equals("Name"))
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

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();
            await ViewModel.Init();
            Title = ViewModel.Title;
        }
    }
}
