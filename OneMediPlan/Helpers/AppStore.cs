using System;
using OneMediPlan.Models;
namespace OneMediPlan.Helpers
{
    public class AppStore
    {
        private static IDataStore<Medi> DataStore;
        protected AppStore() { }

        public static IDataStore<Medi> GetInstance()
        {
            if (DataStore == null)
                DataStore = ServiceLocator.Instance.Get<IDataStore<Medi>>() ?? new MockDataStore();
            return DataStore;
        }
    }
}
