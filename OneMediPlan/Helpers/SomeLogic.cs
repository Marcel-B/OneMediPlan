using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Generic;

using com.b_velop.OneMediPlan.Domain.Services;
using com.b_velop.OneMediPlan.Domain;
using com.b_velop.OneMediPlan.Domain.Enums;
using com.b_velop.OneMediPlan.Services;

namespace com.b_velop.OneMediPlan.Helpers
{
    public class SomeLogic : ISomeLogic
    {
        private IDataStore<Medi> _store;
        private readonly Action<Medi> _setNotification;
        public SomeLogic(IDataStore<Medi> store, Action<Medi> setNotification)
        {
            _store = store;
            _setNotification = setNotification;
        }

        public async Task HandleIntoke(Medi medi)
        {
            var medis = AppStore.Instance.User.Medis;
            var t = Settings.GetStdTime();
            var now = DateTimeOffset.Now;
            switch (medi.IntervallType)
            {
                case IntervallType.Depend:
                    medi.LastDate = now;
                    medi.NextDate = DateTimeOffset.MinValue;
                    medi.Stock = medi.Stock - medi.Dosage < 0 ? 0 : medi.Stock - medi.Dosage;
                    SetNotification(medi);
                    //await _store.UpdateItemAsync(medi);
                    return;
                case IntervallType.Intervall:
                    switch (medi.IntervallTime)
                    {
                        case IntervallTime.Minute:
                            medi.NextDate = DateTimeOffset.Now.AddMinutes(medi.PureIntervall);
                            break;
                        case IntervallTime.Hour:
                            medi.NextDate = DateTimeOffset.Now.AddHours(medi.PureIntervall);
                            break;
                        case IntervallTime.Day:
                            medi.NextDate = t.AddDays(medi.PureIntervall);
                            break;
                        case IntervallTime.Week:
                            medi.NextDate = t.AddDays(medi.PureIntervall * 7);
                            break;
                        case IntervallTime.Month:
                            medi.NextDate = t.AddMonths(medi.PureIntervall);
                            break;
                    }
                    break;
                case IntervallType.Weekdays:
                    medi.CalculateNewWeekdayIntervall();
                    medi.NextDate = t.AddDays(medi.PureIntervall);
                    break;
                case IntervallType.DailyAppointment:
                    var h = now.Hour;
                    var m = now.Minute;
                    var dailyAppointments = AppStore.Instance.CurrentMedi.DailyAppointments;

                    foreach (var item in dailyAppointments)
                    {
                        if (item.Hour == h && item.Minute >= m)// Treffer
                        {
                            medi.NextDate = new DateTimeOffset(now.Year, now.Month, now.Day, item.Hour, item.Minute, 0, now.Offset);
                            break;
                        }
                        if (item.Hour > h)// Treffer
                        {
                            medi.NextDate = new DateTimeOffset(now.Year, now.Month, now.Day, item.Hour, item.Minute, 0, now.Offset);
                            break;
                        }
                    }
                    if (medi.NextDate < now)// Kein treffer Gefunden, dann ist es der erste
                    {
                        medi.NextDate = new DateTimeOffset(now.Year, now.Month, now.Day, medi.DailyAppointments[0].Hour, medi.DailyAppointments[0].Minute, 0, now.Offset).AddDays(1);
                    }
                    break;
            }
            medi.Stock = medi.Stock - medi.Dosage < 0 ? 0 : medi.Stock - medi.Dosage;
            medi.LastDate = now;
            SetNotification(medi);
            await _store.UpdateItemAsync(medi);
            CheckDependencys(medi, medis);
        }

        private void CheckDependencys(Medi medi, IEnumerable<Medi> medis)
        {
            var dep = medis.SingleOrDefault(m => m.DependsOn == medi.Id);
            if (dep == null) return; // fertig
            var pureInterall = dep.PureIntervall;
            var intervallType = dep.IntervallTime.ToMinutes();

            if (dep.IntervallTime == IntervallTime.Day)
            {
                dep.NextDate = medi.LastDate.AddDays(pureInterall);
            }

            dep.NextDate = medi.LastDate.AddMinutes(pureInterall * intervallType);
            SetNotification(dep);
            //await _store.UpdateItemAsync(dep);
        }

        private void SetNotification(Medi medi)
            => _setNotification?.Invoke(medi);
    }

    public static class IntervallTimeExtensions
    {
        public static int ToMinutes(this IntervallTime intervallTime)
        {
            switch (intervallTime)
            {
                case IntervallTime.Minute:
                    return 1;
                case IntervallTime.Hour:
                    return 60;
                case IntervallTime.Day:
                    return 60 * 24;
                case IntervallTime.Week:
                    return 60 * 24 * 7;
                default:
                    return -1;
            }
        }
    }
}
