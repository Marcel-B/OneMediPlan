using System;
using System.Collections.Generic;
using Realms;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using System.ComponentModel.DataAnnotations;
using Ninject;
using System.ComponentModel;

namespace OneMediPlan.Models
{
    public enum IntervallTime
    {
        Minute,
        Hour,
        Day,
        Week,
        Month
    }

    public class Hour
    {
        public Hour(int hour)
        {
            value = hour;
        }
        private readonly int value;
        public int Value { get => value; }
    }

    public class Minute
    {
        public Minute(int minute)
        {
            value = minute;
        }
        private readonly int value;
        public int Value { get => value; }
    }

    public class DailyAppointment : RealmObject
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string MediFk { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
    }

    public class MediSave : RealmObject
    {
        [PrimaryKey]
        public string Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset LastEdit { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
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
        public IntervallTime IntervallTime { get; set; }
        public int PureIntervall { get; set; }
        public TimeSpan Intervall { get; set; }
        public int IntervallInMinutes { get; set; }
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


    public static class MediExtensions
    {
        public static void AddWeekdayToDb(Realm realm, Medi medi)
        {
            realm.Write(() =>
            {
                var weekday = medi.Weekdays;
                var wd = new Weekdays
                {
                    Id = Guid.NewGuid().ToString(),
                    MediFk = medi.Id.ToString(),
                    Created = DateTimeOffset.Now,
                    Monday = weekday[1],
                    Tuesday = weekday[2],
                    Wednesday = weekday[3],
                    Thursday = weekday[4],
                    Friday = weekday[5],
                    Saturday = weekday[6],
                    Sunday = weekday[0]
                };
                realm.Add(wd);
            });
        }
        public async static Task<MediSave> Save(this Medi medi)
        {
            var realm = await Realm.GetInstanceAsync(App.RealmConf);
            var obj = new MediSave();
            obj.Id = medi.Id.ToString();
            obj.Name = medi.Name;
            obj.Created = medi.Create;
            obj.DailyAppointments = medi.IntervallType == IntervallType.DailyAppointment;
            obj.DependsOn = medi.IntervallType == IntervallType.Depend ? medi.DependsOn.ToString() : Guid.Empty.ToString();
            obj.Description = medi.Description ?? String.Empty;
            obj.Dosage = medi.Dosage;
            obj.DosageType = (int)medi.DosageType;
            obj.IntervallTime = (int)medi.IntervallTime;
            obj.IntervallType = (int)medi.IntervallType;
            obj.LastDate = medi.LastDate;
            obj.LastEdit = DateTimeOffset.Now;
            obj.LastRefill = medi.LastRefill;
            obj.MinimumStock = medi.MinimumStock;
            obj.NextDate = medi.NextDate;
            obj.PureIntervall = medi.PureIntervall;
            obj.Stock = medi.Stock;
            realm.Write(() => realm.Add(obj));

            var currentIntervallType = medi.IntervallType;
            switch (currentIntervallType)
            {
                case IntervallType.DailyAppointment:
                    realm.Write(() =>
                    {
                        foreach (var dailyAppointment in medi.DailyAppointments)
                        {
                            var tmp = new DailyAppointment
                            {
                                Id = Guid.NewGuid().ToString(),
                                Hour = dailyAppointment.Item1.Value,
                                Minute = dailyAppointment.Item2.Value,
                                MediFk = medi.Id.ToString()
                            };
                            realm.Add(tmp);
                        }
                    });
                    break;
                case IntervallType.Weekdays:
                    AddWeekdayToDb(realm, medi);
                    break;
                case IntervallType.Depend:
                case IntervallType.IfNedded:
                case IntervallType.Intervall:
                case IntervallType.Nothing:
                    break;
                default:
                    break;
            }
            return obj;
        }

        public async static Task<MediSave> Update(this Medi medi)
        {
            var r = await Realm.GetInstanceAsync(App.RealmConf);
            var me = r.Find<MediSave>(medi.Id.ToString());
            using (var trans = r.BeginWrite())
            {
                me.Name = medi.Name;
                me.DailyAppointments = medi.DailyAppointments != null;
                me.DependsOn = medi.DependsOn.ToString();
                me.Description = medi.Description;
                me.Dosage = medi.Dosage;
                me.DosageType = (int)medi.DosageType;
                me.IntervallTime = (int)medi.IntervallTime;
                me.IntervallType = (int)medi.IntervallType;
                me.LastDate = medi.LastDate;
                me.LastEdit = DateTimeOffset.Now;
                me.LastRefill = medi.LastRefill;
                me.MinimumStock = medi.MinimumStock;
                me.NextDate = medi.NextDate;
                me.PureIntervall = medi.PureIntervall;
                me.Stock = medi.Stock;
                trans.Commit();
            }
            if (medi.DailyAppointments != null)
            {
                var da = r.All<DailyAppointment>()
                             .ToList()
                             .Where(d => d.MediFk.Equals(medi.Id.ToString()))
                             .ToList();
                r.Write(() =>
                {
                    foreach (var d in da)
                    {
                        r.Remove(d);
                    }
                });
                foreach (var appointment in medi.DailyAppointments)
                {
                    await r.WriteAsync((Realm realm) => realm.Add(new DailyAppointment
                    {
                        Id = Guid.NewGuid().ToString(),
                        MediFk = medi.Id.ToString(),
                        Hour = appointment.Item1.Value,
                        Minute = appointment.Item2.Value
                    }));
                }
            }
            else
            {
                // Keine Daily Appointments mehr, abrer es sind noch einträge vorhanden
                var da = r.All<DailyAppointment>()
                          .ToList()
                          .Where(d => d.MediFk.Equals(medi.Id.ToString()));
                r.Write(() =>
                {
                    foreach (var d in da)
                    {
                        r.Remove(d);
                    }
                });
            }
            return me;
        }

        public static Medi ToMedi(this MediSave medi)
        {
            var mediHasDailyAppointments = medi.DailyAppointments;
            IList<Tuple<Hour, Minute>> das = null;
            var intervallType = (IntervallType)medi.IntervallType;

            var currentMedi = new Medi
            {
                Id = Guid.Parse(medi.Id),
                Create = medi.Created,
                Name = medi.Name,
                LastDate = medi.LastDate,
                LastEdit = medi.LastEdit,
                LastRefill = medi.LastRefill,
                DependsOn = Guid.Parse(medi.DependsOn),
                Stock = medi.Stock,
                Dosage = medi.Dosage,
                MinimumStock = medi.MinimumStock,
                NextDate = medi.NextDate,
                DosageType = (MediType)medi.DosageType,
                IntervallType = intervallType,
                IntervallTime = (IntervallTime)medi.IntervallTime,
                Description = medi.Description,
                PureIntervall = medi.PureIntervall,
            };

            if (intervallType == IntervallType.DailyAppointment)
            {
                var realm = Realm.GetInstance(App.RealmConf);
                var da = realm.All<DailyAppointment>();
                var dd = da.Where(a => a.MediFk.Equals(medi.Id));
                currentMedi.DailyAppointments = new List<Tuple<Hour, Minute>>();
                foreach (var item in dd)
                    currentMedi.DailyAppointments.Add(new Tuple<Hour, Minute>(new Hour(item.Hour), new Minute(item.Minute)));
            }
            else if (intervallType == IntervallType.Weekdays)
            {
                var realm = Realm.GetInstance(App.RealmConf);
                var weekdays = realm
                    .All<Weekdays>()
                    .ToList();
                currentMedi.Weekdays = new bool[7];
                var currentWeekdays = weekdays.SingleOrDefault(wd => medi.Id.Equals(wd.MediFk));
                currentMedi.Weekdays[0] = currentWeekdays.Sunday;
                currentMedi.Weekdays[1] = currentWeekdays.Monday;
                currentMedi.Weekdays[2] = currentWeekdays.Tuesday;
                currentMedi.Weekdays[3] = currentWeekdays.Wednesday;
                currentMedi.Weekdays[4] = currentWeekdays.Thursday;
                currentMedi.Weekdays[5] = currentWeekdays.Friday;
                currentMedi.Weekdays[6] = currentWeekdays.Sunday;
            }
            return currentMedi;
        }
    }
}
