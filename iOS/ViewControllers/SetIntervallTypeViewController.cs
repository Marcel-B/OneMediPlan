using Foundation;
using System;
using UIKit;
using OneMediPlan.Models;

namespace OneMediPlan.iOS
{
    public partial class SetIntervallTypeViewController : UIViewController
    {
        public Medi CurrentMedi { get; set; }

        public SetIntervallTypeViewController(IntPtr handle) : base(handle) { }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            CurrentMedi.DependsOn = Guid.Empty;
            if (segue.DestinationViewController is SetDependencyViewController viewController)
            {
                CurrentMedi.IntervallType = IntervallType.Depend;
                viewController.CurrentMedi = CurrentMedi;
                return;
            }
            if (segue.DestinationViewController is SetIntervallViewController setIntervallViewController)
            {
                CurrentMedi.IntervallType = IntervallType.Intervall;
                setIntervallViewController.CurrentMedi = CurrentMedi;
                return;
            }
            if (segue.DestinationViewController is WeekdayViewController weekdayViewController)
            {
                CurrentMedi.IntervallType = IntervallType.Weekdays;
                weekdayViewController.CurrentMedi = CurrentMedi;
                return;
            }
            if (segue.DestinationViewController is SaveMediViewController saveMediViewController)
            {
                CurrentMedi.IntervallType = IntervallType.IfNedded;
                saveMediViewController.CurrentMedi = CurrentMedi;
                return;
            }
            if (segue.DestinationViewController is SetDailyViewController setDailyViewController)
            {
                CurrentMedi.IntervallType = IntervallType.DailyAppointment;
                setDailyViewController.CurrentMedi = CurrentMedi;
                return;
            }
        }
    }
}