using Foundation;
using OneMediPlan.Models;
using System;
using System.Collections.Generic;
using UIKit;
using System.Linq;

namespace OneMediPlan.iOS
{
    public partial class SetDependencyViewController : UIViewController
    {
        public Medi CurrentMedi { get; set; }
        public Medi SelectedMedi { get; set; }

        public IDataStore<Medi> DataStore => ServiceLocator. Instance.Get<IDataStore<Medi>>() ?? new MockDataStore();
        public SetDependencyViewController(IntPtr handle) : base(handle) { }

        async public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var model = new DependencyTypeDataModel();
            model.ValueChanged += (object sender, EventArgs e) =>
            {
                if (sender is DependencyTypeDataModel data)
                {
                    SelectedMedi = data.SelectedItem;
                }
            };
            var tmp = await DataStore.GetItemsAsync(true);
            var medis = tmp.ToList();
            foreach (var medi in medis)
                model.Medis.Add(medi);
            PickerDependency.Model = model;
            PickerDependency.Select(0, 0, true);
            SelectedMedi = medis[0];
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.DestinationViewController is SetIntervallViewController viewController)
            {
                CurrentMedi.DependsOn = SelectedMedi.Id;
                viewController.CurrentMedi = CurrentMedi;
            }
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