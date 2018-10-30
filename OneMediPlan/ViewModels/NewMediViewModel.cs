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
        public int IntervallType
        {
            get => (int)_intervallType;
            set => SetProperty(ref _intervallType, (IntervallType)value);
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
