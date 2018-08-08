using Foundation;
using System;
using UIKit;
using OneMediPlan.Models;
using OneMediPlan.ViewModels;
using Ninject;

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

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();
            await ViewModel.Init();
        }

        //public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        //{
        //CurrentMedi.DependsOn = Guid.Empty;
        //if (segue.DestinationViewController is SetDependencyViewController viewController)
        //{
        //    CurrentMedi.IntervallType = IntervallType.Depend;
        //    viewController.CurrentMedi = CurrentMedi;
        //    return;
        //}
        //if (segue.DestinationViewController is SetIntervallViewController setIntervallViewController)
        //{
        //    CurrentMedi.IntervallType = IntervallType.Intervall;
        //    setIntervallViewController.CurrentMedi = CurrentMedi;
        //    return;
        //}
        //if (segue.DestinationViewController is WeekdayViewController weekdayViewController)
        //{
        //    CurrentMedi.IntervallType = IntervallType.Weekdays;
        //    weekdayViewController.CurrentMedi = CurrentMedi;
        //    return;
        //}
        //if (segue.DestinationViewController is SaveMediViewController saveMediViewController)
        //{
        //    CurrentMedi.IntervallType = IntervallType.IfNedded;
        //    saveMediViewController.CurrentMedi = CurrentMedi;
        //    return;
        //}
        //if (segue.DestinationViewController is SetDailyViewController setDailyViewController)
        //{
        //    CurrentMedi.IntervallType = IntervallType.DailyAppointment;
        //    setDailyViewController.CurrentMedi = CurrentMedi;
        //    return;
        //}
        //}
    }
}