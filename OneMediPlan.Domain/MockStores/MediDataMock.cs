using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.b_velop.OneMediPlan.Domain.Services;
using com.b_velop.OneMediPlan.Meta.Interfaces;
using System.Linq;

namespace com.b_velop.OneMediPlan.Domain.MockStores
{
    public class MediDataMock : IDataStore<Medi>
    {
        public static Guid USER_ID = Guid.NewGuid();
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
                    Name = "Metex",
                    Created = DateTimeOffset.Now.AddDays(-2),
                    Stock = 22,
                    MinimumStock = 2
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

        public Task<Medi> UpdateItemAsync(Medi item)
        {
            throw new NotImplementedException();
        }
    }
}
