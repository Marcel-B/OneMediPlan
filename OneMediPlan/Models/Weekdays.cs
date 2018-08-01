using System;

namespace OneMediPlan.Models
{
    public class Weekdays
    {
        public Guid Id { get; set; }
        public Guid MediFk { get; set; }
        public bool[] Days { get; set; }
    }
}
