using System;
using UIKit;
using Ninject;
using Foundation;
using com.b_velop.OneMediPlan.ViewModels;
using com.b_velop.OneMediPlan.Meta;

namespace com.b_velop.OneMediPlan.iOS
{
    public partial class SaveMediViewController : UIViewController
    {
       public SaveMediViewModel ViewModel { get; set; }

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