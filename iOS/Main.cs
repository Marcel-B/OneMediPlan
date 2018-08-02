using UIKit;
using Ninject;
using System;
using OneMediPlan.Models;
using OneMediPlan.Helpers;

namespace OneMediPlan.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            App.Initialize();
            App.Container.Bind<MedisDataSource>().ToSelf();
            //App.Container.Bind<ISomeLogic>().To<SomeLogic>().;
            App.Container.Bind<Action<Medi>>().ToMethod(context => AppDelegate.SetNotification).InSingletonScope();
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
