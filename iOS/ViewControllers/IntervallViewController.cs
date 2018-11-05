using System;
using UIKit;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using Foundation;
using com.b_velop.OneMediPlan.Meta;
using com.b_velop.OneMediPlan.ViewModels;
using com.b_velop.OneMediPlan.Services;
using com.b_velop.OneMediPlan.Domain.Enums;

namespace com.b_velop.OneMediPlan.iOS
{
    public partial class IntervallViewController : UIViewController
    {


        public IntervallViewController(IntPtr handle) : base(handle)
        {
            ViewModel = App.Container.Get<NewMediViewModel>();
        }

        public NewMediViewModel ViewModel { get; set; }
        internal StringTypeDataModel MediModel { get; set; }
        internal StringTypeDataModel IntervallModel { get; set; }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is IntervallViewModel viewModel)
            {
                if (e.PropertyName.Equals(Strings.CURRENT_MEDI))
                {
                    var noParent = viewModel.CurrentMedi.DependsOn == Guid.Empty;
                    //LabelDependencyInfo.Hidden = noParent;
                }
                else if (e.PropertyName.Equals(NSBundle.MainBundle.GetLocalizedString(Strings.LABEL_TEXT)))
                {
                    //LabelDependencyInfo.Text = Strings.AFTER;
                    //$"{NSBundle.MainBundle.GetLocalizedString(Strings._AFTER)} '{viewModel.LabelText}'.";
                }
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Set if no debendence to other medi
            if (ViewModel.IntervallType != Domain.Enums.IntervallType.Depend)
            {
                PickerViewMedis.Hidden = true;
                LabelAfter.Hidden = true;
                DependsSpace.Constant = 0.0f;
            }

            LabelUnits.Text = ViewModel.Name;
            LabelAfter.Text = Strings.AFTER;
            LabelEvery.Text = Strings.EVERY;
            LabelTake.Text = Strings.TAKE;

            TextFieldDosage.Placeholder = Strings.DOSAGE;
            TextFieldIntervall.Placeholder = Strings.INTERVALL;

            ButtonNext.Hidden = true;

            Title = ViewModel.Title;

            MediModel = new StringTypeDataModel();
            var medis = AppStore.Instance.User.Medis;

            var mediNames = medis.Select(m => m.Name);

            IntervallModel = new StringTypeDataModel();
            var list = new List<string>
            {
                Strings.MINUTES,
                Strings.HOURS,
                Strings.DAYS,
                Strings.WEEKS
            };
            MediModel.Items.AddRange(mediNames);
            IntervallModel.Items.AddRange(list);

            PickerIntervallType.Model = IntervallModel;
            PickerViewMedis.Model = MediModel;

            TextFieldDosage.AllEditingEvents += TextFieldDosage_AllEditingEvents;
            TextFieldIntervall.AllEditingEvents += TextFieldIntervall_AllEditingEvents;
            MediModel.ValueChanged += MediModel_ValueChanged;
            IntervallModel.ValueChanged += IntervallModel_ValueChanged;

        }

        public override void ViewWillUnload()
        {
            base.ViewWillUnload();
            TextFieldDosage.AllEditingEvents -= TextFieldDosage_AllEditingEvents;
            TextFieldIntervall.AllEditingEvents -= TextFieldIntervall_AllEditingEvents;
            MediModel.ValueChanged -= MediModel_ValueChanged;
            IntervallModel.ValueChanged -= IntervallModel_ValueChanged;
        }

        void TextFieldDosage_AllEditingEvents(object sender, EventArgs e)
        {
            if (sender is UITextView dosage)
            {
                ViewModel.Dosage = dosage.Text;
            };
        }

        void TextFieldIntervall_AllEditingEvents(object sender, EventArgs e)
        {
            if (sender is UITextView intervall)
            {
                ViewModel.Intervall = intervall.Text;
            }
        }

        void MediModel_ValueChanged(object sender, EventArgs e)
        {
            if (sender is StringTypeDataModel mediModel)
            {
                ViewModel.DependsOnIdx = mediModel.SelectedIndex;
            }
        }

        void IntervallModel_ValueChanged(object sender, EventArgs e)
        {
            if (sender is StringTypeDataModel intervallTypeDataModel)
            {
                var idx = intervallTypeDataModel.SelectedIndex;
                ViewModel.IntervallTime = (IntervallTime)idx;
            }
        }


        internal class StringTypeDataModel : UIPickerViewModel
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

            public StringTypeDataModel()
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