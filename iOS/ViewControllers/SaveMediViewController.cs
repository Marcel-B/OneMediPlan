using Foundation;
using System;
using UIKit;
using OneMediPlan.Models;
using OneMediPlan.Helpers;
using Ninject;
using OneMediPlan.ViewModels;

namespace OneMediPlan.iOS
{
    public partial class SaveMediViewController : UIViewController
    {
        public string Name { get => LabelName.Text; set => LabelName.Text = value; }
        public string Intervall { get => LabelIntevall.Text; set => LabelIntevall.Text = value; }
        public string Dosage { get => LabelDosage.Text; set => LabelDosage.Text = value; }



        async partial void UIButton18059_TouchUpInside(UIButton sender)
        {
            var dataStore = App.Container.Get<IDataStore<Medi>>();
            await dataStore.AddItemAsync(CurrentMedi);
            NavigationController.PopToRootViewController(true);
        }

        public Medi CurrentMedi { get; set; }

        public SaveMediViewController(IntPtr handle) : base(handle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            //ButtonSave.TitleLabel.Text = $"{CurrentMedi.Name} speichern";
            Name = CurrentMedi.Name;
            Intervall = CurrentMedi.IntervallType.ToString();
            Dosage = CurrentMedi.GetDosage();
        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            //ButtonSave.TitleLabel.Text = $"{CurrentMedi.Name} speichern";
        }
    }
}