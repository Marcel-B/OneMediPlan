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

namespace OneMediPlan
{
    public class MediDataStore : IDataStore<Medi>
    {
        HttpClient client;
        IEnumerable<Medi> medis;

        public MediDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.BackendUrl}/");

            medis = new List<Medi>();
        }

        public async Task<IEnumerable<Medi>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetStringAsync($"api/item");
                medis = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Medi>>(json));
            }

            return medis;
        }

        public async Task<Medi> GetItemAsync(Guid id)
        {
            var i = await Realm.GetInstanceAsync(App.RealmConf);
            var o = i.Find<MediSave>(id.ToString());
            return o?.ToMedi();
            //return o;
            //if (id != null && CrossConnectivity.Current.IsConnected)
            //{
            //    var json = await client.GetStringAsync($"api/item/{id}");
            //    return await Task.Run(() => JsonConvert.DeserializeObject<Medi>(json));
            //}

            //return null;
        }

        public async Task<bool> AddItemAsync(Medi item)
        {
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
            var medi = await item.Update();
            return medi != null;
            //if (item == null || item.Id == null || !CrossConnectivity.Current.IsConnected)
                //return false;

            //var serializedItem = JsonConvert.SerializeObject(item);
            //var buffer = Encoding.UTF8.GetBytes(serializedItem);
            //var byteContent = new ByteArrayContent(buffer);

            //var response = await client.PutAsync(new Uri($"api/item/{item.Id}"), byteContent);

            //return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            var realm = await Realm.GetInstanceAsync(App.RealmConf);
            var item = realm.Find<MediSave>(id.ToString());
            realm.Write(() => realm.Remove(item));
            return true;
            //if (string.IsNullOrEmpty(id.ToString()) && !CrossConnectivity.Current.IsConnected)
            //    return false;

            //var response = await client.DeleteAsync($"api/item/{id}");

            //return response.IsSuccessStatusCode;
        }
    }
}
