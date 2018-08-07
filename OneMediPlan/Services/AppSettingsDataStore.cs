using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneMediPlan.Models;
using Realms;
using System.Linq;

namespace OneMediPlan.Services
{
    public class AppSettingsDataStore : IDataStore<MediSettings>
    {
        RealmConfiguration _conf;
        public AppSettingsDataStore() { 
             _conf = new RealmConfiguration("default.realm");
            _conf.SchemaVersion = 1;
        }

        public async Task<bool> AddItemAsync(MediSettings item)
        {
            var realm = await Realm.GetInstanceAsync(_conf);
            await realm.WriteAsync((r) => r.Add(item, true));
            return true;
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            var realm = await Realm.GetInstanceAsync(_conf);
            await realm.WriteAsync((r) =>
            {
                var obj = r.Find<MediSettings>(id.ToString());
                r.Remove(obj);
            });

            return true;
        }

        public async Task<MediSettings> GetItemAsync(Guid id)
        {
            var realm = await Realm.GetInstanceAsync(_conf);
            return realm.Find<MediSettings>(id.ToString());
        }

        public async Task<IEnumerable<MediSettings>> GetItemsAsync(bool forceRefresh = false)
        {
            var realm = await Realm.GetInstanceAsync(_conf);
            var all = realm.All<MediSettings>();
            return all;
        }

        public async Task<bool> UpdateItemAsync(MediSettings item)
        {
            var realm =  await Realm.GetInstanceAsync(_conf);
            await realm.WriteAsync((Realm r) =>
            {
                var lastItem = r.All<MediSettings>().First();
                lastItem.Hour = item.Hour;
                lastItem.Minute = item.Minute;
            });
            return true;
        }
    }
}
