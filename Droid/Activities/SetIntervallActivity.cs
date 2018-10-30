using System;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Widget;
using com.b_velop.OneMediPlan.Services;
using com.b_velop.OneMediPlan.ViewModels;
using Ninject;

namespace com.b_velop.OneMediPlan.Droid.Activities
{
    [Activity(Label = "SetIntervallActivity")]
    public class SetIntervallActivity : BaseActivity
    {
        /// <summary>
        /// Specify the layout to inflace
        /// </summary>
        protected override int LayoutResource => Resource.Layout.activitySetIntervallLayout;
        public NewMediViewModel ViewModel { get; set; }
        public FloatingActionButton Next { get; set; }
        public EditText Dosage { get; set; }
        public EditText Intervall { get; set; }
        public Spinner DependsOn { get; set; }
        public Spinner TimeType { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ViewModel = App.Container.Get<NewMediViewModel>();
            Next = FindViewById<FloatingActionButton>(Resource.Id.floatButtonNextIntervall);
            Dosage = FindViewById<EditText>(Resource.Id.editTextDosage);
            Intervall = FindViewById<EditText>(Resource.Id.editTextRawIntervall);
            DependsOn = FindViewById<Spinner>(Resource.Id.spinnerDependsOnMedi);
            TimeType = FindViewById<Spinner>(Resource.Id.spinnerIntervallTime);

            var arraySpinnerTimeType = new[]{
                "Minuten",
                "Stunden",
                "Tage",
                "Wochen",
                "Monate"
            };
            var adapterTimeType = new ArrayAdapter<String>(this,
                    Android.Resource.Layout.SimpleSpinnerItem, arraySpinnerTimeType);

            TimeType.Adapter = adapterTimeType;

            var arraySpinnerDependsOn = AppStore.Instance.User.Medis.Select(m => m.Name).ToArray();
            var adapter = new ArrayAdapter<String>(this,
                    Android.Resource.Layout.SimpleSpinnerItem, arraySpinnerDependsOn);

            DependsOn.Adapter = adapter;

            Next.Click += (sender, e) =>
            {
                StartActivity(typeof(SetStartTimeActivity));
            };
        }
    }
}
