using System.Windows.Input;
using com.b_velop.OneMediPlan.Helpers;
using com.b_velop.OneMediPlan.Models;
using com.b_velop.OneMediPlan.Services;
using Ninject;

namespace com.b_velop.OneMediPlan.ViewModels
{
    public class IntervallTypeViewModel : BaseViewModel
    {
        AppMedi _currentMedi;
        public AppMedi CurrentMedi { get => _currentMedi; set => SetProperty(ref _currentMedi, value); }
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
                //CurrentMedi.IntervallType = type;
                //var store = App.Container.Get<IMediDataStore>();
                //store.SetTemporaryMedi(CurrentMedi);
            }
        }

        public void Init()
        {
            //var store = App.Container.Get<IMediDataStore>();
            //CurrentMedi = store.GetTemporaryMedi();
        }
    }
}