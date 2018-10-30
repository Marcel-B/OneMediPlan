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
        private readonly  ILogger _logger;

        public static MediUser USER = new MediUser
        {
            Id = USER_ID,
            Name = "John Doe"
        };

        public static Medi ON_DEMAND = new Medi
        {
            Id = Guid.NewGuid(),
            Name = "On Demand",
            User = USER,
            Created = DateTimeOffset.Now,
            Stock = 11,
            MinimumStock = 1,
            Dosage = 5,
            IntervallType = IntervallType.IfNeeded,
        };

        public static Medi AFTER_ON_DEMAND = new Medi
        {
            Id = Guid.NewGuid(),
            Name = "After on Demand",
            User = USER,
            Created = DateTimeOffset.Now,
            IntervallType = IntervallType.Depend,
            DependsOn = ON_DEMAND.Id,
            Dosage = 1,
            Stock = 33,
            MinimumStock = 2,
            IntervallTime = IntervallTime.Day,
            PureIntervall = 1
        };

        public static Medi MON_N_THUE = new Medi
        {
            Id = Guid.NewGuid(),
            User = USER,
            Name = "Montag und Donnerstag",
            Created = DateTimeOffset.Now.AddDays(-2),
            Stock = 22,
            MinimumStock = 2,
            Dosage = 1,
            IntervallType = IntervallType.Weekdays,
        };

        public static Medi SEVEN_DAYS = new Medi
        {
            Id = Guid.NewGuid(),
            User = USER,
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
        };

        public static Medi SEVEN_DAYS_1 = new Medi
        {
            Id = Guid.NewGuid(),
            User = USER,
            Name = "7 Tage 1",
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
        };

        public static Medi SEVEN_DAYS_2 = new Medi
        {
            Id = Guid.NewGuid(),
            User = USER,
            Name = "7 Tage 2",
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
        };

        public static Medi APPOINTMENTS = new Medi
        {
            Id = MediAppointmentId,
            User = USER,
            Name = "Appointments",
            Created = DateTimeOffset.Now,
            Stock = 11,
            MinimumStock = 4,
            Dosage = 2,
            IntervallType = IntervallType.DailyAppointment,
            DailyAppointments = new List<DailyAppointment>
            {
                DailyAppointmentDataMock.DAILY_ONE
            }
        };

        public MediDataMock(ILogger logger)
        {
            _logger = logger;
            Medis = new List<Medi>
            {
                SEVEN_DAYS,
                MON_N_THUE,
                APPOINTMENTS,
                ON_DEMAND,
                SEVEN_DAYS_1,
                SEVEN_DAYS_2,
                AFTER_ON_DEMAND,
            };
        }

        public async Task<Medi> AddItemAsync(Medi item)
        {
            return await Task.Run(() => { Medis.Add(item); return item; });
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
            return await Task.Run(() => Medis.Where(m => m.User.Id == fk));
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
