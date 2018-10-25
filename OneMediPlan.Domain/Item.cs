using System;
using com.b_velop.OneMediPlan.Domain.Services;
namespace com.b_velop.OneMediPlan.Domain
{
    public abstract class Item : IItem
    {
        public Guid Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset LastEdit { get; set; }
        public string Description { get; set; }

    }
}
