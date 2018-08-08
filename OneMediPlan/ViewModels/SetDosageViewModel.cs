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
        private string _dosage;

        public Medi CurrentMedi
        {
            get => _currentMedi;
            set => SetProperty(ref _currentMedi, value);
        }

        public string Dosage
        {
            get => _dosage;
            set => SetProperty(ref _dosage, value);
        }

        public SetDosageViewModel()
        {

        }

        public async Task Init()
        {
            var store = App.Container.Get<IDataStore<Medi>>();
            CurrentMedi = await store.GetItemAsync(Guid.Empty);
            return;
        }
    }
}
