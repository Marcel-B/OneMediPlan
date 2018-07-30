using Foundation;
using System;
using UIKit;
using OneMediPlan.Models;
using OneMediPlan.Helpers;

namespace OneMediPlan.iOS
{
    public partial class SaveMediViewController : UIViewController
    {
        async partial void UIButton18059_TouchUpInside(UIButton sender)
        {
            var DataStore = AppStore.GetInstance();
            await DataStore.AddItemAsync(CurrentMedi);
            NavigationController.PopToRootViewController(true);
        }

        public Medi CurrentMedi { get; set; }

        public SaveMediViewController(IntPtr handle) : base(handle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            //ButtonSave.TitleLabel.Text = $"{CurrentMedi.Name} speichern";
        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            ButtonSave.TitleLabel.Text = $"{CurrentMedi.Name} speichern";
        }
    }
}