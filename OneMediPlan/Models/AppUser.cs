using System;
using System.Collections.ObjectModel;
using com.b_velop.OneMediPlan.Domain;

namespace com.b_velop.OneMediPlan.Models
{
    public class AppUser
    {
        public Guid Id { get; set; }

        public AppUser()
        {
            Id = Guid.NewGuid();
            AppMedis = new ObservableCollection<Medi>();
        }
        public ObservableCollection<Medi> AppMedis { get; set; }
    }
}
