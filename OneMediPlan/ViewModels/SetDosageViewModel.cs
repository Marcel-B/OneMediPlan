using Ninject;
using OneMediPlan.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OneMediPlan.ViewModels
{
    public class SetDosageViewModel : BaseViewModel
    {
        public ICommand NextCommand { get; }

        private Medi _currentMedi;
        private double _dosage;

        public Medi CurrentMedi
        {
            get => _currentMedi;
            set => SetProperty(ref _currentMedi, value);
        }

        public double Dosage
        {
            get => _dosage;
            set => SetProperty(ref _dosage, value);
        }

        public SetDosageViewModel()
        {
            Title = "Dosis";
            NextCommand = new Command(ExecuteNextCommand, CanExecuteNextCommand);
        }

        public async Task Init()
        {
            var store = App.Container.Get<IDataStore<Medi>>();
            CurrentMedi = await store.GetItemAsync(Guid.Empty);
            return;
        }

        private async void ExecuteNextCommand(object obj)
        {
            CurrentMedi.Dosage = Dosage;
            var store = App.Container.Get<IDataStore<Medi>>();
            await store.UpdateItemAsync(CurrentMedi);
        }

        private bool CanExecuteNextCommand(object obj)
        {
            if (obj.ToString().Length <= 0) return false;
            if (double.TryParse(obj.ToString(), out var dos))
            {
                Dosage = dos;
                return true;
            }
            return false;
        }
    }
}
