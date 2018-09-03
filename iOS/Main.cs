using UIKit;
using System;
using OneMediPlan.Models;
using OneMediPlan.Helpers;
using Foundation;

namespace OneMediPlan.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            App.Initialize();

            // Special Configuration for iOS App
            ConfigureContainer();
            SetLanguageConstants();

            UIApplication.Main(args, null, "AppDelegate");
        }

        private static void ConfigureContainer()
        {
            App.Container.Bind<MedisDataSource>().ToSelf();
            App.Container.Bind<MyDateTableViewSource>().ToSelf();
            App.Container.Bind<Action<Medi>>().ToMethod(context => AppDelegate.SetNotification).InSingletonScope();
        }

        private static void SetLanguageConstants()
        {
            MediExtensions.Today = NSBundle.MainBundle.GetLocalizedString(Strings.TODAY);
        }
    }
}
