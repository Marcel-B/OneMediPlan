
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

        public SwipeRefreshLayout Refresher { get; set; }
        public ProgressBar Progress { get; set; }
        public MainViewModel ViewModel { get; set; }

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
            // Use this to return your custom view for this Fragment
            ViewModel = App.Container.Get<MainViewModel>();
            var view = inflater.Inflate(Resource.Layout.fragmentMediBrowseLayout, container, false);

            Refresher = view.FindViewById<SwipeRefreshLayout>(Resource.Id.refresher);
            Refresher.SetColorSchemeColors(Resource.Color.accent);

            Progress = view.FindViewById<ProgressBar>(Resource.Id.progressbar_loading);
            Progress.Visibility = ViewStates.Gone;

            return view;
        }

        public override void OnStart()
        {
            base.OnStart();
            var transaction = FragmentManager.BeginTransaction();
            transaction.Add(Resource.Id.contentMedisFrame, new MediBrowseFragment());
            transaction.Commit();
            Refresher.Refresh += Refresher_Refresh;
        }
        public override void OnStop()
        {
            base.OnStop();
            Refresher.Refresh -= Refresher_Refresh;
        }
        void Refresher_Refresh(object sender, EventArgs e)
        {
            ViewModel.LoadItemsCommand.Execute(null);
            Refresher.Refreshing = false;
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            Progress = View.FindViewById<ProgressBar>(Resource.Id.progressbar_loading);
            Progress.Visibility = ViewStates.Gone;
            Refresher = View.FindViewById<SwipeRefreshLayout>(Resource.Id.refresher);
            Refresher.SetColorSchemeColors(Resource.Color.accent);
        }
    }
}
