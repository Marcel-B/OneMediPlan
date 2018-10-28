using System;

using Android.App;
using Android.OS;
using Android.Runtime;

using Plugin.CurrentActivity;
using com.b_velop.OneMediPlan;
using com.b_velop.OneMediPlan.Domain;
using Newtonsoft.Json;
using System.IO;

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
			var medi = new Medi();
            medi.Id = Guid.NewGuid();
            medi.Name = "Furtz";

            string json = JsonConvert.SerializeObject(medi);
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var dir = Directory.GetFiles(path);
            string filePath = Path.Combine(path, "employee.txt");
            using (var file = File.Open(filePath, FileMode.Create, FileAccess.Write))
            using (var strm = new StreamWriter(file))
            {
                strm.Write(json);
            }
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
