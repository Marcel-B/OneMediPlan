using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using com.b_velop.OneMediPlan.Domain.Services;
using com.b_velop.OneMediPlan.Meta.Interfaces;

namespace com.b_velop.OneMediPlan.Domain.Stores
{
    public abstract class CloudDataStore<T> : IDataStore<T> where T : IItem
    {
        protected CloudDataStore(string backendUrl, ILogger logger = null)
        {
            this.logger = logger;
            if (Client == null)
                Client = new HttpClient
                {
                    BaseAddress = new Uri($"{backendUrl}/")
                };
        }

        protected ILogger logger;

        protected HttpClient Client;
        protected IEnumerable<T> Items;
        protected abstract string Route { get; }
        protected abstract string RouteSpecial { get; }
        public abstract Task<IEnumerable<T>> GetItemsByFkAsync(Guid fk);

        public async Task<T> UpdateItemAsync(T item)
        {
            try
            {
                var serializedItem = JsonConvert.SerializeObject(item);
                var cnt = new StringContent(serializedItem, Encoding.UTF8, "application/json");
                var buffer = Encoding.UTF8.GetBytes(serializedItem);
                var byteContent = new ByteArrayContent(buffer);
                var response = await Client.PutAsync(Route + item.Id, cnt);
                var succ = response.IsSuccessStatusCode;
                var result = await response.Content.ReadAsStreamAsync();

                return response.IsSuccessStatusCode ? item : default(T);
            }
            catch (Exception ex)
            {
                logger?.Log(ex.Message, GetType(), ex);
                return default(T);
            }
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            try
            {
                var response = await Client.DeleteAsync(Route + id);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                logger?.Log(ex.Message, GetType(), ex);
                return false;
            }
        }

        public async Task<T> GetItemAsync(Guid id)
        {
            try
            {
                var response = await Client.GetAsync(Route + id);
                if (!response.IsSuccessStatusCode) return default(T);
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                logger?.Log(ex.Message, GetType(), ex);
                return default(T);
            }
        }

        public async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            try
            {
                var json = await Client.GetStringAsync(Route);
                Items = JsonConvert.DeserializeObject<T[]>(json);
                return Items;
            }
            catch (Exception ex)
            {
                logger?.Log(ex.Message, GetType(), ex);
                return null;
            }
        }

        public async Task<T> AddItemAsync(T item)
        {
            try
            {
                var serializedItem = JsonConvert.SerializeObject(item);
                var content = new StringContent(serializedItem, Encoding.UTF8, "application/json");
                var response = await Client.PostAsync(Route, content);
                return response.IsSuccessStatusCode ? item : default(T);
            }
            catch (Exception ex)
            {
                logger?.Log(ex.Message, GetType(), ex);
                return default(T);
            }
        }

        public virtual Task<T> GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
