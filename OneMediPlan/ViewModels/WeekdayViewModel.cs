using Ninject;
using OneMediPlan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OneMediPlan.ViewModels
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
            get => _weekdays ?? new bool[7];
            set => SetProperty(ref _weekdays, value);
        }

        public WeekdayViewModel()
        {
            NextCommand = new Command(ExecuteNextCommand, CanExecuteNextCommand);
        }

        public async Task Init()
        {
            var store = App.Container.Get<IDataStore<Medi>>();
            CurrentMedi = await store.GetItemAsync(Guid.Empty);
        }

        public async void ExecuteNextCommand(object obj)
        {
            var days = new Weekdays();
            days.Id = Guid.NewGuid();
            days.MediFk = CurrentMedi.Id;
            days.Days = Weekdays;
            var store = App.Container.Get<IDataStore<Weekdays>>();
            await store.AddItemAsync(days);
        }

        public bool CanExecuteNextCommand(object obj)
            => Weekdays.Contains(true);
    }
}
