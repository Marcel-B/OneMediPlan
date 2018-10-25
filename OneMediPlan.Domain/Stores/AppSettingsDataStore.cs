using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.b_velop.OneMediPlan.Meta.Interfaces;

namespace com.b_velop.OneMediPlan.Domain.Stores
{
    public class AppSettingsDataStore : CloudDataStore<AppSettings>
    {
        public AppSettingsDataStore(string backendUrl, ILogger logger = null)
            : base(backendUrl, logger)
        {
        }

        protected override string Route => throw new NotImplementedException();

        protected override string RouteSpecial => throw new NotImplementedException();

        public override Task<IEnumerable<AppSettings>> GetItemsByFkAsync(Guid fk)
        {
            throw new NotImplementedException();
        }
    }
}
