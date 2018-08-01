using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using OneMediPlan.Models;
//using Plugin.Connectivity;

namespace OneMediPlan
{
    public class CloudDataStore : IDataStore<Medi>
    {
        HttpClient client;
        IEnumerable<Medi> medis;

        public CloudDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.BackendUrl}/");

            medis = new List<Medi>();
        }

        public async Task<IEnumerable<Medi>> GetItemsAsync(bool forceRefresh = false)
        {
            //if (forceRefresh && CrossConnectivity.Current.IsConnected)
            //{
                var json = await client.GetStringAsync($"api/item");
                medis = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Medi>>(json));
            //}

            return medis;
        }

        public async Task<Medi> GetItemAsync(Guid id)
        {
            //if (id != null && CrossConnectivity.Current.IsConnected)
            //{
                var json = await client.GetStringAsync($"api/item/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Medi>(json));
            //}

            //return null;
        }

        public async Task<bool> AddItemAsync(Medi item)
        {
            //if (item == null || !CrossConnectivity.Current.IsConnected)
                //return false;

            var serializedItem = JsonConvert.SerializeObject(item);

            var response = await client.PostAsync($"api/item", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(Medi item)
        {
            //if (item == null || item.Id == null || !CrossConnectivity.Current.IsConnected)
                //return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/item/{item.Id}"), byteContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            //if (string.IsNullOrEmpty(id.ToString()) && !CrossConnectivity.Current.IsConnected)
                //return false;

            var response = await client.DeleteAsync($"api/item/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
