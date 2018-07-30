﻿using System;
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
                new Medi { Id = Guid.Parse("c3dba569-9d45-4c1f-a6fa-613ef832bf83"), Name = "Enbrel", IntervallType = IntervallType.Intervall, NextDate = DateTimeOffset.Now.AddDays(1), LastDate = DateTimeOffset.Now.AddDays(-2), Dosage = 1.5, Stock = 5.5, MinimumStock = 1, DosageType = MediType.Injection},
                new Medi { Id = Guid.NewGuid(), Name = "MTX", IntervallType = IntervallType.Weekdays, NextDate = DateTimeOffset.Now.AddHours(5), LastDate = DateTimeOffset.Now, Dosage = 1, DosageType = MediType.Fluency},
                new Medi { Id = Guid.NewGuid(), Name = "Paracethamol", IntervallType = IntervallType.IfNedded, LastDate = DateTimeOffset.MinValue, DosageType = MediType.Tablet},
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
