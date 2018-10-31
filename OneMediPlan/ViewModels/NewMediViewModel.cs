using System;
using System.Windows.Input;

using com.b_velop.OneMediPlan.Domain;
using com.b_velop.OneMediPlan.Domain.Enums;
using com.b_velop.OneMediPlan.Domain.Services;
using com.b_velop.OneMediPlan.Helpers;
using com.b_velop.OneMediPlan.Meta;
using com.b_velop.OneMediPlan.Services;
using Ninject;

namespace com.b_velop.OneMediPlan.ViewModels
{
    public class NewMediViewModel : BaseViewModel
    {
        public ICommand SaveNameCommand { get; }

        string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        string _stock;
        public string Stock
        {
            get => _stock;
            set => SetProperty(ref _stock, value);
        }

        string _stockMinimum;
        public string StockMinimum
        {
            get => _stockMinimum;
            set => SetProperty(ref _stockMinimum, value);
        }

        IntervallType _intervallType;
        public IntervallType IntervallType
        {
            get => _intervallType;
            set => SetProperty(ref _intervallType, value);
        }

        private IntervallTime _intervallTime;
        public IntervallTime IntervallTime
        {
            get => _intervallTime;
            set => SetProperty(ref _intervallTime, value);
        }

        private string _dosage;
        public string Dosage
        {
            get => _dosage;
            set => SetProperty(ref _dosage, value);
        }

        private string _intervall;
        public string Intervall
        {
            get => _intervall;
            set => SetProperty(ref _intervall, value);
        }

        private int _dependsOnIdx;
        public int DependsOnIdx
        {
            get => _dependsOnIdx;
            set => SetProperty(ref _dependsOnIdx, value);
        }

        public NewMediViewModel()
        {
            Title = Strings.NEW;
            SaveNameCommand = new Command(
                SaveNameExecute,
                CanExecuteSaveName);
        }

        private bool CanExecuteSaveName(object obj)
            => !string.IsNullOrWhiteSpace(Name) &&
                  !string.IsNullOrWhiteSpace(Stock) &&
                  !string.IsNullOrWhiteSpace(StockMinimum);

        private async void SaveNameExecute(object obj)
        {
            var store = App.Container.Get<IDataStore<Medi>>();
            var medi = new Medi
            {
                Id = Guid.NewGuid(),
                Created = DateTimeOffset.Now,
                LastEdit = DateTimeOffset.Now,
                IntervallType = _intervallType,
                Name = Name,
                Stock = double.Parse(Stock),
                MinimumStock = double.Parse(StockMinimum),
            };
            AppStore.Instance.User.Medis.Add(medi);
            await store.AddItemAsync(medi);
        }
    }
}
