using System;
using System.Threading.Tasks;
using Ninject;
using System.Linq;
using System.Collections.Generic;
using com.b_velop.OneMediPlan.Models;
using com.b_velop.OneMediPlan.Services;

namespace com.b_velop.OneMediPlan.Helpers
{
    public static class MediExtensions
    {
        public static string Today { get; set; }

        public static string GetStockInfo(this AppMedi medi)
            => $"{medi.Stock.ToString("F1")}/{medi.MinimumStock.ToString("F1")}";

        async public static Task<AppMedi> GetDependend(this AppMedi medi)
        {
            //var target = medi.DependsOn;
            //var storage = AppStore.Instance.User.AppMedis;
            //var med = await storage.GetItemAsync(target);
            //return med;
            return null;
        }

        public static bool NeedsNoStartDate(this AppMedi medi)
        => medi.DependsOn != Guid.Empty ||
               medi.DailyAppointments != null;

        public static void CalculateNewWeekdayIntervall(this AppMedi medi)
        {
            //var weekdays = await medi.GetWeekdaysAsync();
            var days = medi.Weekdays;
            var now = DateTimeOffset.Now;
            var today = (int)now.DayOfWeek;
            int diffDay = 0;
            for (var i = today + 1; i < days.Length; i++)
            {
                if (days[i])
                {
                    diffDay = i - today;
                    break;
                }
            }
            if (diffDay == 0)
            {
                for (var i = 0; i < today; i++)
                {
                    if (days[i])
                    {
                        diffDay = i - today + 7;
                        break;
                    }
                }
            }
            if (diffDay == 0)
                diffDay = 7;

            medi.PureIntervall = diffDay;
        }

        public static int TimeFactor(this AppMedi medi)
        {
            switch (medi.IntervallTime)
            {
                case IntervallTime.Minute:
                    return 1;
                case IntervallTime.Hour:
                    return 60;
                case IntervallTime.Day:
                    return 60 * 24;
                case IntervallTime.Week:
                    return 60 * 24 * 7;
                case IntervallTime.Month:
                    return 0;
                default:
                    return -1;
            }
        }

        public static int MinutesToNext(this AppMedi medi)
        {
            var factor = medi.TimeFactor();
            var minutesUntilNext = factor * medi.PureIntervall;
            return minutesUntilNext;
        }

        public static string GetNextDate(this AppMedi medi)
        {
            if (medi.IntervallType == IntervallType.IfNedded)
                return "-";
            if (medi.NextDate == DateTimeOffset.MinValue)
                return "-";

            var span = medi.NextDate - DateTimeOffset.Now;
            var today = DateTimeOffset.Now.DayOfWeek;
            var next = medi.NextDate.DayOfWeek;

            if (span.Days == 0 && today == next)
            {
                return medi.MinutesToNext() <= 1440 
                           ? $"{Today} - {medi.NextDate.ToString("t")}" 
                               : Today;
            }

            if (span.Days < 6 && today != next)// Diese Woche
            {
                return medi.NextDate.ToString("dddd");
            }

            return medi.NextDate.ToString("dd. MMMM");
        }

        public static string GetLastDate(this AppMedi medi)
        {
            if (medi.LastDate == DateTimeOffset.MinValue)
                return "-";

            var now = DateTimeOffset.Now;
            var latest = medi.LastDate;
            var diffDay = now - latest;
            return now.DayOfWeek == latest.DayOfWeek && diffDay.Days <= 1 ? 
                      medi.LastDate.ToString("t") : 
                      medi.LastDate.ToString("M");
        }

        public static string GetDosage(this AppMedi medi)
            => medi.IntervallType == IntervallType.IfNedded ? "-" : medi.Dosage.ToString("F1");

        //public static void AddWeekdayToDb(Realm realm, Medi medi)
        //{
        //    realm.Write(() =>
        //    {
        //        var weekday = medi.Weekdays;
        //        var wd = new Weekdays
        //        {
        //            Id = Guid.NewGuid().ToString(),
        //            MediFk = medi.Id.ToString(),
        //            Created = DateTimeOffset.Now,
        //            Monday = weekday[1],
        //            Tuesday = weekday[2],
        //            Wednesday = weekday[3],
        //            Thursday = weekday[4],
        //            Friday = weekday[5],
        //            Saturday = weekday[6],
        //            Sunday = weekday[0]
        //        };
        //        realm.Add(wd);
        //    });
        //}

        //public async static Task<MediSave> Save(this Medi medi)
        //{
        //    var realm = await Realm.GetInstanceAsync(App.RealmConf);
        //    var obj = new MediSave();
        //    obj.Id = medi.Id.ToString();
        //    obj.Name = medi.Name;
        //    obj.Created = medi.Create;
        //    obj.DailyAppointments = medi.IntervallType == IntervallType.DailyAppointment;
        //    obj.DependsOn = medi.IntervallType == IntervallType.Depend ? medi.DependsOn.ToString() : Guid.Empty.ToString();
        //    obj.Description = medi.Description ?? String.Empty;
        //    obj.Dosage = medi.Dosage;
        //    //obj.DosageType = (int)medi.DosageType;
        //    obj.IntervallTime = (int)medi.IntervallTime;
        //    obj.IntervallType = (int)medi.IntervallType;
        //    obj.LastDate = medi.LastDate;
        //    obj.LastEdit = DateTimeOffset.Now;
        //    obj.LastRefill = medi.LastRefill;
        //    obj.MinimumStock = medi.MinimumStock;
        //    obj.NextDate = medi.NextDate;
        //    obj.PureIntervall = medi.PureIntervall;
        //    obj.Stock = medi.Stock;
        //    realm.Write(() => realm.Add(obj));

        //    var currentIntervallType = medi.IntervallType;
        //    switch (currentIntervallType)
        //    {
        //        case IntervallType.DailyAppointment:
        //            realm.Write(() =>
        //            {
        //                foreach (var dailyAppointment in medi.DailyAppointments)
        //                {
        //                    var tmp = new DailyAppointment
        //                    {
        //                        Id = Guid.NewGuid().ToString(),
        //                        Hour = dailyAppointment.Item1.Value,
        //                        Minute = dailyAppointment.Item2.Value,
        //                        MediFk = medi.Id.ToString()
        //                    };
        //                    realm.Add(tmp);
        //                }
        //            });
        //            break;
        //        case IntervallType.Weekdays:
        //            AddWeekdayToDb(realm, medi);
        //            break;
        //        case IntervallType.Depend:
        //        case IntervallType.IfNedded:
        //        case IntervallType.Intervall:
        //        case IntervallType.Nothing:
        //            break;
        //        default:
        //            break;
        //    }
        //    return obj;
        //}

        //public async static Task<MediSave> Update(this Medi medi)
        //{
        //    var r = await Realm.GetInstanceAsync(App.RealmConf);
        //    var me = r.Find<MediSave>(medi.Id.ToString());
        //    using (var trans = r.BeginWrite())
        //    {
        //        me.Name = medi.Name;
        //        me.DailyAppointments = medi.DailyAppointments != null;
        //        me.DependsOn = medi.DependsOn.ToString();
        //        me.Description = medi.Description;
        //        me.Dosage = medi.Dosage;
        //        //me.DosageType = (int)medi.DosageType;
        //        me.IntervallTime = (int)medi.IntervallTime;
        //        me.IntervallType = (int)medi.IntervallType;
        //        me.LastDate = medi.LastDate;
        //        me.LastEdit = DateTimeOffset.Now;
        //        me.LastRefill = medi.LastRefill;
        //        me.MinimumStock = medi.MinimumStock;
        //        me.NextDate = medi.NextDate;
        //        me.PureIntervall = medi.PureIntervall;
        //        me.Stock = medi.Stock;
        //        trans.Commit();
        //    }
        //    if (medi.DailyAppointments != null)
        //    {
        //        var da = r.All<DailyAppointment>()
        //                     .ToList()
        //                     .Where(d => d.MediFk.Equals(medi.Id.ToString()))
        //                     .ToList();
        //        r.Write(() =>
        //        {
        //            foreach (var d in da)
        //            {
        //                r.Remove(d);
        //            }
        //        });
        //        foreach (var appointment in medi.DailyAppointments)
        //        {
        //            await r.WriteAsync((Realm realm) => realm.Add(new DailyAppointment
        //            {
        //                Id = Guid.NewGuid().ToString(),
        //                MediFk = medi.Id.ToString(),
        //                Hour = appointment.Item1.Value,
        //                Minute = appointment.Item2.Value
        //            }));
        //        }
        //    }
        //    else
        //    {
        //        // Keine Daily Appointments mehr, abrer es sind noch einträge vorhanden
        //        var da = r.All<DailyAppointment>()
        //                  .ToList()
        //                  .Where(d => d.MediFk.Equals(medi.Id.ToString()));
        //        r.Write(() =>
        //        {
        //            foreach (var d in da)
        //            {
        //                r.Remove(d);
        //            }
        //        });
        //    }
        //    return me;
        //}

        //public static Medi ToMedi(this MediSave medi)
        //{
        //    var mediHasDailyAppointments = medi.DailyAppointments;
        //    var intervallType = (IntervallType)medi.IntervallType;

        //    var currentMedi = new Medi
        //    {
        //        Id = Guid.Parse(medi.Id),
        //        Create = medi.Created,
        //        Name = medi.Name,
        //        LastDate = medi.LastDate,
        //        LastEdit = medi.LastEdit,
        //        LastRefill = medi.LastRefill,
        //        DependsOn = Guid.Parse(medi.DependsOn),
        //        Stock = medi.Stock,
        //        Dosage = medi.Dosage,
        //        MinimumStock = medi.MinimumStock,
        //        NextDate = medi.NextDate,
        //        //DosageType = (MediType)medi.DosageType,
        //        IntervallType = intervallType,
        //        IntervallTime = (IntervallTime)medi.IntervallTime,
        //        Description = medi.Description,
        //        PureIntervall = medi.PureIntervall,
        //    };

        //    if (intervallType == IntervallType.DailyAppointment)
        //    {
        //        var realm = Realm.GetInstance(App.RealmConf);
        //        var da = realm.All<DailyAppointment>();
        //        var dd = da.Where(a => a.MediFk.Equals(medi.Id));
        //        currentMedi.DailyAppointments = new List<Tuple<Hour, Minute>>();
        //        foreach (var item in dd)
        //            currentMedi.DailyAppointments.Add(new Tuple<Hour, Minute>(new Hour(item.Hour), new Minute(item.Minute)));
        //    }
        //    else if (intervallType == IntervallType.Weekdays)
        //    {
        //        var realm = Realm.GetInstance(App.RealmConf);
        //        var weekdays = realm
        //            .All<Weekdays>()
        //            .ToList();
        //        currentMedi.Weekdays = new bool[7];
        //        var currentWeekdays = weekdays.SingleOrDefault(wd => medi.Id.Equals(wd.MediFk));
        //        currentMedi.Weekdays[0] = currentWeekdays.Sunday;
        //        currentMedi.Weekdays[1] = currentWeekdays.Monday;
        //        currentMedi.Weekdays[2] = currentWeekdays.Tuesday;
        //        currentMedi.Weekdays[3] = currentWeekdays.Wednesday;
        //        currentMedi.Weekdays[4] = currentWeekdays.Thursday;
        //        currentMedi.Weekdays[5] = currentWeekdays.Friday;
        //        currentMedi.Weekdays[6] = currentWeekdays.Sunday;
        //    }
        //    return currentMedi;
        //}
    }
}
