using UIKit;
using System;
using OneMediPlan.Models;

namespace OneMediPlan.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            App.Initialize();
            App.Container.Bind<MedisDataSource>().ToSelf();
            App.Container.Bind<Action<Medi>>().ToMethod(context => AppDelegate.SetNotification).InSingletonScope();
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
