using System;
using System.Collections.Generic;
using System.Linq;
using com.b_velop.OneMediPlan.Domain.Enums;
using com.b_velop.OneMediPlan.iOS.ViewControllers;
using com.b_velop.OneMediPlan.Meta;
using com.b_velop.OneMediPlan.Services;
using com.b_velop.OneMediPlan.ViewModels;
using Foundation;
using Ninject;
using UIKit;

namespace com.b_velop.OneMediPlan.iOS
{
    public partial class IntervallViewController : BaseViewController
    {
        public IntervallViewController(IntPtr handle) : base(handle)
        {
            ViewModel = App.Container.Get<NewMediViewModel>();
        }

        public NewMediViewModel ViewModel { get; set; }
        internal StringTypeDataModel MediModel { get; set; }
        internal StringTypeDataModel IntervallModel { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Reset ViewModel Values
            ViewModel.Dosage = string.Empty;
            ViewModel.Intervall = string.Empty;
            ViewModel.CurrentViewType = NewMediViewModel.ViewType.Intervall;

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
            //IEnumerable<string> mediNames = new string[0];

            //if(medis != null)
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

        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            ButtonNext.TouchUpInside += ButtonNext_TouchUpInside;
            TextFieldDosage.AllEditingEvents += TextFieldDosage_AllEditingEvents;
            TextFieldIntervall.AllEditingEvents += TextFieldIntervall_AllEditingEvents;
            MediModel.ValueChanged += MediModel_ValueChanged;
            IntervallModel.ValueChanged += IntervallModel_ValueChanged;
            if (ViewModel.IntervallType == IntervallType.Depend)
            {
                ViewModel.DependsOnIdx = 0;
                PickerViewMedis.Select(0, 0, true);
            }
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            ButtonNext.TouchUpInside -= ButtonNext_TouchUpInside;
            TextFieldDosage.AllEditingEvents -= TextFieldDosage_AllEditingEvents;
            TextFieldIntervall.AllEditingEvents -= TextFieldIntervall_AllEditingEvents;
            MediModel.ValueChanged -= MediModel_ValueChanged;
            IntervallModel.ValueChanged -= IntervallModel_ValueChanged;
        }

        void ButtonNext_TouchUpInside(object sender, EventArgs e)
        {
            var destination = ViewModel.IntervallType == IntervallType.Intervall 
                                       ? "FromIntervallToStarttime" 
                                       : "FromIntervallToSave";
            PerformSegue(destination, this);
        }

        void TextFieldDosage_AllEditingEvents(object sender, EventArgs e)
        {
            if (sender is UITextField dosage)
            {
                ViewModel.Dosage = dosage.Text;
                ButtonNext.Hidden = !ViewModel.SaveNameCommand.CanExecute(null);
            }
        }

        void TextFieldIntervall_AllEditingEvents(object sender, EventArgs e)
        {
            if (sender is UITextField intervall)
            {
                ViewModel.Intervall = intervall.Text;
                ButtonNext.Hidden = !ViewModel.SaveNameCommand.CanExecute(null);
            }
        }

        void MediModel_ValueChanged(object sender, EventArgs e)
        {
            if (sender is StringTypeDataModel mediModel)
            {
                ViewModel.DependsOnIdx = mediModel.SelectedIndex;
                ButtonNext.Hidden = !ViewModel.SaveNameCommand.CanExecute(null);
            }
        }

        void IntervallModel_ValueChanged(object sender, EventArgs e)
        {
            if (sender is StringTypeDataModel intervallTypeDataModel)
            {
                var idx = (int)intervallTypeDataModel.SelectedIndex; 
                ViewModel.IntervallTime = (IntervallTime)idx;
                ButtonNext.Hidden = !ViewModel.SaveNameCommand.CanExecute(null);
            }
        }

        public override bool ShouldPerformSegue(string segueIdentifier, NSObject sender)
            => ViewModel.IntervallType == IntervallType.Intervall;

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
