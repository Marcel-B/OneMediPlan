using System;
using System.Windows.Input;
using Ninject;
using OneMediPlan.Models;

namespace OneMediPlan.ViewModels
{
    public class MediStockViewModel : BaseViewModel
    {
        public ICommand SaveStockCommand { get; }

        Medi _medi;
        string _stock;
        string _stockMinimum;

        public Medi Medi { get => _medi; set => SetProperty(ref _medi, value); }
        public string Stock { get => _stock; set => _stock = value; }
        public string StockMinimum { get => _stockMinimum; set => _stockMinimum = value; }

        public MediStockViewModel()
        {
            Title = "Vorrat";
            SaveStockCommand = new Command(SaveStockExecute, SaveStockCanExecute);
        }

        public void Init()
        {
            GetMedi();
        }

        private async void GetMedi()
        {
            var store = App.Container.Get<IDataStore<Medi>>();
            Medi = await store.GetItemAsync(Guid.Empty);
        }

        private bool SaveStockCanExecute(object obj)
        {
            if (obj is string[] arr)
            {
                if (string.IsNullOrWhiteSpace(arr[0])) return false;
                if (string.IsNullOrWhiteSpace(arr[1])) return false;
                return true;
            }
            return false;
        }
        private async void SaveStockExecute(object obj)
        {
            var store = App.Container.Get<IDataStore<Medi>>();
            var medi = await store.GetItemAsync(Guid.Empty);

            if (double.TryParse(Stock, out var stock))
                medi.Stock = stock;

            if (double.TryParse(StockMinimum, out var stockMin))
                medi.MinimumStock = stockMin;

            await store.UpdateItemAsync(medi);
        }
    }
}
