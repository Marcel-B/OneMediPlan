using Ninject;
using OneMediPlan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OneMediPlan.ViewModels
{
    public class DependencyViewModel : BaseViewModel
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

        public DependencyViewModel()
        {
            Title = "Abhängigkeit";
            NextCommand = new Command(NextCommandExecute, NextCommandCanExecute);
        }

        public async Task Init()
        {
            var store = App.Container.Get<IMediDataStore>();
            CurrentMedi = store.GetTemporaryMedi();
            var medis = await store.GetItemsAsync();
            Medis = medis
                .Where(m => m.Id != Guid.Empty)
                .ToList();
            ParentMedi = Medis.First();
        }

        public void NextCommandExecute(object obj)
        {
            CurrentMedi.DependsOn = ParentMedi.Id;
            var store = App.Container.Get<IMediDataStore>();
            store.SetTemporaryMedi(CurrentMedi);
        }

        public bool NextCommandCanExecute(object obj)
            => ParentMedi != null;
    }
}
