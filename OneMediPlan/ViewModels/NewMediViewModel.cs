using OneMediPlan.Models;
using System.Windows.Input;
using System;
using Ninject;
using System.Threading.Tasks;

namespace OneMediPlan.ViewModels
{
    public class NewMediViewModel : BaseViewModel
    {
        public ICommand SaveNameCommand { get; }

        private Medi _currentMedi;
        public Medi CurrentMedi
        {
            get => _currentMedi;
            set => SetProperty(ref _currentMedi, value);
        }

        string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public NewMediViewModel()
        {
            Title = "Neu";
            SaveNameCommand = new Command(
                SaveNameExecute,
                CanExecuteSaveName);
        }

        public async Task Init()
        {
            var store = App.Container.Get<IDataStore<Medi>>();
            var medi = await store.GetItemAsync(Guid.Empty);
            if (medi == null)
                medi = new Medi
                {
                    Id = Guid.Empty,
                    Name = string.Empty,
                    Create = DateTimeOffset.Now
                };
            CurrentMedi = medi;
            Name = medi.Name;
            return;
        }

        private bool CanExecuteSaveName(object obj)
            => obj.ToString().Length > 0;

        private async void SaveNameExecute(object obj)
        {
            var store = App.Container.Get<IDataStore<Medi>>();
            CurrentMedi.Name = Name;
            store.AddItemAsync(CurrentMedi);
        }
    }
}
