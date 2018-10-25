using Foundation;
using System;
using UIKit;
using Ninject;
using com.b_velop.OneMediPlan.Models;
using com.b_velop.OneMediPlan.ViewModels;
using com.b_velop.OneMediPlan.Meta;

namespace com.b_velop.OneMediPlan.iOS
{
    public partial class IntervallTypeViewController : UIViewController
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

        public AppMedi CurrentMedi { get; set; }
        public IntervallTypeViewModel ViewModel;

        public IntervallTypeViewController(IntPtr handle) : base(handle)
        {

            ViewModel = App.Container.Get<IntervallTypeViewModel>();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ViewModel.Init();
            Title = NSBundle.MainBundle.GetLocalizedString(Strings.INTERVALL_TYPE);
        }
    }
}