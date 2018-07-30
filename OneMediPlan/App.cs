using System;
using OneMediPlan.Models;

namespace OneMediPlan
{
    public class App
    {
        public static bool UseMockDataStore = true;
        public static string BackendUrl = "http://localhost:5000";

        public static void Initialize()
        {
            if (UseMockDataStore)
                ServiceLocator.Instance.Register<IDataStore<Medi>, MockDataStore>();
            else
                ServiceLocator.Instance.Register<IDataStore<Medi>, CloudDataStore>();
        }
    }
}
