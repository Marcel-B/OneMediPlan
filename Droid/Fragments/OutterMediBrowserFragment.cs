using System;
using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;

using com.b_velop.OneMediPlan;
using com.b_velop.OneMediPlan.Droid;
using com.b_velop.OneMediPlan.ViewModels;
using Ninject;

namespace OneMediPlan.Droid.Fragments
{
    public class OutterMediBrowserFragment : Android.Support.V4.App.Fragment
    {
        public static OutterMediBrowserFragment NewInstance() =>
            new OutterMediBrowserFragment { Arguments = new Bundle() };

        //public SwipeRefreshLayout Refresher { get; set; }
        public ProgressBar Progress { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override void OnResume()
        {
            base.OnResume();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragmentMediBrowseLayout, container, false);

            //Refresher = view.FindViewById<SwipeRefreshLayout>(Resource.Id.refresher);
            //Refresher.SetColorSchemeColors(Resource.Color.accent);

            Progress = view.FindViewById<ProgressBar>(Resource.Id.progressbar_loading);
            Progress.Visibility = ViewStates.Gone;

            return view;
        }

        public override void OnStart()
        {
            base.OnStart();
            var transaction = FragmentManager.BeginTransaction();
            transaction.Add(Resource.Id.contentMedisFrame, MediBrowseFragment.NewInstance());
            transaction.Commit();
            //Refresher.Refresh += Refresher_Refresh;
        }
        public override void OnStop()
        {
            base.OnStop();
        }
    }
}
