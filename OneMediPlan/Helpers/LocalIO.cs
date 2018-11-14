using System.Threading.Tasks;
using com.b_velop.OneMediPlan.Domain.Stores;
using com.b_velop.OneMediPlan.Services;
using System.Linq;

namespace com.b_velop.OneMediPlan.Helpers
{
    public static class LocalIO
    {
        public static async Task<bool> SaveDataAsync()
        {
            var st = new LocalDataStore();
            //var inst = AppStore.Instance;
            var state = App.Store.GetState();
            var user = state.Users.SingleOrDefault();
            if (user == null) return false;
            var userSuccess = await st.PersistToDevice(user, "user.json");
            var medis = state.Medis.ToList();
            if (medis != null)
                await st.PersistToDevice(medis, "medis.json");
            var weekdays = state.Weekdays.ToList();
            if (weekdays != null)
                await st.PersistToDevice(weekdays, "weekdays.json");
            //var weekdaysSuccess = await st.PersistToDevice(AppStore.Instance.Weekdays, ;
            //var settingsSuccess = await st.PersistToDevice(AppStore.Instance.AppSettings, "settings.json");
            return  true; //userSuccess && weekdaysSuccess && settingsSuccess;
        }
    }
}
