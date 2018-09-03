namespace OneMediPlan.Models
{
    public class Hour
    {
        public Hour(int hour)
        {
            value = hour;
        }
        private readonly int value;
        public int Value { get => value; }
    }
}
