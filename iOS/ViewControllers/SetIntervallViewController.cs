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
        private double _intervall;
        private int _intervallFactor;

        public IDataStore<Medi> DataStore => ServiceLocator.Instance.Get<IDataStore<Medi>>() ?? new MockDataStore();

        IEnumerable<string> IntervallTypes =
            new[] { "Minute(n)", "Stunde(n)", "Tag(e)", "Woche(n)" };

        public SetIntervallViewController(IntPtr handle) : base(handle) { }

        async public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            CurrentMedi.IntervallInMinutes = 0;
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

            pickerModle.ValueChanged += (object sender, EventArgs e) =>
            {
                if (sender is IntervallTypeDataModel intervallTypeDataModel)
                {
                    var item = intervallTypeDataModel.SelectedItem;
                    var idx =intervallTypeDataModel.SelectedIndex;
                    switch(idx){
                        case 0: // Minuten
                            _intervallFactor = 1;
                            break;
                        case 1: // Stunden
                            _intervallFactor = 1 * 60;
                            break;
                        case 2: // Tage
                            _intervallFactor = 1 * 60 * 24;
                            break;
                        case 3: // Woche(n)
                            _intervallFactor = 1 * 60 * 24 * 7;
                            break;
                        case 4: // Monat(e)
                            _intervallFactor = 99;
                            break;
                        default:
                            _intervallFactor = 0;
                            break;
                    }
                }
            };
        }

        public override bool ShouldPerformSegue(string segueIdentifier, NSObject sender)
            => double.TryParse(LabelIntervallCount.Text, out _intervall);

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.DestinationViewController is SetDosageViewController setDosageViewController)
            {
                CurrentMedi.IntervallInMinutes = (int)(_intervall * _intervallFactor);
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