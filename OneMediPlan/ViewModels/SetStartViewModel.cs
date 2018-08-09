using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;
using Ninject;
using OneMediPlan.Models;

namespace OneMediPlan.ViewModels
{
    public class SetStartViewModel : BaseViewModel
    {
        public ICommand NextCommand { get; }

        Medi _currentMedi;
        public Medi CurrentMedi
        {
            get => _currentMedi;
            set => SetProperty(ref _currentMedi, value);
        }

        public DateTimeOffset StartDate { get; set; }

     
        public SetStartViewModel()
        {
            NextCommand = new Command(NextCommandExecute, CanExecuteNextCommand);
        }

        public async Task Init()
        {
            var store = App.Container.Get<IDataStore<Medi>>();
            CurrentMedi = await store.GetItemAsync(Guid.Empty);
        }

        private async void NextCommandExecute(object obj)
        {
            var store = App.Container.Get<IDataStore<Medi>>();
            CurrentMedi.NextDate = StartDate;
            await store.UpdateItemAsync(CurrentMedi);
        }

        private bool CanExecuteNextCommand(object obj)
        {
            return true;
        }
    }
}
