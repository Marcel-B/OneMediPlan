namespace OneMediPlan.Models
{
    public class Minute
    {
        public Minute(int minute)
        {
            value = minute;
        }
        private readonly int value;
        public int Value { get => value; }
    }
}
