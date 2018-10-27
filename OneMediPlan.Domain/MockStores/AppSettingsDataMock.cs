using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using com.b_velop.OneMediPlan.Domain.Services;
using com.b_velop.OneMediPlan.Meta.Interfaces;

namespace com.b_velop.OneMediPlan.Domain.MockStores
{
    public class AppSettingsDataMock : IDataStore<AppSettings>
    {
        public List<AppSettings> AppSettings { get; set; }
        ILogger _logger;

        public AppSettingsDataMock(ILogger logger)
        {
            _logger = logger;
            AppSettings = new List<AppSettings>();
        }

        public async Task<AppSettings> AddItemAsync(AppSettings item)
        {
            await Task.Run(() =>
            {
                AppSettings.Add(item);
                _logger.Log($"AppSettings item '{item.Id}' added.", GetType());
            });
            return item;
        }

        public Task<bool> DeleteItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<AppSettings> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<AppSettings> GetItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AppSettings>> GetItemsAsync(bool forceRefresh = false)
        {
            var settings = await Task.Run(() => AppSettings);
            _logger.Log($"Returned {settings.Count} values.", GetType());
            return settings;
        }

        public Task<IEnumerable<AppSettings>> GetItemsByFkAsync(Guid fk)
        {
            throw new NotImplementedException();
        }

        public async Task<AppSettings> UpdateItemAsync(AppSettings item)
        {
            await Task.Run(() =>
            {
                var i = AppSettings.SingleOrDefault(s => s.Id == item.Id);
                AppSettings.Remove(i);
                AppSettings.Add(item);
                _logger.Log($"AppSetting item '{item.Id}' updated.", GetType());
            });
            return item;
        }
    }
}
