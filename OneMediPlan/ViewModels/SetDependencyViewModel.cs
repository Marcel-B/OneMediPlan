using Ninject;
using OneMediPlan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OneMediPlan.ViewModels
{
    public class SetDependencyViewModel : BaseViewModel
    {
        public ICommand NextCommand { get; }

        public Medi ParentMedi { get; set; }

        private Medi _currentMedi;
        private IList<Medi> _medis;

        public Medi CurrentMedi
        {
            get => _currentMedi;
            set => SetProperty(ref _currentMedi, value);
        }

        public IList<Medi> Medis
        {
            get => _medis;
            set => SetProperty(ref _medis, value);
        }

        public SetDependencyViewModel()
        {
            NextCommand = new Command(ExecuteNext, CanExecuteNext);
        }

        public async Task Init()
        {
            var store = App.Container.Get<IDataStore<Medi>>();
            CurrentMedi = await store.GetItemAsync(Guid.Empty);
            var medis = await store.GetItemsAsync();
            Medis = medis
                .Where(m => m.Id != Guid.Empty)
                .ToList();
            ParentMedi = Medis.First();
        }

        public async void ExecuteNext(object obj)
        {
            CurrentMedi.DependsOn = ParentMedi.Id;
            var store = App.Container.Get<IDataStore<Medi>>();
            await store.UpdateItemAsync(CurrentMedi);
        }

        public bool CanExecuteNext(object obj)
        {
            if (ParentMedi == null) return false;
            return true;
        }
    }
}
