using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using com.b_velop.OneMediPlan.Domain.Services;
using com.b_velop.OneMediPlan.Meta.Interfaces;
using System.Collections;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace com.b_velop.OneMediPlan.Domain.MockStores
{
    public class DailyAppointmentDataMock : IDataStore<DailyAppointment>
    {
        public static Guid DailyAppId = Guid.NewGuid();
        public static DailyAppointment DAILY_ONE = new DailyAppointment
        {
            Id = DailyAppId,
            Hour = 15,
            Minute = 22,
            Created = DateTimeOffset.Now,
            LastEdit = DateTimeOffset.Now,
            Medi = MediDataMock.APPOINTMENTS
        };
        public DailyAppointmentDataMock(ILogger logger)
        {
            _logger = logger;
            DAILY_ONE.Medi = MediDataMock.APPOINTMENTS;
            DailyAppointments = new List<DailyAppointment>
            {
                DAILY_ONE
            };
        }

        ILogger _logger;
        public IList<DailyAppointment> DailyAppointments { get; set; }

        public Task<DailyAppointment> AddItemAsync(DailyAppointment item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<DailyAppointment> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<DailyAppointment> GetItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DailyAppointment>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DailyAppointment>> GetItemsByFkAsync(Guid fk)
        {
            _logger.Log($"Now searching for appointments with MediFk '{fk}'", GetType());
            return await Task.Run(() =>
                                  DailyAppointments.Where(da => da.Medi.Id == fk));
        }

        public Task<DailyAppointment> UpdateItemAsync(DailyAppointment item)
        {
            throw new NotImplementedException();
        }
    }
}
