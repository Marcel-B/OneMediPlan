using System;
namespace com.b_velop.OneMediPlan.Domain.Services
{
    public interface IItem
    {
        Guid Id { get; set; }
        DateTimeOffset Created { get; set; }
        DateTimeOffset LastEdit { get; set; }
        string Description { get; set; }
    }
}
