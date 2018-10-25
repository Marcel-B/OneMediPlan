using System;

namespace com.b_velop.OneMediPlan.Models
{
    public class MediSettings
    {
        public MediSettings() { }
        public Guid Id { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
    }
}
