using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using com.b_velop.OneMediPlan.Domain;
using com.b_velop.OneMediPlan.Meta.Interfaces;
using Ninject;
using Plugin.CurrentActivity;

namespace com.b_velop.OneMediPlan.Droid
{
    //You can specify additional application information in this attribute
    [Application]
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
        : base(handle, transer)
        {
            var app = new App();
            App.Initialize();
            App.Container.Bind<Action<Medi>>().ToMethod(context => MainApplication.SetNotification).InSingletonScope();
        }
        public static void SetNotification(Medi medi)
        {
            // TODO - Notification handling for Android
            var logger = App.Container.Get<ILogger>();
            logger.Log($"Notification for '{medi.Name}' has to be set.", typeof(MainApplication));
        }
        public override void OnCreate()
        {
            base.OnCreate();
            RegisterActivityLifecycleCallbacks(this);
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityStopped(Activity activity)
        {

        }
    }
}
