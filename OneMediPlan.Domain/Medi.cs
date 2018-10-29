using System;
using System.Collections.Generic;

using com.b_velop.OneMediPlan.Domain.Enums;

namespace com.b_velop.OneMediPlan.Domain
{
    public class Medi : Item, IComparable
    {

        // ForaignKeys --
        public MediUser User { get; set; }
        public Weekdays Weekdays { get; set; }
        public IList<DailyAppointment> DailyAppointments { get; set; }
        // --------------

        public string Name { get; set; }
        public Guid DependsOn { get; set; }
        public double Stock { get; set; }
        public double MinimumStock { get; set; }
        public double Dosage { get; set; }
        public int DosageType { get; set; }

        public IntervallType IntervallType { get; set; }
        public IntervallTime IntervallTime { get; set; }
        public int PureIntervall { get; set; }

        public DateTimeOffset NextDate { get; set; }
        public DateTimeOffset LastDate { get; set; }
        public DateTimeOffset LastRefill { get; set; }

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
