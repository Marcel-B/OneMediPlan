using System;

using com.b_velop.OneMediPlan.Models;
using com.b_velop.OneMediPlan.Domain;

namespace com.b_velop.OneMediPlan.Services
{
    public class AppStore
    {
        static readonly Lazy<AppStore> instance = new Lazy<AppStore>(() => new AppStore());
        public static AppStore Instance => instance.Value;

        protected AppStore() { }

        public MediUser User { get; set; }
        public Medi CurrentMedi { get; set; }
        public AppSettings AppSettings { get; set; }
    }
}
