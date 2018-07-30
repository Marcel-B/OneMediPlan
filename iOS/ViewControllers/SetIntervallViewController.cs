using System;
using UIKit;
using System.Collections.Generic;
using System.Linq;
using OneMediPlan.Models;
using Foundation;

namespace OneMediPlan.iOS
{
    public partial class SetIntervallViewController : UIViewController
    {
        public Medi CurrentMedi { get; set; }

        public IDataStore<Medi> DataStore => ServiceLocator.Instance.Get<IDataStore<Medi>>() ?? new MockDataStore();


        IEnumerable<string> IntervallTypes =
            new[] { "Minute(n)", "Stunde(n)", "Tag(e)", "Woche(n)" };

        public SetIntervallViewController(IntPtr handle) : base(handle) { }

        async public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var pickerModle = new IntervallTypeDataModel();
            pickerModle.Items.AddRange(IntervallTypes.ToList());
            PickerIntervallType.Model = pickerModle;
            if (CurrentMedi.DependsOn != Guid.Empty)
            {
                var m = await DataStore.GetItemAsync(CurrentMedi.DependsOn);
                LabelDependencyInfo.Hidden = false;
                LabelDependencyInfo.Text = $"nach {m.Name}.";
            }
            else
            {
                LabelDependencyInfo.Hidden = true;
            }
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.DestinationViewController is SetDosageViewController setDosageViewController)
            {
                setDosageViewController.CurrentMedi = CurrentMedi;
            }
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