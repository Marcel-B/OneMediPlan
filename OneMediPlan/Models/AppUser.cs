using System;
using System.Collections.ObjectModel;
namespace com.b_velop.OneMediPlan.Models
{
    public class AppUser
    {
        public AppUser()
        {
            AppMedis = new ObservableCollection<AppMedi>();
        }
        public ObservableCollection<AppMedi> AppMedis { get; set; }
    }
}
