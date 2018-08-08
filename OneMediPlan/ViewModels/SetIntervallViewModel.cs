using System;
using OneMediPlan.Models;
using System.Threading.Tasks;
using Ninject;
using System.Windows.Input;
namespace OneMediPlan.ViewModels
{
    public class SetIntervallViewModel : BaseViewModel
    {
        public ICommand NextCommand { get; }

        bool _labelHidden;
        Medi _currentMedi;

        public Medi CurrentMedi { get => _currentMedi; set => SetProperty(ref _currentMedi, value); }
        public int Intervall { get; set; }
        public IntervallTime IntervallTime { get; set; }



        public SetIntervallViewModel()
        {
            Title = "Intervall";
            NextCommand = new Command(SaveMedi, Check);
        }

        private bool Check(object obj)
            => obj.ToString().Length > 0;

        private async void SaveMedi(object obj)
        {

        }

        // Vielleich über ne property und property changed
        // wenn das medi fertig geladen ist
        public bool ShowLable()
            => CurrentMedi.DependsOn != Guid.Empty;

        public async Task Init()
        {
            var store = App.Container.Get<IDataStore<Medi>>();
            CurrentMedi = await store.GetItemAsync(Guid.Empty);
            return;
        }
    }
}
