using System;
using com.b_velop.OneMediPlan.Domain;
using com.b_velop.OneMediPlan.Domain.Enums;
using com.b_velop.OneMediPlan.Meta;
using com.b_velop.OneMediPlan.ViewModels;
using Foundation;
using Ninject;
using UIKit;
using com.b_velop.OneMediPlan.Services;

namespace com.b_velop.OneMediPlan.iOS
{
    public partial class IntervallTypeViewController : UIViewController
    {
        public IntervallTypeViewController(IntPtr handle) : base(handle)
        {
            ViewModel = App.Container.Get<NewMediViewModel>();
        }

        public NewMediViewModel ViewModel { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = Strings.INTERVALL_TYPE;
            if (AppStore.Instance.User.Medis == null ||
               AppStore.Instance.User.Medis.Count == 0)
                Depends.Enabled = false;
            else Depends.Enabled = true;
        }

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
                    intervallType = IntervallType.IfNeeded;
                    break;
                case 5:
                    intervallType = IntervallType.DailyAppointment;
                    break;
                default:
                    break;
            }
            ViewModel.IntervallType = intervallType;
        }
    }
}