using System;
using Realms;

namespace OneMediPlan.Models
{
    public class Weekdays : RealmObject
    {

        [PrimaryKey]
        public string Id { get; set; }
        public string MediFk { get; set; }

        public DateTimeOffset Created { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
    }
}
