using System.Windows.Input;
using com.b_velop.OneMediPlan.Helpers;
using com.b_velop.OneMediPlan.Meta;
using com.b_velop.OneMediPlan.Models;
using Ninject;

namespace com.b_velop.OneMediPlan.ViewModels
{
    public class StockViewModel : BaseViewModel
    {
        public ICommand SaveStockCommand { get; }

        AppMedi _currentMedi;
        string _stock;
        string _stockMinimum;

        public AppMedi CurrentMedi
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
            //var store = App.Container.Get<IMediDataStore>();
            //CurrentMedi = store.GetTemporaryMedi();
            //if (CurrentMedi.MinimumStock > 0)
                //StockMinimum = CurrentMedi.MinimumStock.ToString();
        }

        private bool SaveStockCanExecute(object obj)
            => obj is string[] arr && 
                       !string.IsNullOrWhiteSpace(arr[0]) &&
                       !string.IsNullOrWhiteSpace(arr[1]) &&
                       double.TryParse(arr[0], out var valueOne) &&
                       double.TryParse(arr[1], out var ValueTwo);

        private void SaveStockExecute(object obj)
        {
            //var store = App.Container.Get<IMediDataStore>();

            //if (double.TryParse(Stock, out var stock))
            //    CurrentMedi.Stock = stock;

            //if (double.TryParse(StockMinimum, out var stockMin))
            //    CurrentMedi.MinimumStock = stockMin;

            //store.SetTemporaryMedi(CurrentMedi);
        }
    }
}
