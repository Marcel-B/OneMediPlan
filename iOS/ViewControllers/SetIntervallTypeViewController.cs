using Foundation;
using System;
using UIKit;
using OneMediPlan.Models;
using OneMediPlan.ViewModels;
using Ninject;
using OneMediPlan.Helpers;

namespace OneMediPlan.iOS
{
    public partial class SetIntervallTypeViewController : UIViewController
    {
        partial void SetIntervallTouched(UIButton sender)
        {
            var t = sender.Tag;
            var intervallType = IntervallType.Nothing;
            switch (t)
            {
                case 1:
                    intervallType = IntervallType.Intervall;
                    break;
                case 2:
                    intervallType = IntervallType.Depend;
                    break;
                case 3:
                    intervallType = IntervallType.Weekdays;
                    break;
                case 4:
                    intervallType = IntervallType.IfNedded;
                    break;
                case 5:
                    intervallType = IntervallType.DailyAppointment;
                    break;
                default:
                    break;
            }
            ViewModel.SelectIntervallCommand.Execute(intervallType);
        }

        public Medi CurrentMedi { get; set; }
        SetIntervallTypeViewModel ViewModel;

        public SetIntervallTypeViewController(IntPtr handle) : base(handle)
        {
            ViewModel = App.Container.Get<SetIntervallTypeViewModel>();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ViewModel.Init();
            Title = NSBundle.MainBundle.GetLocalizedString(Strings.INTERVALL_TYPE);
        }
    }
}