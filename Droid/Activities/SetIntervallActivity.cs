
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using com.b_velop.OneMediPlan;
using Ninject;
using com.b_velop.OneMediPlan.ViewModels;
using com.b_velop.OneMediPlan.Droid;
using Android.Support.Design.Widget;

namespace OneMediPlan.Droid.Activities
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


        public SetIntervallActivity()
        {

        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ViewModel = App.Container.Get<NewMediViewModel>();
            Next = FindViewById<FloatingActionButton>(Resource.Id.floatButtonNextIntervall);
            Next.Click += (sender, e) =>
            {
                Finish();
            };
        }
    }
}
