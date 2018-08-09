﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using OneMediPlan.Models;
using System.Threading.Tasks;
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

        public async Task Init()
        {
            var store = App.Container.Get<IDataStore<Medi>>();
            CurrentMedi = await store.GetItemAsync(Guid.Empty);
        }
        private async void SaveMediExecute(object obj)
        {
            var store = App.Container.Get<IDataStore<Medi>>();

            await store.AddItemAsync(CurrentMedi);
        }

        private bool SaveMediCanExecute(object obj)
        {
            return true;
        }
    }
}
