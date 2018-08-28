using System;
using UIKit;
using OneMediPlan.ViewModels;
using Ninject;
using OneMediPlan.Helpers;

namespace OneMediPlan.iOS
{
    public partial class WeekdayViewController : UIViewController
    {
        WeekdayViewModel ViewModel;

        partial void WeekdayValueChanged(UISwitch sender)
        {
            if (sender == null)
                return;
            var i = sender.Tag == 7 ? 0 : sender.Tag;
            ViewModel.Weekdays[i] = sender.On;
            ButtonWeiter.Hidden = ViewModel.NextCommand.CanExecute(null);
        }

        public WeekdayViewController(IntPtr handle) : base(handle)
        {
            ViewModel = App.Container.Get<WeekdayViewModel>();
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(sender is WeekdayViewModel viewModel)
            {
                if (e.PropertyName.Equals(Strings.WEEKDAYS))
                {
                    // TODO - Set Weekdays on UIControll
                }
            }
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ButtonWeiter.Hidden = true;
            await ViewModel.Init();
        }
    }
}