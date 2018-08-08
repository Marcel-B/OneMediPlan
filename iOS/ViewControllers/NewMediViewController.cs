using System;
using OneMediPlan.Models;
using UIKit;
using System.Runtime.Remoting.Channels;
using Foundation;
using OneMediPlan.ViewModels;
using Ninject;

namespace OneMediPlan.iOS
{
    public partial class NewMediViewController : UIViewController
    {
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

        public NewMediViewModel ViewModel { get; set; }
        //public Medi CurrentMedi { get; set; }

        public NewMediViewController(IntPtr handle) : base(handle) {
            ViewModel = App.Container.Get<NewMediViewModel>();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = ViewModel.Title;
        }

        //public override bool ShouldPerformSegue(string segueIdentifier, NSObject sender)
            //=> !string.IsNullOrWhiteSpace(TextFieldName.Text);

        //public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        //{
        //    if (segue.DestinationViewController is MediStockViewController mediStockViewController)
        //    {
        //        mediStockViewController.CurrentMedi = new Medi
        //        {
        //            Id = Guid.NewGuid(),
        //            Name = TextFieldName.Text,
        //            Create = DateTimeOffset.Now
        //        };
        //    }
        //}
    }
}
