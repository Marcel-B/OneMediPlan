using System;
using OneMediPlan.Models;
using System.Threading.Tasks;
using Ninject;
using System.Windows.Input;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace OneMediPlan.ViewModels
{
    public class IntervallViewModel : BaseViewModel
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

        public IntervallViewModel()
        {
            Title = "Intervall";
            NextCommand = new Command(NextCommandExecute, NextCommandCanExecute);
        }

        private bool NextCommandCanExecute(object obj)
            => int.TryParse(obj.ToString(), out Intervall);

        private void NextCommandExecute(object obj)
        {
            CurrentMedi.PureIntervall = Intervall;
            CurrentMedi.IntervallTime = IntervallTime;
            var store = App.Container.Get<IMediDataStore>();
            store.SetTemporaryMedi(CurrentMedi);
        }

        // Vielleich über ne property und property changed
        // wenn das medi fertig geladen ist
        public bool ShowLable()
            => CurrentMedi.DependsOn != Guid.Empty;

        public async Task Init()
        {
            var store = App.Container.Get<IMediDataStore>();
            CurrentMedi = store.GetTemporaryMedi();
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
