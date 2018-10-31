using System;
using UIKit;
using Ninject;

using com.b_velop.OneMediPlan.ViewModels;
using com.b_velop.OneMediPlan.Meta;

namespace com.b_velop.OneMediPlan.iOS
{
    public partial class NewMediViewController : UIViewController
    {
        public NewMediViewModel ViewModel { get; set; }

        public NewMediViewController(IntPtr handle) : base(handle)
        {
            ViewModel = App.Container.Get<NewMediViewModel>();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = Strings.NEW_MEDI;
            TextFieldName.Placeholder = Strings.NAME;
            TextFieldStock.Placeholder = Strings.STOCK;
            TextFieldMinimumStock.Placeholder = Strings.STOCK_MINIMUM;

            TextFieldName.AllEditingEvents += TextFieldName_AllEditingEvents;
            TextFieldStock.AllEditingEvents += TextFieldStock_AllEditingEvents;
            TextFieldMinimumStock.AllEditingEvents += TextFieldMinimumStock_AllEditingEvents;
        }

        public override void ViewDidUnload()
        {
            base.ViewDidUnload();
            TextFieldName.AllEditingEvents -= TextFieldName_AllEditingEvents;
            TextFieldStock.AllEditingEvents -= TextFieldStock_AllEditingEvents;
            TextFieldMinimumStock.AllEditingEvents -= TextFieldMinimumStock_AllEditingEvents;
        }

        void TextFieldName_AllEditingEvents(object sender, EventArgs e)
        {
            if (sender is UITextField textField)
            {
                ViewModel.Name = textField.Text;
                ButtonNext.Hidden = !ViewModel.SaveNameCommand.CanExecute(null);
            }
        }

        void TextFieldStock_AllEditingEvents(object sender, EventArgs e)
        {
            if (sender is UITextField textField)
            {
                ViewModel.Stock = textField.Text;
                ButtonNext.Hidden = !ViewModel.SaveNameCommand.CanExecute(null);
            }
        }

        void TextFieldMinimumStock_AllEditingEvents(object sender, EventArgs e)
        {
            if (sender is UITextField textField)
            {
                ViewModel.StockMinimum = textField.Text;
                ButtonNext.Hidden = !ViewModel.SaveNameCommand.CanExecute(null);
            }
        }
    }
}
