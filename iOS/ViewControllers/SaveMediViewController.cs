using System;
using com.b_velop.OneMediPlan.Meta;
using com.b_velop.OneMediPlan.ViewModels;
using Ninject;
using UIKit;

namespace com.b_velop.OneMediPlan.iOS
{
    public partial class SaveMediViewController : UIViewController
    {
        public NewMediViewModel ViewModel { get; set; }

        public SaveMediViewController(IntPtr handle) : base(handle)
        {
            ViewModel = App.Container.Get<NewMediViewModel>();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = Strings.SAVE;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            ButtonSave.TouchUpInside += ButtonSave_TouchUpInside;
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            ButtonSave.TouchUpInside -= ButtonSave_TouchUpInside;
        }

        void ButtonSave_TouchUpInside(object sender, EventArgs e)
        {
            ViewModel.SaveNameCommand.Execute(null);
            NavigationController.PopToRootViewController(true);
        }
    }
}