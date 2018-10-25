using System;
using System.Collections.Generic;
using Ninject;

namespace com.b_velop.OneMediPlan.Models
{

    public class Medi : IComparable
    {
        public Medi() { }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Stock { get; set; }
        public double MinimumStock { get; set; }
        public double Dosage { get; set; }
        public Guid DependsOn { get; set; }
        //public MediType DosageType { get; set; }
        public IntervallType IntervallType { get; set; }
        public IntervallTime IntervallTime { get; set; }
        public int PureIntervall { get; set; }
        //public TimeSpan Intervall { get; set; }
        //public int IntervallInMinutes { get; set; }
        public DateTimeOffset NextDate { get; set; }
        public DateTimeOffset LastDate { get; set; }
        public DateTimeOffset LastRefill { get; set; }
        public IList<Tuple<Hour, Minute>> DailyAppointments { get; set; } // z.B. morgens mittags abends
        public bool[] Weekdays { get; set; }
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

        internal void Reset()
        {
            var store = App.Container.Get<IMediDataStore>();
            store.SetTemporaryMedi(new Medi());
            Console.WriteLine("AllValues reseted");
        }
    }
}
