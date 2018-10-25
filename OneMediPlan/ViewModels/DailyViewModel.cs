using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Threading.Tasks;
using Ninject;
using com.b_velop.OneMediPlan.Models;
using com.b_velop.OneMediPlan.Meta;
using com.b_velop.OneMediPlan.Helpers;
using com.b_velop.OneMediPlan.Services;
using com.b_velop.OneMediPlan.Domain;

namespace com.b_velop.OneMediPlan.ViewModels
{
    public class DailyViewModel : BaseViewModel
    {
        Medi _currentMedi;
        public Medi CurrentMedi
        {
            get => _currentMedi;
            set => SetProperty(ref _currentMedi, value);
        }
        public ICommand NextCommand { get; }
        public DailyViewModel()
        {
            Title = Strings.APPOINTMENTS;
            NextCommand = new Command(NextCommandExecute, NextCommandCanExecute);
        }

        public void Init()
        {
            //var store = App.Container.Get<IMediDataStore>();
            //var medi = store.GetTemporaryMedi();
            //medi.DailyAppointments = null;
            //CurrentMedi = medi;
        }

        private void NextCommandExecute(object obj)
        {
            if (obj is List<Tuple<Hour, Minute>> appointmentList)
            {
                //var store = App.Container.Get<IMediDataStore>();
                //CurrentMedi.DailyAppointments = appointmentList;
                //store.SetTemporaryMedi(CurrentMedi);
            }
        }

        private bool NextCommandCanExecute(object obj)
        {
            if (obj is List<Tuple<Hour, Minute>> cl)
            {
                return cl.Count > 0;
            }
            return false;
        }
    }
}
