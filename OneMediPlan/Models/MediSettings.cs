using Realms;

namespace OneMediPlan.Models
{
    public class MediSettings : RealmObject
    {
        public MediSettings() { }
        public string Id { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
    }
}
