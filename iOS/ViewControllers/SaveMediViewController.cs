using System;
using UIKit;
using Ninject;
using OneMediPlan.ViewModels;
using Foundation;
using OneMediPlan.Helpers;

namespace OneMediPlan.iOS
{
    public partial class SaveMediViewController : UIViewController
    {
        SaveMediViewModel ViewModel { get; set; }

        partial void UIButton18059_TouchUpInside(UIButton sender)
        {
            ViewModel.SaveMediCommand.Execute(null);
            NavigationController.PopToRootViewController(true);
        }

        public SaveMediViewController(IntPtr handle) : base(handle)
        {
            ViewModel = App.Container.Get<SaveMediViewModel>();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ViewModel.Init();
            Title = NSBundle.MainBundle.GetLocalizedString(Strings.SAVE);
        }
    }
}