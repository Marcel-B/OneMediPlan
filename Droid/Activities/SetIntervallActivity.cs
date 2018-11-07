using System;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Widget;

using com.b_velop.OneMediPlan.Services;
using com.b_velop.OneMediPlan.ViewModels;
using Ninject;
using com.b_velop.OneMediPlan.Meta;
using com.b_velop.OneMediPlan.Domain.Enums;

namespace com.b_velop.OneMediPlan.Droid.Activities
{
    [Activity(Label = "SetIntervallActivity")]
    public class SetIntervallActivity : BaseActivity
    {
        /// <summary>
        /// Specify the layout to inflace
        /// </summary>
        protected override int LayoutResource
            => Resource.Layout.activitySetIntervallLayout;

        public NewMediViewModel ViewModel { get; set; }
        public FloatingActionButton Next { get; set; }
        public EditText Dosage { get; set; }
        public EditText Intervall { get; set; }
        public Spinner DependsOn { get; set; }
        public Spinner TimeType { get; set; }
        public TextView Units { get; set; }
        public TextView After { get; set; }
        public TextView Take { get; set; }
        public TextView Every { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ViewModel = App.Container.Get<NewMediViewModel>();
            ViewModel.CurrentViewType = NewMediViewModel.ViewType.Intervall;
            ViewModel.Intervall = string.Empty;
            ViewModel.Dosage = string.Empty;
        }

        public override void GetViews()
        {
            Next = FindViewById<FloatingActionButton>(Resource.Id.floatButtonNextIntervall);
            Dosage = FindViewById<EditText>(Resource.Id.editTextDosage);
            Intervall = FindViewById<EditText>(Resource.Id.editTextRawIntervall);
            DependsOn = FindViewById<Spinner>(Resource.Id.spinnerDependsOnMedi);
            TimeType = FindViewById<Spinner>(Resource.Id.spinnerIntervallTime);

            Take = FindViewById<TextView>(Resource.Id.textViewTake);
            After = FindViewById<TextView>(Resource.Id.textViewAfter);
            Units = FindViewById<TextView>(Resource.Id.textViewUnits);
            Every = FindViewById<TextView>(Resource.Id.textViewEvery);
        }

        public override void InitViews()
        {
            base.InitViews();
            var arraySpinnerTimeType = new[]{
                Strings.MINUTES,
                Strings.HOURS,
                Strings.DAYS,
                Strings.WEEKS,
                Strings.MONTHS
            };
            var adapterTimeType = new ArrayAdapter<String>(this,
                    Android.Resource.Layout.SimpleSpinnerItem, arraySpinnerTimeType);

            TimeType.Adapter = adapterTimeType;

            var arraySpinnerDependsOn = AppStore.Instance.User.Medis.Select(m => m.Name).ToArray();
            var adapter = new ArrayAdapter<String>(this,
                    Android.Resource.Layout.SimpleSpinnerItem, arraySpinnerDependsOn);

            DependsOn.Adapter = adapter;



            if (ViewModel.CurrentViewType == NewMediViewModel.ViewType.Intervall &&
                ViewModel.IntervallType != IntervallType.Depend)
            {
                DependsOn.Visibility = Android.Views.ViewStates.Invisible;
                After.Visibility = Android.Views.ViewStates.Invisible;
            }
            else
            {
                DependsOn.Visibility = Android.Views.ViewStates.Visible;
                After.Visibility = Android.Views.ViewStates.Visible;
            }

            Next.Enabled = false;
        }

        public override void Localize()
        {
            Units.Text = Strings.UNITS;
            After.Text = Strings.AFTER;
            Take.Text = Strings.TAKE;
            Every.Text = Strings.EVERY;
        }

        public override void SetEvents()
        {
            Dosage.TextChanged += Dosage_TextChanged;
            Intervall.TextChanged += Intervall_TextChanged;
            DependsOn.ItemSelected += DependsOn_ItemSelected;
            TimeType.ItemSelected += TimeType_ItemSelected;
            Next.Click += Next_Click;
        }

        public override void DestroyEvents()
        {
            Dosage.TextChanged -= Dosage_TextChanged;
            Intervall.TextChanged -= Intervall_TextChanged;
            DependsOn.ItemSelected -= DependsOn_ItemSelected;
            TimeType.ItemSelected -= TimeType_ItemSelected;
            Next.Click -= Next_Click;
        }

        void Dosage_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            ViewModel.Dosage = e.Text.ToString();
            SetButtonState();
        }

        void Intervall_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            ViewModel.Intervall = e.Text.ToString();
            SetButtonState();
        }

        void DependsOn_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            ViewModel.DependsOnIdx = e.Position;
            SetButtonState();
        }

        void TimeType_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            ViewModel.IntervallTime = (IntervallTime)e.Position;
            SetButtonState();
        }

        void Next_Click(object sender, EventArgs e)
            => StartActivity(typeof(SetStartTimeActivity));

        private void SetButtonState()
        {
            Next.Enabled = ViewModel.SaveNameCommand.CanExecute(null);
        }
    }
}
