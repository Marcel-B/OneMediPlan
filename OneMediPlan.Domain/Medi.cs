using System;
namespace com.b_velop.OneMediPlan.Domain
{
    public class Medi : Item
    {
        public string Name { get; set; }
        public bool DailyAppointments { get; set; }
        public string DependsOn { get; set; }
        public double Stock { get; set; }
        public double MinimumStock { get; set; }
        public double Dosage { get; set; }
        public int DosageType { get; set; }

        public int IntervallType { get; set; }
        public int IntervallTime { get; set; }
        public int PureIntervall { get; set; }

        public DateTimeOffset NextDate { get; set; }
        public DateTimeOffset LastDate { get; set; }
        public DateTimeOffset LastRefill { get; set; }
    }
}
