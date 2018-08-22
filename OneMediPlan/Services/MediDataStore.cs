using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using OneMediPlan.Models;
using Plugin.Connectivity;
using Realms;
using System.Linq;

namespace OneMediPlan
{
    public class MediDataStore : IDataStore<Medi>
    {
        IList<Medi> _medis;

        public MediDataStore()
        {
            _medis = new List<Medi>();
        }

        public async Task<IEnumerable<Medi>> GetItemsAsync(bool forceRefresh = false)
        {
            _medis.Clear();
            var realm = await Realm.GetInstanceAsync(App.RealmConf);
            var medis = realm.All<MediSave>();
            foreach (var medi in medis)
            {
                _medis.Add(medi.ToMedi());
            }
            return _medis;
        }

        public async Task<Medi> GetItemAsync(Guid id)
        {
            var med = _medis.SingleOrDefault(m => m.Id == id);
            if (med != null) return med;

            var i = await Realm.GetInstanceAsync(App.RealmConf);
            var o = i.Find<MediSave>(id.ToString());
            return o?.ToMedi();
        }

        public async Task<bool> AddItemAsync(Medi item)
        {
            _medis.Add(item);
            var realm = await Realm.GetInstanceAsync(App.RealmConf);
            var obj = realm.Find("MediSave", item.Id.ToString());
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
            realm.Write(() => realm.Remove(item));
            return true;
        }
    }
}
