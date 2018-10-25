using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using com.b_velop.OneMediPlan.Domain;
using com.b_velop.OneMediPlan.Domain.Services;
using Ninject;

namespace com.b_velop.OneMediPlan.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Medi> MediDataStore => App.Container.Get<IDataStore<Medi>>();
        public IDataStore<Weekdays> WeekdaysDataStore => App.Container.Get<IDataStore<Weekdays>>();
        public IDataStore<DailyAppointment> DailyAppointmentDataStore => App.Container.Get<IDataStore<DailyAppointment>>();
        public IDataStore<AppSettings> AppSettingsDataStore => App.Container.Get<IDataStore<AppSettings>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
