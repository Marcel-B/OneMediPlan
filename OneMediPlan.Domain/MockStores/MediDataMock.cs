using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.b_velop.OneMediPlan.Domain.Services;
using com.b_velop.OneMediPlan.Meta.Interfaces;
using System.Linq;
using com.b_velop.OneMediPlan.Domain.Enums;
using System.Diagnostics;

namespace com.b_velop.OneMediPlan.Domain.MockStores
{
    public class MediDataMock : IDataStore<Medi>
    {
        public static Guid USER_ID = Guid.NewGuid();
        public static Guid MediAppointmentId = Guid.NewGuid();

        public List<Medi> Medis { get; set; }
        private ILogger _logger;

        public MediDataMock(ILogger logger)
        {
            _logger = logger;
            Medis = new List<Medi>
            {
                new Medi{
                    Id = Guid.NewGuid(),
                    UserFk = USER_ID,
                    Name = "7 Tage",
                    Created = DateTimeOffset.Now.AddDays(-2),
                    LastEdit = DateTimeOffset.Now,
                    Stock = 22,
                    MinimumStock = 2,
                    NextDate = DateTimeOffset.Now,
                    //NextDate = DateTimeOffset.Now.AddDays(1),
                    Dosage = 1,
                    IntervallTime = IntervallTime.Day,
                    PureIntervall = 7,
                    IntervallType = IntervallType.Intervall
                },
                new Medi{
                    Id = Guid.NewGuid(),
                    UserFk = USER_ID,
                    Name = "Montag und Donnerstag",
                    Created = DateTimeOffset.Now.AddDays(-2),
                    Stock = 22,
                    MinimumStock = 2,
                    Dosage = 1,
                    IntervallType = IntervallType.Weekdays,
                    WeekdaysFk = WeekdaysDataMock.WeekdaysId,
                },
                new Medi{
                    Id = MediAppointmentId,
                    UserFk = USER_ID,
                    Name = "appointments",
                    Created = DateTimeOffset.Now,
                    Stock = 11,
                    MinimumStock = 4,
                    Dosage = 2,
                    IntervallType = IntervallType.DailyAppointment,
                }
            };
        }

        public Task<Medi> AddItemAsync(Medi item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Medi> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Medi> GetItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Medi>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Medi>> GetItemsByFkAsync(Guid fk)
        {
            _logger.Log($"Now fetching Medis from user '{fk}'", GetType());
            return await Task.Run(() => Medis.Where(m => m.UserFk == fk));
        }

        public async Task<Medi> UpdateItemAsync(Medi item)
        {
            _logger.Log($"Medi '{item.Id}' updated.", GetType());
            var medi = await Task.Run(() => Medis.SingleOrDefault(m => m.Id == item.Id));
            Medis.Remove(medi);
            Medis.Add(item);
            return item;
        }
    }
}
