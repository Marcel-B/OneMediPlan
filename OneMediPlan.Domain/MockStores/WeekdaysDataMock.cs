using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.b_velop.OneMediPlan.Domain.Services;
using com.b_velop.OneMediPlan.Meta.Interfaces;
using System.Collections;
using System.Linq;

namespace com.b_velop.OneMediPlan.Domain.MockStores
{
    public class WeekdaysDataMock : IDataStore<Weekdays>
    {
        public static Guid WeekdaysId = Guid.NewGuid();
        public WeekdaysDataMock(ILogger logger)
        {
            _logger = logger;
            Weekdays = new List<Weekdays>{
                new Weekdays{
                    Id = WeekdaysId,
                    Sunday = false,
                    Monday = true,
                    Tuesday = false,
                    Wednesday = false,
                    Thursday = true,
                    Friday = false,
                    Saturday = false
        }
    };
        }
        ILogger _logger;
        public IList<Weekdays> Weekdays { get; set; }

        public Task<Weekdays> AddItemAsync(Weekdays item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Weekdays> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Weekdays> GetItemAsync(Guid id)
        {
            _logger.Log($"Search for Weekdays with fk '{id}'", GetType());
            return await Task.Run(() => Weekdays.SingleOrDefault(w => w.Id == id));
        }

        public Task<IEnumerable<Weekdays>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Weekdays>> GetItemsByFkAsync(Guid fk)
        {
            throw new NotImplementedException();
        }

        public Task<Weekdays> UpdateItemAsync(Weekdays item)
        {
            throw new NotImplementedException();
        }
    }
}
