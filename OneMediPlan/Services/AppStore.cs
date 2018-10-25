using System;

using com.b_velop.OneMediPlan.Models;

namespace com.b_velop.OneMediPlan.Services
{
    public class AppStore
    {
        static readonly Lazy<AppStore> instance = new Lazy<AppStore>(() => new AppStore());
        public static AppStore Instance => instance.Value;

        protected AppStore()
        {

        }

        public AppUser User { get; set; }
        public AppMedi CurrentMedi { get; set; }
    }
}
