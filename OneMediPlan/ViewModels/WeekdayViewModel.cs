using com.b_velop.OneMediPlan.Domain;
using com.b_velop.OneMediPlan.Helpers;
using com.b_velop.OneMediPlan.Models;
using com.b_velop.OneMediPlan.Services;
using Ninject;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace com.b_velop.OneMediPlan.ViewModels
{
    public class WeekdayViewModel : BaseViewModel
    {
        public ICommand NextCommand { get; }

        private Medi _currentMedi;
        private bool[] _weekdays;

        public Medi CurrentMedi
        {
            get => _currentMedi;
            set => SetProperty(ref _currentMedi, value);
        }

        public bool[] Weekdays
        {
            get => _weekdays;
            set => SetProperty(ref _weekdays, value);
        }

        //public void SetWeekday(int idx, bool value)
        //{
        //    Weekdays[idx] = value;
        //}

        public WeekdayViewModel()
        {
            NextCommand = new Command(NextCommandExecute, NextCommandCanExecute);
        }

        public void Init()
        {
            //_weekdays = new bool[7];
            //var store = App.Container.Get<IMediDataStore>();
            //CurrentMedi = store.GetTemporaryMedi();
            //if (CurrentMedi.Weekdays != null)
                //Weekdays = CurrentMedi.Weekdays;
        }

        private void NextCommandExecute(object obj)
        {
            //CurrentMedi.Weekdays = Weekdays;
            //var store = App.Container.Get<MediDataStore>();
            //store.SetTemporaryMedi(CurrentMedi);
        }

        private bool NextCommandCanExecute(object obj)
            => Weekdays.Contains(true);
    }
}
