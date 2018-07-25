using System;

namespace OneMediPlan.Models
{
    public enum IntervallType
    {
        Nothing = -1,
        Intervall = 0,
        Weekdays = 1,
        Depend = 2,
        IfNedded = 3,
    }

    public enum MediType
    {
        Tablet = 0,
        Injection = 1,
        Fluency = 2,
    }

    public class Medi : Item
    {
        public Medi() { }

        public string Name { get; set; }
        public double Stock { get; set; }
        public double MinimumStock { get; set; }
        public double Dosage { get; set; }
        public MediType DosageType { get; set; }
        public IntervallType IntervallType { get; set; }
        public TimeSpan Intervall { get; set; }
        public int IntervallInMinutes { get; set; }
        public DateTimeOffset NextDate { get; set; }
        public DateTimeOffset LastDate { get; set; }
        public DateTimeOffset LastRefill { get; set; }
        public bool Confirmed { get; set; }
        public bool Scheduled { get; set; }
    }
}
