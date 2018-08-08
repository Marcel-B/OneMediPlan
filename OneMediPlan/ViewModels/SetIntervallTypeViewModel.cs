using OneMediPlan.Models;
using System.Windows.Input;
using Ninject;
using System;
using System.Threading.Tasks;
namespace OneMediPlan.ViewModels
{
    public class SetIntervallTypeViewModel : BaseViewModel
    {
        Medi _currentMedi;
        public Medi CurrentMedi { get => _currentMedi; set => SetProperty(ref _currentMedi, value); }
        public ICommand SelectIntervallCommand { get; set; }


        public SetIntervallTypeViewModel()
        {
            Title = "Intervalltyp";

            SelectIntervallCommand = new Command(async (object obj) =>
            {
                if (obj is IntervallType type)
                {
                    CurrentMedi.IntervallType = type;
                    var store = App.Container.Get<IDataStore<Medi>>();
                    await store.UpdateItemAsync(CurrentMedi);
                }
            });
        }

        public async Task Init()
        {
            var store = App.Container.Get<IDataStore<Medi>>();
            CurrentMedi = await store.GetItemAsync(Guid.Empty);
            return;
        }
    }
}