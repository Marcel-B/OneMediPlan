using System;
using UIKit;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using Foundation;
using Ninject.Parameters;
using com.b_velop.OneMediPlan.Meta;
using com.b_velop.OneMediPlan.ViewModels;

namespace com.b_velop.OneMediPlan.iOS
{
    public partial class IntervallViewController : UIViewController
    {
        public IntervallViewController(IntPtr handle) : base(handle)
        {
            ViewModel = App.Container.Get<NewMediViewModel>();
        }

        public NewMediViewModel ViewModel { get; set; }


        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is IntervallViewModel viewModel)
            {
                if (e.PropertyName.Equals(Strings.CURRENT_MEDI))
                {
                    var noParent = viewModel.CurrentMedi.DependsOn == Guid.Empty;
                    LabelDependencyInfo.Hidden = noParent;
                }
                else if (e.PropertyName.Equals(NSBundle.MainBundle.GetLocalizedString(Strings.LABEL_TEXT)))
                {
                    LabelDependencyInfo.Text = Strings.AFTER;
                        //$"{NSBundle.MainBundle.GetLocalizedString(Strings._AFTER)} '{viewModel.LabelText}'.";
                }
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ButtonNext.Hidden = true;

            Title = ViewModel.Title;

            var pickerModle = new IntervallTypeDataModel();
            var list = new List<string>
            {
                Strings.MINUTES,
                Strings.HOURS,
                Strings.DAYS,
                Strings.WEEKS
            };
            pickerModle.Items.AddRange(list);
            PickerIntervallType.Model = pickerModle;

            pickerModle.ValueChanged += (object sender, EventArgs e) =>
            {
                if (sender is IntervallTypeDataModel intervallTypeDataModel)
                {
                    var idx = intervallTypeDataModel.SelectedIndex;
                    //ViewModel.IntervallTime = (IntervallTime)idx;
                }
            };
        }

        internal class IntervallTypeDataModel : UIPickerViewModel
        {
            public event EventHandler<EventArgs> ValueChanged;

            /// <summary>
            /// The items to show up in the picker
            /// </summary>
            public List<string> Items { get; private set; }

            /// <summary>
            /// The current selected item
            /// </summary>
            public string SelectedItem
            {
                get => Items[selectedIndex];
            }

            int selectedIndex = 0;
            public int SelectedIndex { get => selectedIndex; }

            public IntervallTypeDataModel()
            {
                Items = new List<string>();
            }

            /// <summary>
            /// Called by the picker to determine how many rows are in a given spinner item
            /// </summary>
            public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
                => Items.Count;


            /// <summary>
            /// called by the picker to get the text for a particular row in a particular
            /// spinner item
            /// </summary>
            public override string GetTitle(UIPickerView pickerView, nint row, nint component)
                => Items[(int)row];

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