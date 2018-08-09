using Foundation;
using OneMediPlan.Models;
using System;
using System.Collections.Generic;
using UIKit;
using System.Linq;
using Ninject;
using OneMediPlan.ViewModels;

namespace OneMediPlan.iOS
{
    public partial class SetDependencyViewController : UIViewController
    {
        partial void UIButton35489_TouchUpInside(UIButton sender)
            => ViewModel.NextCommand.Execute(null);

        SetDependencyViewModel ViewModel { get; set; }

        public SetDependencyViewController(IntPtr handle) : base(handle)
        {
            ViewModel = App.Container.Get<SetDependencyViewModel>();
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is SetDependencyViewModel viewModel)
            {
                if (e.PropertyName.Equals("CurrentMedi"))
                {

                }
                else if (e.PropertyName.Equals("Medis"))
                {
                    var model = new DependencyTypeDataModel();
                    model.ValueChanged -= Model_ValueChanged;
                    model.ValueChanged += Model_ValueChanged;
                    model.Medis.AddRange(viewModel.Medis);
                    PickerDependency.Select(0, 0, true);
                    PickerDependency.Model = model;
                }
            }
        }

        private void Model_ValueChanged(object sender, EventArgs e)
        {
            if (sender is DependencyTypeDataModel data)
                ViewModel.ParentMedi = data.SelectedItem;
        }

        async public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            await ViewModel.Init();
            Title = ViewModel.Title;
        }

        internal class DependencyTypeDataModel : UIPickerViewModel
        {
            public event EventHandler<EventArgs> ValueChanged;

            /// <summary>
            /// The items to show up in the picker
            /// </summary>
            public List<Medi> Medis { get; private set; }

            /// <summary>
            /// The current selected item
            /// </summary>
            public Medi SelectedItem
            {
                get => Medis[selectedIndex];
            }

            int selectedIndex = 0;

            public DependencyTypeDataModel()
            {
                Medis = new List<Medi>();
            }

            /// <summary>
            /// Called by the picker to determine how many rows are in a given spinner item
            /// </summary>
            public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
                => Medis.Count;


            /// <summary>
            /// called by the picker to get the text for a particular row in a particular
            /// spinner item
            /// </summary>
            public override string GetTitle(UIPickerView pickerView, nint row, nint component)
                => Medis[(int)row].Name;

            /// <summary>
            /// called by the picker to get the number of spinner items
            /// </summary>
            public override nint GetComponentCount(UIPickerView pickerView)
                => 1;

            /// <summary>
            /// called when a row is selected in the spinner
            /// </summary>
            public override void Selected(UIPickerView pickerView, nint row, nint component)
            {
                selectedIndex = (int)row;
                ValueChanged?.Invoke(this, new EventArgs());
            }
        }
    }
}
