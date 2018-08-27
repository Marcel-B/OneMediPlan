using OneMediPlan.Models;
using Ninject;
using OneMediPlan.ViewModels;
using OneMediPlan.Helpers;
using System;
using OneMediPlan.Services;
using System.Threading.Tasks;
using System.Linq;
using Realms;

namespace OneMediPlan
{
    public class Settings
    {
        public static Hour DefaultHour = new Hour(12);
        public static Minute DefaultMinute = new Minute(15);

        public static DateTimeOffset GetStdTime()
        {
            var n = DateTimeOffset.Now;
            return new DateTimeOffset(n.Year, n.Month, n.Day, DefaultHour.Value, DefaultMinute.Value, 0, n.Offset);
        }

        public Settings()
        {
            DefaultHour = new Hour(12);
            DefaultMinute = new Minute(15);
        }
    }

 
    public class App
    {
        public static StandardKernel Container { get; set; }
        public static bool UseMockDataStore = false;
        public static string BackendUrl = "http://localhost:5000";
        public static RealmConfiguration RealmConf = new RealmConfiguration("default.realm");
        public const int SCHEMA_VERSION = 2;

        public static Action<Medi> SetNotification { get; set; }

        public static void Initialize()
        {
            RealmConf.SchemaVersion = SCHEMA_VERSION;

            Container = new StandardKernel();

            Container.Bind<MediViewModel>().ToSelf().InSingletonScope();
            Container.Bind<MediDetailViewModel>().ToSelf().InSingletonScope();
            Container.Bind<NewMediViewModel>().ToSelf().InSingletonScope();
            Container.Bind<MediStockViewModel>().ToSelf().InSingletonScope();
            Container.Bind<SetIntervallTypeViewModel>().ToSelf().InSingletonScope();
            Container.Bind<SetIntervallViewModel>().ToSelf().InSingletonScope();
            Container.Bind<SetDependencyViewModel>().ToSelf().InSingletonScope();
            Container.Bind<SetDosageViewModel>().ToSelf().InSingletonScope();
            Container.Bind<WeekdayViewModel>().ToSelf().InSingletonScope(); 
            Container.Bind<SetDailyViewModel>().ToSelf().InSingletonScope();
            Container.Bind<SetStartViewModel>().ToSelf().InSingletonScope();
            Container.Bind<SaveMediViewModel>().ToSelf().InSingletonScope();
            Container.Bind<Medi>().ToSelf().InSingletonScope(); // Temp Medi


            Container.Bind<ISomeLogic>().To<SomeLogic>();
            if (UseMockDataStore)
            {
                Container.Bind<IDataStore<Medi>>().To<MockDataStore>().InSingletonScope();
                Container.Bind<IDataStore<Weekdays>>().To<WeekdayDataStoreMock>().InSingletonScope();
                Container.Bind<IDataStore<MediSettings>>().To<AppSettingsDataStore>().InSingletonScope();
            }
            else
            {
                Container.Bind<IDataStore<Medi>>().To<MediDataStore>().InSingletonScope();
                Container.Bind<IDataStore<Weekdays>>().ToSelf().InSingletonScope();
                Container.Bind<IDataStore<MediSettings>>().To<AppSettingsDataStore>().InSingletonScope();
            }

            Task.Run(() => SetSettings());

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
