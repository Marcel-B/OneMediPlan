
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
using com.b_velop.OneMediPlan.ViewModels;
using Ninject;

namespace com.b_velop.OneMediPlan.Droid.Activities
{
    [Activity(Label = "SetStartTimeActivity")]
    public class SetStartTimeActivity : BaseActivity
    {
        protected override int LayoutResource => Resource.Layout.activitySetStartTimeLayout;
        public NewMediViewModel ViewModel { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ViewModel = App.Container.Get<NewMediViewModel>();
            // Create your application here
        }
    }
}
