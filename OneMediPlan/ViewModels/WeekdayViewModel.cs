using Ninject;
using OneMediPlan.Models;
using System;
using System.Linq;
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
            NextCommand = new Command(NextCommandExecute, NextCommandCanExecute);
        }

        public async Task Init()
        {
            var store = App.Container.Get<IDataStore<Medi>>();
            CurrentMedi = await store.GetItemAsync(Guid.Empty);
        }

        private async void NextCommandExecute(object obj)
        {
            var days = new Weekdays
            {
                Id = Guid.NewGuid(),
                MediFk = CurrentMedi.Id,
                Days = Weekdays
            };
            var store = App.Container.Get<IDataStore<Weekdays>>();
            await store.AddItemAsync(days);
        }

        private bool NextCommandCanExecute(object obj)
            => Weekdays.Contains(true);
    }
}
