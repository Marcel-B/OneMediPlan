using System;
using OneMediPlan.Models;
using System.Threading.Tasks;
using Ninject;
using System.Windows.Input;
using System.Collections.Generic;

namespace OneMediPlan.ViewModels
{
    public class SetIntervallViewModel : BaseViewModel
    {
        public ICommand NextCommand { get; }

        private readonly IEnumerable<string> _intervallTypes;
        public IEnumerable<string> IntervallTypes => _intervallTypes ?? new[] { "Minute(n)", "Stunde(n)", "Tag(e)", "Woche(n)" };

        bool _labelHidden;
        string _labelText;
        Medi _currentMedi;

        public bool LabelHidden
        {
            get => _labelHidden;
            set => SetProperty(ref _labelHidden, value);
        }

        public string LabelText
        {
            get => _labelText;
            set => SetProperty(ref _labelText, value);
        }

        public Medi CurrentMedi
        {
            get => _currentMedi;
            set => SetProperty(ref _currentMedi, value);
        }

        public int Intervall;
        public IntervallTime IntervallTime { get; set; }

        public SetIntervallViewModel()
        {
            Title = "Intervall";
            NextCommand = new Command(SaveMedi, Check);
        }

        private bool Check(object obj)
            => int.TryParse(obj.ToString(), out Intervall);

        private async void SaveMedi(object obj)
        {
            CurrentMedi.PureIntervall = Intervall;
            CurrentMedi.IntervallTime = IntervallTime;
            var store = App.Container.Get<IDataStore<Medi>>();
            await store.UpdateItemAsync(CurrentMedi);
        }

        // Vielleich über ne property und property changed
        // wenn das medi fertig geladen ist
        public bool ShowLable()
            => CurrentMedi.DependsOn != Guid.Empty;

        public async Task Init()
        {
            var store = App.Container.Get<IDataStore<Medi>>();
            CurrentMedi = await store.GetItemAsync(Guid.Empty);
            LabelHidden = CurrentMedi.DependsOn == Guid.Empty;
            if (!LabelHidden)
            {
                var parentMedi = await store.GetItemAsync(CurrentMedi.DependsOn);
                LabelText = parentMedi.Name;
            }
            return;
        }
    }
}
