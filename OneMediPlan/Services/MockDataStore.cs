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

        public async Task<bool> AddItemAsync(Medi medi)
        {
            medis.Add(medi);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Medi medi)
        {
            var _medi = medis.Where((Medi arg) => arg.Id == medi.Id).FirstOrDefault();
            medis.Remove(_medi);
            medis.Add(medi);

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


    //public class MockDataStoreItem : IDataStore<Item>
    //{
    //    List<Item> items;

    //    public MockDataStoreItem()
    //    {
    //        items = new List<Item>();
    //        var _items = new List<Item>
    //        {
    //            new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is a nice description"},
    //            new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is a nice description"},
    //            new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is a nice description"},
    //            new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is a nice description"},
    //            new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is a nice description"},
    //            new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is a nice description"},
    //        };

    //        foreach (Item item in _items)
    //        {
    //            items.Add(item);
    //        }
    //    }

    //    public async Task<bool> AddItemAsync(Item item)
    //    {
    //        items.Add(item);

    //        return await Task.FromResult(true);
    //    }

    //    public async Task<bool> UpdateItemAsync(Item item)
    //    {
    //        var _item = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
    //        items.Remove(_item);
    //        items.Add(item);

    //        return await Task.FromResult(true);
    //    }

    //    public async Task<bool> DeleteItemAsync(string id)
    //    {
    //        var _item = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
    //        items.Remove(_item);

    //        return await Task.FromResult(true);
    //    }

    //    public async Task<Item> GetItemAsync(string id)
    //    {
    //        return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
    //    }

    //    public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
    //    {
    //        return await Task.FromResult(items);
    //    }
    //}
}
