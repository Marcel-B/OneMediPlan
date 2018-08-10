using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Threading.Tasks;
using OneMediPlan.Models;
using Ninject;

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
            Title = "Termine";
            NextCommand = new Command(NextCommandExecute, NextCommandCanExecute);
        }

        public async Task Init()
        {
            var store = App.Container.Get<IDataStore<Medi>>();
            var medi = await store.GetItemAsync(Guid.Empty);
            medi.DailyAppointments = null;
            CurrentMedi = medi;
        }

        private async void NextCommandExecute(object obj)
        {
            if (obj is List<Tuple<Hour, Minute>> cl)
            {
                var store = App.Container.Get<IDataStore<Medi>>();
                CurrentMedi.DailyAppointments = cl;
                await store.UpdateItemAsync(CurrentMedi);
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
