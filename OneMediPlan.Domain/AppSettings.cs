namespace com.b_velop.OneMediPlan.Domain
{
    public class AppSettings : Item
    {
        public MediUser User { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
    }
}
