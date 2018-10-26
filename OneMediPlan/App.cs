using Ninject;
using System;
using System.Threading.Tasks;

using com.b_velop.OneMediPlan.Models;
using com.b_velop.OneMediPlan.ViewModels;
using com.b_velop.OneMediPlan.Helpers;
using com.b_velop.OneMediPlan.Services;
using System.Collections.Generic;
using System.Linq;
using com.b_velop.OneMediPlan.Domain;
using com.b_velop.OneMediPlan.Domain.MockStores;
using com.b_velop.OneMediPlan.Domain.Services;
using com.b_velop.OneMediPlan.Domain.Stores;
using com.b_velop.OneMediPlan.Meta.Interfaces;

namespace com.b_velop.OneMediPlan
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
        public static string URL = "https://marcelbenders.de";
        public static StandardKernel Container { get; set; }
        public static bool UseMockDataStore = true;
        public static Action<Medi> SetNotification { get; set; }

        public static void Initialize()
        {
            Container = new StandardKernel();
            Container.Bind<ILogger>().To<AppLogger>();
            Container.Bind<MainViewModel>().ToSelf().InSingletonScope();
            Container.Bind<AppSettingsViewModel>().ToSelf().InSingletonScope();
            Container.Bind<MediDetailViewModel>().ToSelf().InSingletonScope();
            Container.Bind<NewMediViewModel>().ToSelf().InSingletonScope();
            Container.Bind<StockViewModel>().ToSelf().InSingletonScope();
            Container.Bind<IntervallTypeViewModel>().ToSelf().InSingletonScope();
            Container.Bind<IntervallViewModel>().ToSelf().InSingletonScope();
            Container.Bind<DependencyViewModel>().ToSelf().InSingletonScope();
            Container.Bind<DosageViewModel>().ToSelf().InSingletonScope();
            Container.Bind<WeekdayViewModel>().ToSelf().InSingletonScope();
            Container.Bind<DailyViewModel>().ToSelf().InSingletonScope();
            Container.Bind<StartViewModel>().ToSelf().InSingletonScope();
            Container.Bind<SaveMediViewModel>().ToSelf().InSingletonScope();
            Container.Bind<Medi>().ToSelf().InSingletonScope(); // Temp Medi
            Container.Bind<ISomeLogic>().To<SomeLogic>();

            if (UseMockDataStore)
            {
                Container.Bind<IDataStore<Medi>>().To<MediDataMock>();
                Container.Bind<IDataStore<Weekdays>>().To<WeekdaysDataMock>();
                Container.Bind<IDataStore<AppSettings>>().To<AppSettingsDataMock>();
                Container.Bind<IDataStore<DailyAppointment>>().To<DailyAppointmentDataMock>();
            }
            else
            {
                Container.Bind<IDataStore<Medi>>().To<MediDataStore>().WithConstructorArgument("backendUrl", App.URL);
                Container.Bind<IDataStore<Weekdays>>().To<WeekdaysDataStore>().WithConstructorArgument("backendUrl", App.URL);
                Container.Bind<IDataStore<AppSettings>>().To<AppSettingsDataStore>().WithConstructorArgument("backendUrl", App.URL);
                Container.Bind<IDataStore<DailyAppointment>>().To<DailyAppointmentDataStore>().WithConstructorArgument("backendUrl", App.URL);
            }
            AppStore.Instance.User = new User
            {
                Id = MediDataMock.USER_ID,
                Created = DateTimeOffset.Now,
                Medis = new List<Medi>()
            };

            Task.Run(() => FetchDataByUser());
            Task.Run(() => SetSettings());
        }
        public static async Task FetchDataByUser()
        {
            var appUser = AppStore.Instance.User;
            var mediStore = Container.Get<IDataStore<Medi>>();
            var dailyStore = Container.Get<IDataStore<DailyAppointment>>();
            var weekdaysStore = Container.Get<IDataStore<Weekdays>>();

            var medis = await mediStore.GetItemsByFkAsync(appUser.Id);
            foreach (var medi in medis)
            {
                var dailyAppointments = await dailyStore.GetItemsByFkAsync(medi.Id);
                if (medi.WeekdaysFk != Guid.Empty)
                {
                    var weekdays = await weekdaysStore.GetItemAsync(medi.WeekdaysFk);
                    medi.Weekdays = weekdays;
                }
                medi.DailyAppointments = dailyAppointments.ToList();
                appUser.Medis.Add(medi);
            }
        }

        public static async Task SetSettings()
        {
            var appSettingsStore = Container.Get<IDataStore<AppSettings>>();
            var appSettings = (await appSettingsStore.GetItemsAsync()).ToArray();
            AppSettings settings = null;
            if (appSettings == null || appSettings.Length == 0)
            {
                settings = new AppSettings
                {
                    Hour = 12,
                    Minute = 15,
                    Created = DateTimeOffset.Now,
                    Id = Guid.NewGuid()
                };
                await appSettingsStore.AddItemAsync(settings);
            }
            else
            {
                settings = appSettings[0];
            }
            AppStore.Instance.AppSettings = settings;
        }
    }
}
