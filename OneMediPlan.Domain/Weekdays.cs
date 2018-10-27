using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace com.b_velop.OneMediPlan.Domain
{
    public class Weekdays : Item
    {
        [NotMapped]
        public Guid MediFk { get; set; }

        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
    }
}
