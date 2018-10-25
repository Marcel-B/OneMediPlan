using System;
using UIKit;
using OneMediPlan.ViewModels;
using Ninject;
using OneMediPlan.Helpers;
using Foundation;

namespace com.b_velop.OneMediPlan.iOS
{
    public partial class StockViewController : UIViewController
    {
        partial void ButtonNext_TouchUpInside(UIButton sender)
        => ViewModel.SaveStockCommand.Execute(null);

        StockViewModel ViewModel { get; set; }

        partial void TextViewStockMinimumChanged(UITextField sender)
        {
            ViewModel.StockMinimum = sender.Text;
            var arr = new[] { sender.Text, LabelCurrentStock.Text };
            var result = ViewModel.SaveStockCommand.CanExecute(arr);
            ButtonNext.Hidden = !result;
        }

        partial void TextViewStockChanged(UITextField sender)
        {
            ViewModel.Stock = sender.Text;
            var arr = new[] { LabelMinimumStock.Text, sender.Text };
            var result = ViewModel.SaveStockCommand.CanExecute(arr);
            ButtonNext.Hidden = !result;
        }

        public StockViewController(IntPtr handle) : base(handle)
        {
            ViewModel = App.Container.Get<StockViewModel>();
            ViewModel.PropertyChanged += (sender, e) =>
            {
                if (sender is StockViewModel viewModel)
                {
                    if (e.PropertyName.Equals(Strings.CURRENT_MEDI))
                    {
                        var stock = viewModel.CurrentMedi.Stock;
                        var minStock = viewModel.CurrentMedi.MinimumStock;
                        if (stock > 0)
                            LabelCurrentStock.Text = stock.ToString();
                        if (minStock > 0)
                            LabelMinimumStock.Text = minStock.ToString();
                    }
                    else if (e.PropertyName.Equals(Strings.STOCK_MINIMUM))
                    {
                        LabelMinimumStock.Text = viewModel.StockMinimum;
                    }
                }
            };
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = NSBundle.MainBundle.GetLocalizedString(Strings.STOCK);
            ViewModel.Init();
            ButtonNext.Hidden = true;
        }
    }
}