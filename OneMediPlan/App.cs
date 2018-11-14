using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
using Ninject;
using Redux;
using com.b_velop.OneMediPlan.Redux.Actions;
using com.b_velop.OneMediPlan.Redux.States;
using com.b_velop.OneMediPlan.Redux;

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


    public static class App
    {


        /// <summary>
        /// The Store<TState> is the class that bring actions and reducer 
        /// together.The store has the following responsibilities:

        /// * Holds application state of type TState.
        /// * Allows state to be updated via Dispatch(IAction action).
        /// * Registers listeners via Subscribe(IObserver observer). 
        ///   The Store<TState> class implements IObservable so 
        ///   ReactiveExtensions is a usefull tool to observe 
        ///   state changes.

        /// It’s important to note that you’ll only have a single store in a 
        /// Redux application.In the examples, I keep it as a static property 
        /// on the application class.

        /// The Store constructor take an initial state, of type TState, 
        /// and a reducer.

        /// This is the main entry point of the application.
        /// </summary>
        public static IStore<ApplicationState> Store { get; private set; }

        /// <summary>
        /// Main url for app API
        /// </summary>
        public static string URL = "https://app.marcelbenders.de";
        public static StandardKernel Container { get; set; }

#if DEBUG
        public static bool UseMockDataStore = true;
#else
        public static bool UseMockDataStore = false;
#endif

        /// <summary>
        /// Generic Action which is used to set a notification on device
        /// </summary>
        /// <value>The set notification.</value>
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

            var initialState = new ApplicationState
            {
                AppSettings = ImmutableArray<AppSettings>.Empty,
                Users = ImmutableArray<MediUser>.Empty,
                Weekdays = ImmutableArray<Weekdays>.Empty,
                Medis = ImmutableArray<Medi>.Empty,
            };
            Store = new Store<ApplicationState>(Reducers.ReduceApplication, initialState);


            //AppStore.Instance.User = new MediUser
            //{
            //    Id = MediDataMock.USER_ID,
            //    Created = DateTimeOffset.Now,
            //    Medis = new List<Medi>()
            //};

            Task.Run(() => FetchDataByUser());
        }


        public static async Task FetchDataByUser()
        {
            // Load data from device
            var localDataStore = new LocalDataStore();

            // First load user data, if not exists create a new user
            var user = await localDataStore.LoadFromDevice<MediUser>("user.json");
            if (user == null)
            {
                user = new MediUser
                {
                    Id = Guid.NewGuid(),
                    Created = DateTimeOffset.Now,
                    LastEdit = DateTimeOffset.Now,
                    Name = Environment.UserName,
                    Medis = new List<Medi>()
                };
            }

            App.Store.Dispatch(new AddUserAction
            {
                Id = user.Id,
                Created = user.Created,
                LastEdit = user.LastEdit,
                Email = user.Email,
                Birthday = user.Birthdate,
                Username = user.Username,
                Name = user.Name,
                Surename = user.Surename
            });

            //AppStore.Instance.User = user;

            // Get medis from user and put it into redux-store
            foreach (var medi in user.Medis)
            {
                App.Store.Dispatch(new AddMediAction
                {
                    Id = medi.Id,
                    User = medi.User,
                    Name = medi.Name,
                    Dosage = medi.Dosage,
                    Stock = medi.Stock,
                    Created = medi.Created,
                    DailyAppointments = medi.DailyAppointments,
                    DependsOn = medi.DependsOn,
                    Description = medi.Description,
                    LastEdit = medi.LastEdit,
                    DosageType = medi.DosageType,
                    IntervallTime = medi.IntervallTime,
                    MinimumStock = medi.MinimumStock,
                    IntervallType = medi.IntervallType,
                    PureIntervall = medi.PureIntervall,
                    NextDate = medi.NextDate,
                    LastDate = medi.LastDate,
                    LastRefill = medi.LastRefill
                });
            }

            await SetSettings();

            // Load Weekdays from Device
            var weekdays = await localDataStore.LoadFromDevice<Dictionary<Guid, Weekdays>>("weekdays.json");

            foreach (var wd in weekdays.Values)
            {
                App.Store.Dispatch(new AddWeekdaysAction
                {
                    Id = wd.Id,
                    Created = wd.Created,
                    LastEdit = wd.LastEdit,
                    Medi = wd.Medi,
                    Monday = wd.Monday,
                    Tuesday = wd.Tuesday,
                    Wednesday = wd.Wednesday,
                    Thursday = wd.Thursday,
                    Friday = wd.Friday,
                    Saturday = wd.Saturday,
                    Sunday = wd.Sunday
                });
            }


            //AppStore.Instance.Weekdays = weekdays ?? new Dictionary<Guid, Weekdays>();
        }

        //public static async Task FetchFromCloud(){
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
        //}

        public static async Task SetSettings()
        {
            var localDataStore = new LocalDataStore();
            var appSettings = await localDataStore.LoadFromDevice<AppSettings>("settings.json");
            if (appSettings == null)
                appSettings = new AppSettings
                {
                    Id = Guid.NewGuid(),
                    Created = DateTimeOffset.Now,
                    LastEdit = DateTimeOffset.Now,
                    Description = "nothing",
                    Hour = 12,
                    Minute = 30,
                    User = AppStore.Instance.User
                };
            AppStore.Instance.AppSettings = appSettings;
        }
    }
}
