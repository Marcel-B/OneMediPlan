using System;

namespace OneMediPlan.Models
{

    public class Medi : Item, IComparable
    {
        public Medi() { }

        public string Name { get; set; }
        public double Stock { get; set; }
        public double MinimumStock { get; set; }
        public double Dosage { get; set; }
        public Guid DependsOn { get; set; }
        public MediType DosageType { get; set; }
        public IntervallType IntervallType { get; set; }
        public TimeSpan Intervall { get; set; }
        public int IntervallInMinutes { get; set; }
        public DateTimeOffset NextDate { get; set; }
        public DateTimeOffset LastDate { get; set; }
        public DateTimeOffset LastRefill { get; set; }
        public bool Confirmed { get; set; }
        public bool Scheduled { get; set; }

        public int CompareTo(Object obj)
        {
            if (obj is Medi med)
            {
                if (this.NextDate == DateTimeOffset.MinValue) return 1;
                if (med.NextDate == DateTimeOffset.MinValue) return -1;
                if (this.NextDate < med.NextDate) return -1;
                if (this.NextDate > med.NextDate) return 1;
                return 0;
            }
            throw new ArgumentException();
        }
    }
}
