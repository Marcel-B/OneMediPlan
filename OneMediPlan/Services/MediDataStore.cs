﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneMediPlan.Helpers;
using OneMediPlan.Models;
using Realms;

namespace OneMediPlan
{
    public interface IMediDataStore : IDataStore<Medi>
    {
        Medi GetTemporaryMedi();
        void SetTemporaryMedi(Medi medi);
    }

    public class MediDataStore : IMediDataStore
    {
        readonly IList<Medi> _medis;
        Medi _temporaryMedi;

        public MediDataStore()
        {
            _medis = new List<Medi>();
            var realm = Realm.GetInstance(App.RealmConf);
            var medis = realm.All<MediSave>();
            foreach (var medi in medis)
                _medis.Add(medi.ToMedi());
        }

        public async Task<IEnumerable<Medi>> GetItemsAsync(bool forceRefresh = false)
        {
            _medis.Clear();
            var realm = await Realm.GetInstanceAsync(App.RealmConf);
            var medis = realm.All<MediSave>();
            foreach (var medi in medis)
                _medis.Add(medi.ToMedi());
            return _medis;
        }

        public async Task<Medi> GetItemAsync(Guid id)
        {
            var med = _medis.SingleOrDefault(m => m.Id == id);
            if (med != null) return med;
            var i = await Realm.GetInstanceAsync(App.RealmConf);
            var o = i.Find<MediSave>(id.ToString());
            return o?.ToMedi() ?? med;
        }

        public async Task<bool> AddItemAsync(Medi item)
        {
            var contains = _medis.SingleOrDefault(m => m.Id == item.Id);
            if (contains != null)
                _medis[_medis.IndexOf(contains)] = contains;
            else
                _medis.Add(item);
            var realm = await Realm.GetInstanceAsync(App.RealmConf);
            var obj = realm.Find<MediSave>(item.Id.ToString());
            MediSave medi;
            if (obj == null)
                medi = await item.Save();
            else
                medi = await item.Update();
            return medi != null;
        }

        public async Task<bool> UpdateItemAsync(Medi item)
        {
            var med = _medis.SingleOrDefault(m => m.Id == item.Id);
            _medis[_medis.IndexOf(med)] = item;

            var medi = await item.Update();
            return medi != null;
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            var med = _medis.SingleOrDefault(m => m.Id == id);
            _medis.Remove(med);
            var realm = await Realm.GetInstanceAsync(App.RealmConf);
            var item = realm.Find<MediSave>(id.ToString());
            var weekdays = realm.All<Weekdays>();
            var idStr = id.ToString();

            var dailyAppointments = realm
                .All<DailyAppointment>()
                .Where(d => d.MediFk.Equals(idStr));

            var weekday = weekdays.SingleOrDefault(w => w.MediFk.Equals(idStr));

            realm.Write(() =>
            {
                realm.Remove(item);
                if (weekday != null)
                    realm.Remove(weekday);
                realm.RemoveRange(dailyAppointments);
            });
            return true;
        }

        public async Task SaveStore()
        {
            foreach (var medi in _medis)
                await medi.Save();
            return;
        }

        public Medi GetTemporaryMedi()
            => _temporaryMedi ?? new Medi { Create = DateTimeOffset.Now };

        public void SetTemporaryMedi(Medi medi)
            => _temporaryMedi = medi;
    }
}
