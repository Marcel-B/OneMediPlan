using OneMediPlan.Models;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Generic;

namespace OneMediPlan.Helpers
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
            var medis = await _store.GetItemsAsync();
            var t = Settings.GetStdTime();
            var now = DateTimeOffset.Now;
            switch (medi.IntervallType)
            {
                case IntervallType.Depend:
                    medi.LastDate = now;
                    medi.NextDate = DateTimeOffset.MinValue;
                    medi.Stock--;
                    SetNotification(medi);
                    await _store.UpdateItemAsync(medi);
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
                    await medi.CalculateNewWeekdayIntervall();
                    medi.NextDate = t.AddDays(medi.PureIntervall);
                    break;
                case IntervallType.DailyAppointment:
                    var h = now.Hour;
                    var m = now.Minute;
                    foreach (var item in medi.DailyAppointments)
                    {
                        if (item.Item1.Value == h && item.Item2.Value >= m)// Treffer
                        {
                            medi.NextDate = new DateTimeOffset(now.Year, now.Month, now.Day, item.Item1.Value, item.Item2.Value, 0, now.Offset);
                            break;
                        }
                        if (item.Item1.Value > h)// Treffer
                        {
                            medi.NextDate = new DateTimeOffset(now.Year, now.Month, now.Day, item.Item1.Value, item.Item2.Value, 0, now.Offset);
                            break;
                        }
                    }
                    if (medi.NextDate < now)// Kein treffer Gefunden, dann ist es der erste
                    {
                        medi.NextDate = new DateTimeOffset(now.Year, now.Month, now.Day, medi.DailyAppointments[0].Item1.Value, medi.DailyAppointments[0].Item2.Value, 0, now.Offset).AddDays(1);
                    }
                    break;
            }
            medi.Stock--;
            medi.LastDate = now;
            SetNotification(medi);
            await _store.UpdateItemAsync(medi);
            await CheckDependencys(medi, medis);
        }

        private async Task CheckDependencys(Medi medi, IEnumerable<Medi> medis)
        {
            var dep = medis.SingleOrDefault(m => m.DependsOn == medi.Id);
            if (dep == null) return; // fertig
            dep.NextDate = medi.LastDate.AddMinutes(dep.IntervallInMinutes);
            SetNotification(dep);
            await _store.UpdateItemAsync(dep);
        }

        private void SetNotification(Medi medi)
            => _setNotification?.Invoke(medi);
    }
}
