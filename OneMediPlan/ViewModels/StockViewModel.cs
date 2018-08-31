using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Ninject;
using OneMediPlan.Models;
using OneMediPlan.Helpers;

namespace OneMediPlan.ViewModels
{
    public class StockViewModel : BaseViewModel
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
                OnPropertyChanged(Strings.CURRENT_MEDI);
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

        public StockViewModel()
        {
            SaveStockCommand = new Command(SaveStockExecute, SaveStockCanExecute);
        }

        public void Init()
        {
            var store = App.Container.Get<IMediDataStore>();
            CurrentMedi = store.GetTemporaryMedi();
            if (CurrentMedi.MinimumStock > 0)
                StockMinimum = CurrentMedi.MinimumStock.ToString();
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

        private void SaveStockExecute(object obj)
        {
            var store = App.Container.Get<IMediDataStore>();

            if (double.TryParse(Stock, out var stock))
                CurrentMedi.Stock = stock;

            if (double.TryParse(StockMinimum, out var stockMin))
                CurrentMedi.MinimumStock = stockMin;

            store.SetTemporaryMedi(CurrentMedi);
        }
    }
}
