using System;
using System.Threading.Tasks;
using Ninject;
using OneMediPlan.Models;
using System.Linq;
using System.Runtime.CompilerServices;

namespace OneMediPlan.Helpers
{
    public static class MediExtensions
    {
        public static string GetStockInfo(this Medi medi)
        {
            return $"{medi.Stock.ToString("F1")}/{medi.MinimumStock.ToString("F1")}";
        }

        async public static Task<Medi> GetDependend(this Medi medi)
        {
            var target = medi.DependsOn;
            var storage = App.Container.Get<IDataStore<Medi>>();
            var med = await storage.GetItemAsync(target);
            return med;
        }

        async public static Task<Weekdays> GetWeekdaysAsync(this Medi medi)
        {
            var store = App.Container.Get<IDataStore<Weekdays>>();
            var days = await store.GetItemsAsync();
            return days.SingleOrDefault(x => x.MediFk == medi.Id);
        }

        public async static Task CalculateNewWeekdayIntervall(this Medi medi)
        {
            var weekdays = await medi.GetWeekdaysAsync();
            var days = weekdays.Days;
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
            {
                diffDay = 7;
            }
            medi.PureIntervall = diffDay;
        }

        public static string GetNextDate(this Medi medi)
        {
            if (medi.IntervallType == IntervallType.IfNedded)
                return "-";
            if (medi.NextDate == DateTimeOffset.MinValue)
                return "-";
            var span = medi.NextDate - DateTimeOffset.Now;
            if (span.Days == 0)
            {
                if (medi.IntervallInMinutes <= 1440)
                {
                    return $"Heue um {medi.NextDate.ToString("HH:mm")}";
                }
                else
                {
                    return "Heute";
                }
            }

            if (span.Days < 6 && span.Days > 0)// Diese Woche
            {
                return medi.NextDate.ToString("dddd");
            }
            return medi.NextDate.ToString("dd.MM.y");
        }

        public static string GetLastDate(this Medi medi)
        {
            if (medi.LastDate == DateTimeOffset.MinValue)
                return "n/a";

            var now = DateTimeOffset.Now;
            var latest = medi.LastDate;
            var diffDay = now - latest;

            if (now.DayOfWeek == latest.DayOfWeek && diffDay.Days <= 1) // Heute
                return medi.LastDate.ToString("HH:mm");
            
            return medi.LastDate.ToString("dd.MM.y");
        }

        public static string GetDosage(this Medi medi)
            => medi.IntervallType == IntervallType.IfNedded ? "-" : medi.Dosage.ToString("F1");

    }
}
