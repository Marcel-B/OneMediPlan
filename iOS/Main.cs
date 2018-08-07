using UIKit;
using Ninject;
using System;
using OneMediPlan.Models;
using OneMediPlan.Helpers;
using System.Threading.Tasks;
using System.Linq;
using AudioToolbox;

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

            Task.Run(() => SetSettings());

            UIApplication.Main(args, null, "AppDelegate");
        }

        public static async Task SetSettings()
        {
            var ct = App.Container.Get<IDataStore<MediSettings>>();
            var ffo = await ct.GetItemsAsync();
            if (ffo.ToList().Count <= 0)
            {
                var medSett = new MediSettings();
                medSett.Hour = 12;
                medSett.Minute = 15;
                medSett.Id = Guid.NewGuid().ToString();
                await ct.AddItemAsync(medSett);
            }
        }
    }
}
