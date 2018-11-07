using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using com.b_velop.OneMediPlan.Domain;
using com.b_velop.OneMediPlan.Domain.MockStores;
using com.b_velop.OneMediPlan.Domain.Services;
using com.b_velop.OneMediPlan.Domain.Stores;
using com.b_velop.OneMediPlan.Helpers;
using com.b_velop.OneMediPlan.Meta.Interfaces;
using com.b_velop.OneMediPlan.Models;
using com.b_velop.OneMediPlan.Services;
using com.b_velop.OneMediPlan.ViewModels;

using I18NPortable;
using Newtonsoft.Json;
using Ninject;

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
        public static string URL = "https://app.marcelbenders.de";
        public static StandardKernel Container { get; set; }

#if DEBUG
        public static bool UseMockDataStore = true;
#else
        public static bool UseMockDataStore = false;
#endif

        public static Action<Medi> SetNotification { get; set; }

        public static void Initialize()
        {
            var Logger = new AppLogger();
            I18N.Current
               .SetNotFoundSymbol("!!") // Optional: when a key is not found, it will appear as $key$ (defaults to "$")
               .SetFallbackLocale("de") // Optional but recommended: locale to load in case the system locale is not supported
               .SetThrowWhenKeyNotFound(true) // Optional: Throw an exception when keys are not found (recommended only for debugging)
               .SetLogger(text => Logger.Log(text.Substring(7), typeof(I18N).Name)) // action to output traces
               .SetResourcesFolder("Locales") // Optional: The directory containing the resource files (defaults to "Locales")
               .Init(typeof(App).GetTypeInfo().Assembly); // assembl

            try
            {


                var medi = new Medi();
                medi.Id = Guid.NewGuid();
                medi.Name = "Furtz";

                var json = JsonConvert.SerializeObject(medi);
                var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                var dir = Directory.GetFiles(path);
                var filePath = Path.Combine(path, "medi.json");
                using (var file = File.Open(filePath, FileMode.Create, FileAccess.Write))
                using (var strm = new StreamWriter(file))
                {
                    strm.Write(json);
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Error occured while writing to device", typeof(App), ex);
            }

            Container = new StandardKernel();
            Container.Bind<ILogger>().To<AppLogger>();
            Container.Bind<MainViewModel>().ToSelf().InSingletonScope();
            Container.Bind<AppSettingsViewModel>().ToSelf().InSingletonScope();
            Container.Bind<MediDetailViewModel>().ToSelf().InSingletonScope();
            Container.Bind<NewMediViewModel>().ToSelf().InSingletonScope();
            Container.Bind<DependencyViewModel>().ToSelf().InSingletonScope();
            Container.Bind<DailyViewModel>().ToSelf().InSingletonScope();
            Container.Bind<StartViewModel>().ToSelf().InSingletonScope();
            Container.Bind<Medi>().ToSelf().InSingletonScope(); // Temp Medi
            Container.Bind<ISomeLogic>().To<SomeLogic>();

            if (UseMockDataStore)
            {
                Container.Bind<IDataStore<Medi>>().To<MediDataMock>().InSingletonScope();
                Container.Bind<IDataStore<Weekdays>>().To<WeekdaysDataMock>().InSingletonScope();
                Container.Bind<IDataStore<AppSettings>>().To<AppSettingsDataMock>().InSingletonScope();
                Container.Bind<IDataStore<DailyAppointment>>().To<DailyAppointmentDataMock>().InSingletonScope();
            }
            else
            {
                Container.Bind<IDataStore<Medi>>().To<MediDataStore>().WithConstructorArgument("backendUrl", App.URL);
                Container.Bind<IDataStore<Weekdays>>().To<WeekdaysDataStore>().WithConstructorArgument("backendUrl", App.URL);
                Container.Bind<IDataStore<AppSettings>>().To<AppSettingsDataStore>().WithConstructorArgument("backendUrl", App.URL);
                Container.Bind<IDataStore<DailyAppointment>>().To<DailyAppointmentDataStore>().WithConstructorArgument("backendUrl", App.URL);
            }
            AppStore.Instance.User = new MediUser
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
            var localDataStore = new LocalDataStore();
            var user = await localDataStore.LoadFromDevice<MediUser>("user.json");
            if (user == null)
            {
                user = new MediUser
                {
                    Id = Guid.NewGuid(),
                    Created = DateTimeOffset.Now,
                    LastEdit = DateTimeOffset.Now,
                    Name = Environment.UserName,
                };
            }
            var weekdays = await localDataStore.LoadFromDevice<Dictionary<Guid, Weekdays>>("weekdays.json");

            AppStore.Instance.User = user;
            AppStore.Instance.Weekdays = weekdays;

            //var appUser = AppStore.Instance.User;
            //var mediStore = Container.Get<IDataStore<Medi>>();
            //var dailyStore = Container.Get<IDataStore<DailyAppointment>>();
            //var weekdaysStore = Container.Get<IDataStore<Weekdays>>();

            //var medis = await mediStore.GetItemsByFkAsync(appUser.Id);
            //foreach (var medi in medis)
            //{
            //    var dailyAppointments = await dailyStore.GetItemsByFkAsync(medi.Id);
            //    var weekdays = (await weekdaysStore.GetItemsByFkAsync(medi.Id)).ToList();
            //    if (weekdays.Count > 0)
            //        AppStore.Instance.Weekdays[medi.Id] = weekdays.First();
            //    if (dailyAppointments != null)
            //        medi.DailyAppointments = dailyAppointments.ToList();
            //    appUser.Medis.Add(medi);
            //}
        }

        public static async Task SetSettings()
        {
            var localDataStore = new LocalDataStore();
            var appSettings = await localDataStore.LoadFromDevice<AppSettings>("settings.json");
            AppStore.Instance.AppSettings = appSettings;
            //var appSettingsStore = Container.Get<IDataStore<AppSettings>>();
            //var appSettings = (await appSettingsStore.GetItemsAsync()).ToArray();
            //AppSettings settings = null;
            //if (appSettings == null || appSettings.Length == 0)
            //{
            //    settings = new AppSettings
            //    {
            //        Hour = 12,
            //        Minute = 15,
            //        Created = DateTimeOffset.Now,
            //        Id = Guid.NewGuid()
            //    };
            //    await appSettingsStore.AddItemAsync(settings);
            //}
            //else
            //{
            //    settings = appSettings[0];
            //}
            //AppStore.Instance.AppSettings = settings;
        }
    }
}
