using System;
using UIKit;
using OneMediPlan.ViewModels;
using Ninject;
using Foundation;

namespace com.b_velop.OneMediPlan.iOS
{
    public partial class WeekdayViewController : UIViewController
    {
        public WeekdayViewModel ViewModel { get; set; }

        partial void WeekdayValueChanged(UISwitch sender)
        {
            if (sender == null)
                return;
            var i = sender.Tag == 7 ? 0 : sender.Tag;
            ViewModel.Weekdays[i] = sender.On;
            //ViewModel.SetWeekday((int)i, sender.On);
            ButtonWeiter.Hidden = !ViewModel.NextCommand.CanExecute(null);
        }

        public WeekdayViewController(IntPtr handle) : base(handle)
        {
            ViewModel = App.Container.Get<WeekdayViewModel>();
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        partial void ButtonWeiter_TouchUpInside(UIButton sender)
        => ViewModel.NextCommand.Execute(null);

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is WeekdayViewModel viewModel)
            {
                if (e.PropertyName.Equals(Strings.WEEKDAYS))
                {
                    // TODO - Set Weekdays on UIControll
                }
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = NSBundle.MainBundle.GetLocalizedString(Strings.WEEKDAYS);
            ButtonWeiter.Hidden = true;
            ViewModel.Init();
        }
    }
}