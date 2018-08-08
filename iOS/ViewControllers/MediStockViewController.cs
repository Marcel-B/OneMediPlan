using Foundation;
using System;
using UIKit;
using OneMediPlan.Models;
using OneMediPlan.ViewModels;
using Ninject;

namespace OneMediPlan.iOS
{
    public partial class MediStockViewController : UIViewController
    {
        private MediStockViewModel _viewModel;

        partial void TextViewStockMinimumChanged(UITextField sender)
        {
            _viewModel.StockMinimum = sender.Text;
            var arr = new[] { sender.Text, LabelCurrentStock.Text };
            var result = _viewModel.SaveStockCommand.CanExecute(arr);
            ButtonNext.Hidden = !result;
        }

        partial void TextViewStockChanged(UITextField sender)
        {
            _viewModel.Stock = sender.Text;
            var arr = new[] { LabelMinimumStock.Text, sender.Text };
            var result = _viewModel.SaveStockCommand.CanExecute(arr);
            ButtonNext.Hidden = !result;
        }

        public MediStockViewController(IntPtr handle) : base(handle)
        {
            _viewModel = App.Container.Get<MediStockViewModel>();
            _viewModel.PropertyChanged += (sender, e) =>
            {
                if (sender is MediStockViewModel viewModel)
                {
                    if (e.PropertyName.Equals("Medi"))
                    {
                        var stock = viewModel.Medi.Stock;
                        var minStock = viewModel.Medi.MinimumStock;
                        if (stock > 0)
                            LabelCurrentStock.Text = stock.ToString();
                        if (minStock > 0)
                            LabelMinimumStock.Text = minStock.ToString();
                    }
                }
            };
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _viewModel.Init();
            ButtonNext.Hidden = true;
        }
    }
}