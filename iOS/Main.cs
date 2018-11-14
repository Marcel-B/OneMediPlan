using UIKit;
using System;
using com.b_velop.OneMediPlan.Domain;
using Redux;
using com.b_velop.OneMediPlan.Services;

namespace com.b_velop.OneMediPlan.iOS
{
    public class Application
    {
        static void Main(string[] args)
        {
            App.Initialize();
            // Special Configuration for iOS App
            ConfigureContainer();
            UIApplication.Main(args, null, "AppDelegate");
        }

        private static void ConfigureContainer()
        {
            App.Container.Bind<MedisDataSource>().ToSelf();
            App.Container.Bind<MyDateTableViewSource>().ToSelf();
            App.Container.Bind<Action<Medi>>().ToMethod(context => AppDelegate.SetNotification).InSingletonScope();
        }
    }
}
