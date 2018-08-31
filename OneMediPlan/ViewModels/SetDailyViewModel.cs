using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Threading.Tasks;
using OneMediPlan.Models;
using Ninject;
using OneMediPlan.Helpers;

namespace OneMediPlan.ViewModels
{
    public class SetDailyViewModel : BaseViewModel
    {
        Medi _currentMedi;
        public Medi CurrentMedi
        {
            get => _currentMedi;
            set => SetProperty(ref _currentMedi, value);
        }
        public ICommand NextCommand { get; }
        public SetDailyViewModel()
        {
            Title = Strings.APPOINTMENTS;
            NextCommand = new Command(NextCommandExecute, NextCommandCanExecute);
        }

        public void Init()
        {
            var store = App.Container.Get<IMediDataStore>();
            var medi = store.GetTemporaryMedi();
            medi.DailyAppointments = null;
            CurrentMedi = medi;
        }

        private void NextCommandExecute(object obj)
        {
            if (obj is List<Tuple<Hour, Minute>> cl)
            {
                var store = App.Container.Get<IMediDataStore>();
                CurrentMedi.DailyAppointments = cl;
                store.SetTemporaryMedi(CurrentMedi);
            }
        }

        private bool NextCommandCanExecute(object obj)
        {
            if (obj is List<Tuple<Hour, Minute>> cl)
            {
                return cl.Count > 0;
            }
            return false;
        }
    }
}
