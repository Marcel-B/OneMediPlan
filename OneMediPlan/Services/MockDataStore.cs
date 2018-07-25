using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneMediPlan.Models;

namespace OneMediPlan
{
    public class MockDataStore : IDataStore<Medi>
    {
        List<Medi> medis;

        public MockDataStore()
        {
            medis = new List<Medi>();
            var _medis = new List<Medi>
            {
                new Medi { Id = Guid.NewGuid(), Name = "First item"},
                new Medi { Id = Guid.NewGuid(), Name = "Second item"},
                new Medi { Id = Guid.NewGuid(), Name = "Third item"},
                new Medi { Id = Guid.NewGuid(), Name = "Fourth item"},
                new Medi { Id = Guid.NewGuid(), Name = "Fifth item"},
                new Medi { Id = Guid.NewGuid(), Name = "Sixth item"},
            };

            foreach (Medi medi in _medis)
            {
                medis.Add(medi);
            }
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
        {
            return await Task.FromResult(medis.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Medi>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(medis);
        }
    }
}
