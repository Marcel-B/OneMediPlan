using OneMediPlan.Models;
using Ninject;
using OneMediPlan.ViewModels;
using OneMediPlan.Helpers;
using System;

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
        public static bool UseMockDataStore = true;
        public static string BackendUrl = "http://localhost:5000";

        public static void Initialize()
        {
            Container = new Ninject.StandardKernel();
            Container.Bind<MediViewModel>().ToSelf().InSingletonScope();
            Container.Bind<MediDetailViewModel>().ToSelf().InSingletonScope();
            Container.Bind<NewMediViewModel>().ToSelf().InSingletonScope();
            Container.Bind<ISomeLogic>().To<SomeLogic>();
            if (UseMockDataStore)
            {
                Container.Bind<IDataStore<Medi>>().To<MockDataStore>().InSingletonScope();
                Container.Bind<IDataStore<Weekdays>>().To<WeekdayDataStoreMock>().InSingletonScope();
            }
            else
            {
                Container.Bind<IDataStore<Medi>>().To<CloudDataStore>().InSingletonScope();
                Container.Bind<IDataStore<Weekdays>>().ToSelf().InSingletonScope();
            }
        }
    }
}
