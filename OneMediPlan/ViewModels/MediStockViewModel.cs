using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Ninject;
using OneMediPlan.Models;

namespace OneMediPlan.ViewModels
{
    public class MediStockViewModel : BaseViewModel
    {
        public ICommand SaveStockCommand { get; }

        Medi _currentMedi;
        string _stock;
        string _stockMinimum;

        public Medi CurrentMedi
        {
            get => _currentMedi;
            set
            {
                _currentMedi = value;
                OnPropertyChanged("CurrentMedi");
            }
        }

        public string Stock
        {
            get => _stock;
            set => _stock = value;
        }
        public string StockMinimum
        {
            get => _stockMinimum;
            set => SetProperty(ref _stockMinimum, value);
        }

        public MediStockViewModel()
        {
            Title = "Vorrat";
            SaveStockCommand = new Command(SaveStockExecute, SaveStockCanExecute);
        }

        public async Task Init()
        {
            var store = App.Container.Get<IDataStore<Medi>>();
            CurrentMedi = await store.GetItemAsync(Guid.Empty);
            if (CurrentMedi.MinimumStock > 0)
                StockMinimum = CurrentMedi.MinimumStock.ToString();
            return;
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

            if (double.TryParse(Stock, out var stock))
                CurrentMedi.Stock = stock;

            if (double.TryParse(StockMinimum, out var stockMin))
                CurrentMedi.MinimumStock = stockMin;

            await store.UpdateItemAsync(CurrentMedi);
        }
    }
}
