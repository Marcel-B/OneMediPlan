using System.Collections.Immutable;
using com.b_velop.OneMediPlan.Domain;

namespace com.b_velop.OneMediPlan.Redux.States
{
    public class ApplicationState
    {
        public ImmutableArray<MediUser> Users { get; set; }
        public ImmutableArray<Medi> Medis { get; set; }
        public ImmutableArray<Weekdays> Weekdays { get; set; }
        public ImmutableArray<AppSettings> AppSettings { get; set; }
    }
}
