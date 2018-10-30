using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using com.b_velop.OneMediPlan.Domain.Services;
using com.b_velop.OneMediPlan.Meta.Interfaces;

namespace com.b_velop.OneMediPlan.Domain.MockStores
{
    public class WeekdaysDataMock : IDataStore<Weekdays>
    {
        public static Guid WeekdaysId = Guid.NewGuid();

        public static Weekdays MON_THUE = new Weekdays
        {
            Id = WeekdaysId,
            Sunday = false,
            Monday = true,
            Tuesday = false,
            Wednesday = false,
            Thursday = true,
            Friday = false,
            Saturday = false
        };

        public WeekdaysDataMock(ILogger logger)
        {
            _logger = logger;
            MON_THUE.Medi = MediDataMock.MON_N_THUE;
            Weekdays = new List<Weekdays>
            {
                MON_THUE,
                new Weekdays
                {
                    Id = Guid.NewGuid(),
                    Sunday = false,
                    Monday = true,
                    Tuesday = false,
                    Wednesday = false,
                    Thursday = false,
                    Friday = false,
                    Saturday = false
                }
            };
        }

        ILogger _logger;
        public IList<Weekdays> Weekdays { get; set; }

        public async Task<Weekdays> AddItemAsync(Weekdays item)
        {
            return await Task.Run(() =>
            {
                Weekdays.Add(item);
                return item;
            });
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            _logger.Log($"Delete weekdays '{id}'", GetType());
            return await Task.Run(() =>
            {
                var wd = Weekdays.SingleOrDefault(w => w.Id == id);
                if (wd != null)
                {
                    Weekdays.Remove(wd);
                    return true;
                }
                return false;
            });
        }

        public Task<Weekdays> GetByName(string name)
            => throw new NotImplementedException();

        public async Task<Weekdays> GetItemAsync(Guid id)
        {
            _logger.Log($"Search for Weekdays with fk '{id}'", GetType());
            return await Task.Run(() => Weekdays.SingleOrDefault(w => w.Id == id));
        }

        public async Task<IEnumerable<Weekdays>> GetItemsAsync(bool forceRefresh = false)
        {
            _logger.Log($"Get all weekdays", GetType());
            return await Task.Run(() => Weekdays);
        }

        public async Task<IEnumerable<Weekdays>> GetItemsByFkAsync(Guid fk)
        {
            _logger.Log($"Get weekdays with medi key '{fk}'", GetType());
            return await Task.Run(() => Weekdays.Where(w =>
             {
                 if (w.Medi != null)
                     return w.Medi.Id == fk;
                 return false;
             }));
        }

        public async Task<Weekdays> UpdateItemAsync(Weekdays item)
        {
            _logger.Log($"Update weekdays '{item.Id}'", GetType());
            return await Task.Run(() =>
            {
                var wd = Weekdays.SingleOrDefault(w => w.Id == item.Id);
                if (wd != null)
                    Weekdays.Remove(wd);
                Weekdays.Add(wd);
                return item;
            });

        }
    }
}
