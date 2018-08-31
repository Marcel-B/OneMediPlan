using OneMediPlan.Models;
using System.Windows.Input;
using Ninject;

namespace OneMediPlan.ViewModels
{
    public class IntervallTypeViewModel : BaseViewModel
    {
        Medi _currentMedi;
        public Medi CurrentMedi { get => _currentMedi; set => SetProperty(ref _currentMedi, value); }
        public ICommand SelectIntervallCommand { get; set; }

        public IntervallTypeViewModel()
        {
            Title = "Intervalltyp";
            SelectIntervallCommand = new Command(SelectIntervallCommandExecute);
        }

        private void SelectIntervallCommandExecute(object obj)
        {
            if (obj is IntervallType type)
            {
                CurrentMedi.IntervallType = type;
                var store = App.Container.Get<IMediDataStore>();
                store.SetTemporaryMedi(CurrentMedi);
            }
        }

        public void Init()
        {
            var store = App.Container.Get<IMediDataStore>();
            CurrentMedi = store.GetTemporaryMedi();
        }
    }
}