using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;
using Ninject;
using OneMediPlan.Models;

namespace OneMediPlan.ViewModels
{
    public class SetStartViewModel : BaseViewModel
    {
        public ICommand NextCommand { get; }

        Medi _currentMedi;
        public Medi CurrentMedi
        {
            get => _currentMedi;
            set => SetProperty(ref _currentMedi, value);
        }

        public DateTimeOffset StartDate { get; set; }

     
        public SetStartViewModel()
        {
            NextCommand = new Command(NextCommandExecute, NextCommandCanExecute);
        }

        public void Init()
        {
            var store = App.Container.Get<IMediDataStore>();
            CurrentMedi = store.GetTemporaryMedi();
        }

        private void NextCommandExecute(object obj)
        {
            var store = App.Container.Get<IMediDataStore>();
            CurrentMedi.NextDate = StartDate;
            store.SetTemporaryMedi(CurrentMedi);
        }

        private bool NextCommandCanExecute(object obj) => true;
    }
}
