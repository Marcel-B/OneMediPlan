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
            set
            {
                _currentMedi = value;
                OnPropertyChanged("CurrentMedi");
            }
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

        public void Init()
        {
            var store = App.Container.Get<IMediDataStore>();
            CurrentMedi = store.GetTemporaryMedi();
            Name = CurrentMedi.Name;
        }

        private bool CanExecuteSaveName(object obj)
            => obj.ToString().Length > 0;

        private void SaveNameExecute(object obj)
        {
            var store = App.Container.Get<IMediDataStore>();
            CurrentMedi.Name = Name;
            store.SetTemporaryMedi(CurrentMedi);
        }
    }
}
