﻿using System;
using com.b_velop.OneMediPlan.iOS.ViewControllers;
using com.b_velop.OneMediPlan.Meta;
using com.b_velop.OneMediPlan.ViewModels;
using Ninject;
using UIKit;
using static com.b_velop.OneMediPlan.ViewModels.NewMediViewModel;

namespace com.b_velop.OneMediPlan.iOS
{
    public partial class NewMediViewController : BaseViewController
    {
        public NewMediViewModel ViewModel { get; set; }

        public NewMediViewController(IntPtr handle) : base(handle)
        {
            ViewModel = App.Container.Get<NewMediViewModel>();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ViewModel.CurrentViewType = ViewType.NameAndStock;
            Title = Strings.NEW_MEDI;
            TextFieldName.Placeholder = Strings.NAME;
            TextFieldStock.Placeholder = Strings.STOCK;
            TextFieldMinimumStock.Placeholder = Strings.STOCK_MINIMUM;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            TextFieldName.AllEditingEvents += TextFieldName_AllEditingEvents;
            TextFieldStock.AllEditingEvents += TextFieldStock_AllEditingEvents;
            TextFieldMinimumStock.AllEditingEvents += TextFieldMinimumStock_AllEditingEvents;
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
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
