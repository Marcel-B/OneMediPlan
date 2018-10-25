using Ninject;
using OneMediPlan.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace com.b_velop.OneMediPlan.ViewModels
{
    public class DosageViewModel : BaseViewModel
    {
        public ICommand NextCommand { get; }

        private Medi _currentMedi;
        private double _dosage;

        public Medi CurrentMedi
        {
            get => _currentMedi;
            set => SetProperty(ref _currentMedi, value);
        }

        public double Dosage
        {
            get => _dosage;
            set => SetProperty(ref _dosage, value);
        }

        public DosageViewModel()
        {
            NextCommand = new Command(ExecuteNextCommand, NextCommandCanExecute);
        }

        public void Init()
        {
            var store = App.Container.Get<IMediDataStore>();
            CurrentMedi = store.GetTemporaryMedi();
        }

        private void ExecuteNextCommand(object obj)
        {
            CurrentMedi.Dosage = Dosage;
            var store = App.Container.Get<IMediDataStore>();
            store.SetTemporaryMedi(CurrentMedi);
        }

        private bool NextCommandCanExecute(object obj)
        {
            if (obj.ToString().Length <= 0) return false;
            if (double.TryParse(obj.ToString(), out var dos))
            {
                Dosage = dos;
                return true;
            }
            return false;
        }
    }
}
