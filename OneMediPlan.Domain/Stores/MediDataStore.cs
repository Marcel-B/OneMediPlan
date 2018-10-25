using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.b_velop.OneMediPlan.Meta.Interfaces;
using Newtonsoft.Json;

namespace com.b_velop.OneMediPlan.Domain.Stores
{
    public class MediDataStore : CloudDataStore<Medi>
    {
        public MediDataStore(string backendUrl, ILogger logger = null)
            : base(backendUrl, logger)
        {
            Items = new List<Medi>();
        }

        protected override string Route => $"api/medi/";
        protected override string RouteSpecial => $"api/medi/byuser/";

        public override async Task<IEnumerable<Medi>> GetItemsByFkAsync(Guid fk)
        {
            try
            {
                var response = await Client.GetAsync(RouteSpecial + fk);
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                var trips = JsonConvert.DeserializeObject<IEnumerable<Medi>>(content);
                return trips;
            }
            catch (Exception ex)
            {
                logger.Log($"Error while GetItemsByFkAsync in MediDataStore with UserId: '{fk}'{Environment.NewLine}Exception Message: '{ex.Message}'", GetType(), ex);
                //System.Diagnostics.Debug.WriteLine($"Error while GetItemsByFkAsync in TripDataStore with UserId: '{fk}'{Environment.NewLine}Exception Message: '{ex.Message}'");
                return null;
            }
        }
    }
}
