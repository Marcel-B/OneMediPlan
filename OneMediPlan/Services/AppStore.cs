using System;
using System.Collections.ObjectModel;

using com.b_velop.OneMediPlan.Models;

namespace com.b_velop.OneMediPlan.Services
{
    public class AppStore
    {
        static readonly Lazy<AppStore> instance = new Lazy<AppStore>(() => new AppStore());
        public static AppStore Instance => instance.Value;

        protected AppStore()
        {
            Medis = new ObservableCollection<Medi>();
        }

        public ObservableCollection<Medi> Medis { get; set; }
        public Medi CurrentMedi { get; set; }

    }
}
