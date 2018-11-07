using UIKit;
using System;
using com.b_velop.OneMediPlan.Domain;

namespace com.b_velop.OneMediPlan.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
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
