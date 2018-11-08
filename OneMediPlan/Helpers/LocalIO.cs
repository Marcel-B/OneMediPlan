using System.Threading.Tasks;
using com.b_velop.OneMediPlan.Domain.Stores;
using com.b_velop.OneMediPlan.Services;

namespace com.b_velop.OneMediPlan.Helpers
{
    public static class LocalIO
    {
        public static async Task<bool> SaveDataAsync()
        {
            var st = new LocalDataStore();
            var inst = AppStore.Instance;
            if (inst.User == null) return false;
            var user = inst.User;
            var userSuccess = await st.PersistToDevice(user, "user.json");
            var weekdaysSuccess = await st.PersistToDevice(AppStore.Instance.Weekdays, "weekdays.json");
            var settingsSuccess = await st.PersistToDevice(AppStore.Instance.AppSettings, "settings.json");
            return userSuccess && weekdaysSuccess && settingsSuccess;
        }
    }
}
