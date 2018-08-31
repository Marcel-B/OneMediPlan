﻿using System;
using System.Windows.Input;
using OneMediPlan.Models;
using Ninject;

namespace OneMediPlan.ViewModels
{
    public class SaveMediViewModel : BaseViewModel
    {
        public ICommand SaveMediCommand { get; }

        Medi _currentMedi;
        public Medi CurrentMedi
        {
            get => _currentMedi;
            set => SetProperty(ref _currentMedi, value);
        }

        public SaveMediViewModel()
        {
            SaveMediCommand = new Command(SaveMediExecute, SaveMediCanExecute);
        }

        public void Init()
        {
            var store = App.Container.Get<IMediDataStore>();
            CurrentMedi = store.GetTemporaryMedi();
        }

        private async void SaveMediExecute(object obj)
        {
            var store = App.Container.Get<MediDataStore>();
            CurrentMedi.Id = Guid.NewGuid();
            if (App.SetNotification != null)
                App.SetNotification(CurrentMedi);
            await store.AddItemAsync(CurrentMedi);
            CurrentMedi.Reset();
        }

        private bool SaveMediCanExecute(object obj)
            => true;
    }
}
