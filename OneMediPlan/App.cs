using OneMediPlan.Models;
using Ninject;

namespace OneMediPlan
{
    public class App
    {
        public static StandardKernel Container { get; set; }
        public static bool UseMockDataStore = true;
        public static string BackendUrl = "http://localhost:5000";

        public static void Initialize()
        {
            Container = new Ninject.StandardKernel();
            Container.Bind<MediViewModel>().ToSelf().InSingletonScope();
            if (UseMockDataStore)
                Container.Bind<IDataStore<Medi>>().To<MockDataStore>().InSingletonScope();
            else
                Container.Bind<IDataStore<Medi>>().To<CloudDataStore>().InSingletonScope();

        }
    }
}
