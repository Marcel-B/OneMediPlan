
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Widget;
using com.b_velop.OneMediPlan.ViewModels;
using Ninject;
using System;

namespace com.b_velop.OneMediPlan.Droid.Activities
{
    [Activity(Label = "SetStartTimeActivity")]
    public class SetStartTimeActivity : BaseActivity
    {
        protected override int LayoutResource 
            => Resource.Layout.activitySetStartTimeLayout;

        public NewMediViewModel ViewModel { get; set; }
        public FloatingActionButton Save { get; set; }
        public TimePicker StartTime { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ViewModel = App.Container.Get<NewMediViewModel>();
        }
        public override void GetViews()
        {
            base.GetViews();
            Save = FindViewById<FloatingActionButton>(Resource.Id.floatButtonSaveMedi);
            StartTime = FindViewById<TimePicker>(Resource.Id.timePickerStartTime);
        }
        public override void Localize()
        {
            base.Localize();
        }
        public override void SetEvents()
        {
            base.SetEvents();
            Save.Click += Save_Click;
        }
        public override void InitViews()
        {
            base.InitViews();
            var now = DateTimeOffset.Now;
            StartTime.Hour = now.Hour;
            StartTime.Minute = now.Minute;
        }
        public override void DestroyEvents()
        {
            base.DestroyEvents();
            Save.Click -= Save_Click;
        }
        void Save_Click(object sender, System.EventArgs e)
        {
            ViewModel.SaveNameCommand.Execute(null);
            StartActivity(typeof(MainActivity));
        }
    }
}
