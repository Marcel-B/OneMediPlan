using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace com.b_velop.OneMediPlan.Domain.Services
{
    public interface IDataStore<T> where T : IItem
    {
        /// <summary>
        /// Add a Item to DataBase
        /// </summary>
        /// <param name="item">The Item itselfe</param>
        /// <returns></returns>
        Task<T> AddItemAsync(T item);

        /// <summary>
        /// Update the Item in DataBase
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<T> UpdateItemAsync(T item);

        /// <summary>
        /// Delete the Item in DataBase
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteItemAsync(Guid id);

        Task<T> GetItemAsync(Guid id);

        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);

        /// <summary>
        /// Get Items by ForeignKey
        /// </summary>
        /// <param name="fk">The Foreign Key</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetItemsByFkAsync(Guid fk);

        Task<T> GetByName(string name);
    }
}
