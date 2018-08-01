using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneMediPlan.Models;

namespace OneMediPlan
{
    public class WeekdayDataStoreMock : IDataStore<Weekdays>
    {
        IList<Weekdays> weekdays;

        public WeekdayDataStoreMock()
        {
            weekdays = new List<Weekdays>();
            weekdays.Add(new Weekdays
            {
                Id = Guid.Parse("f4282afe-5b1b-450a-ab14-e211301c30a6"),
                MediFk = Guid.Parse("c2a6321c-bd83-48fa-a3df-4369834b3782"),
                Days = new[] { true, false, false, false, false, true, false }
            });
        }

        async public Task<bool> AddItemAsync(Weekdays item)
        {
            weekdays.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            var _medi = weekdays.Where((Weekdays arg) => arg.Id == id).FirstOrDefault();
            weekdays.Remove(_medi);
            return await Task.FromResult(true);
        }

        public async Task<Weekdays> GetItemAsync(Guid id)
        {
            return await Task.FromResult(weekdays.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Weekdays>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(weekdays);
        }

        async public Task<bool> UpdateItemAsync(Weekdays item)
        {
            var _weekdays = weekdays.Where((Weekdays arg) => arg.Id == item.Id).FirstOrDefault();
            weekdays.Remove(_weekdays);
            weekdays.Add(item);
            return await Task.FromResult(true);
        }
    }

    public class MockDataStore : IDataStore<Medi>
    {
        List<Medi> medis;

        public MockDataStore()
        {
            medis = new List<Medi>();
            var _medis = new List<Medi>
            {
                new Medi { Id = Guid.Parse("c3dba569-9d45-4c1f-a6fa-613ef832bf83"), Name = "Enbrel", IntervallType = IntervallType.Intervall, NextDate = DateTimeOffset.Now.AddDays(2), LastDate = DateTimeOffset.Now.AddDays(-2), Dosage = 1.5, Stock = 5.5, MinimumStock = 1, DosageType = MediType.Injection},
                new Medi { Id = Guid.Parse("c2a6321c-bd83-48fa-a3df-4369834b3782"), Name = "MTX", IntervallType = IntervallType.Weekdays, NextDate = DateTimeOffset.Now.AddHours(5), LastDate = DateTimeOffset.Now, Dosage = 1, DosageType = MediType.Fluency},
                new Medi { Id = Guid.Parse("7be98ea0-fe14-4ea2-805f-db919ff0c0dc"), Name = "Folsäure", IntervallType = IntervallType.Depend, DependsOn = Guid.Parse("c2a6321c-bd83-48fa-a3df-4369834b3782"), NextDate = DateTimeOffset.MinValue, LastDate = DateTimeOffset.Now, Dosage = 2, DosageType = MediType.Fluency},
                new Medi { Id = Guid.Parse("be09d674-85fe-4d2a-9e94-65ece36b4d0e"), Name = "Paracethamol", IntervallType = IntervallType.IfNedded, LastDate = DateTimeOffset.MinValue, DosageType = MediType.Tablet},
            };

            foreach (Medi medi in _medis)
                medis.Add(medi);
        }

        public async Task<bool> AddItemAsync(Medi item)
        {
            medis.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Medi item)
        {
            var _medi = medis.Where((Medi arg) => arg.Id == item.Id).FirstOrDefault();
            medis.Remove(_medi);
            medis.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            var _medi = medis.Where((Medi arg) => arg.Id == id).FirstOrDefault();
            medis.Remove(_medi);

            return await Task.FromResult(true);
        }

        public async Task<Medi> GetItemAsync(Guid id)
            => await Task.FromResult(medis.FirstOrDefault(s => s.Id == id));

        public async Task<IEnumerable<Medi>> GetItemsAsync(bool forceRefresh = false)
            => await Task.FromResult(medis);
    }
}
