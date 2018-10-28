using System;
using System.Linq;
using com.b_velop.OneMediPlan.Services;
using com.b_velop.OneMediPlan.Domain;
using com.b_velop.OneMediPlan.Domain.Enums;

namespace com.b_velop.OneMediPlan.Helpers
{
    public static class MediExtensions
    {
        public static string Today { get; set; }

        public static string GetStockInfo(this Medi medi)
            => $"{medi.Stock.ToString("F1")}/{medi.MinimumStock.ToString("F1")}";

        public static Medi GetDependend(this Medi medi)
        {
            var target = medi.DependsOn;
            var storage = AppStore.Instance.User.Medis;
            return storage.SingleOrDefault(m => m.Id == target);
        }

        public static bool NeedsNoStartDate(this Medi medi)
        => medi.DependsOn != Guid.Empty ||
               medi.DailyAppointments != null;

        public static bool[] AsArray(this Weekdays days)
        {
            var d = new bool[7];
            d[0] = days.Sunday;
            d[1] = days.Monday  ;
            d[2] = days.Tuesday;
            d[3] = days.Wednesday;
            d[4] = days.Thursday;
            d[5] = days.Friday;
            d[6] = days.Saturday;
            return d;
        }
        public static void CalculateNewWeekdayIntervall(this Medi medi)
        {
            var days = medi.Weekdays.AsArray();
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

        public static int TimeFactor(this Medi medi)
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

        public static int MinutesToNext(this Medi medi)
        {
            var factor = medi.TimeFactor();
            var minutesUntilNext = factor * medi.PureIntervall;
            return minutesUntilNext;
        }

        public static string GetNextDate(this Medi medi)
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

        public static string GetLastDate(this Medi medi)
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

        public static string GetDosage(this Medi medi)
            => medi.IntervallType == IntervallType.IfNedded ? "-" : medi.Dosage.ToString("F1");
    }
}
