using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneMediPlan.Models;

namespace OneMediPlan
{
    //public class WeekdayDataStoreMock : IDataStore<Weekdays>
    //{
    //    IList<Weekdays> weekdays;

    //    public WeekdayDataStoreMock()
    //    {
    //        weekdays = new List<Weekdays>();
    //        weekdays.Add(new Weekdays
    //        {
    //            Id = Guid.Parse("f4282afe-5b1b-450a-ab14-e211301c30a6"),
    //            MediFk = Guid.Parse("c2a6321c-bd83-48fa-a3df-4369834b3782"),
    //            Days = new[] { true, false, false, false, false, true, false }
    //        });
    //    }

    //    async public Task<bool> AddItemAsync(Weekdays item)
    //    {
    //        weekdays.Add(item);
    //        return await Task.FromResult(true);
    //    }

    //    public async Task<bool> DeleteItemAsync(Guid id)
    //    {
    //        var _medi = weekdays.Where((Weekdays arg) => arg.Id == id).FirstOrDefault();
    //        weekdays.Remove(_medi);
    //        return await Task.FromResult(true);
    //    }

    //    public async Task<Weekdays> GetItemAsync(Guid id)
    //    {
    //        return await Task.FromResult(weekdays.FirstOrDefault(s => s.Id == id));
    //    }

    //    public async Task<IEnumerable<Weekdays>> GetItemsAsync(bool forceRefresh = false)
    //    {
    //        return await Task.FromResult(weekdays);
    //    }

    //    public Task SaveStore()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    async public Task<bool> UpdateItemAsync(Weekdays item)
    //    {
    //        var _weekdays = weekdays.Where((Weekdays arg) => arg.Id == item.Id).FirstOrDefault();
    //        weekdays.Remove(_weekdays);
    //        weekdays.Add(item);
    //        return await Task.FromResult(true);
    //    }
    //}

    public class MockDataStore : IDataStore<Medi>
    {
        private List<Medi> medis;

        public MockDataStore()
        {
            var tpls = new List<Tuple<Hour, Minute>>
            {
                new Tuple<Hour, Minute>(new Hour(8), new Minute(0)),
                new Tuple<Hour, Minute>(new Hour(12), new Minute(30)),
                new Tuple<Hour, Minute>(new Hour(22), new Minute(15)),
                new Tuple<Hour, Minute>(new Hour(23), new Minute(15)),
            };
            var n = DateTime.Now;
            var t = new DateTime(n.Year, n.Month, n.Day, tpls[2].Item1.Value, tpls[2].Item2.Value, 0);
            medis = new List<Medi>{
                new Medi {
                    Id = Guid.Parse("c3dba569-9d45-4c1f-a6fa-613ef832bf83"),
                    Name = "Enbrel",
                    IntervallType = IntervallType.Intervall,
                    IntervallTime = IntervallTime.Day,
                    PureIntervall = 7,
                    NextDate = DateTimeOffset.Now.AddDays(2),
                    LastDate = DateTimeOffset.Now.AddDays(-2),
                    Dosage = 1.5,
                    Stock = 5,
                    MinimumStock = 2,
                    DosageType = MediType.Injection
                },
                new Medi {
                    Id = Guid.Parse("c2a6321c-bd83-48fa-a3df-4369834b3782"),
                    Name = "MTX",
                    IntervallType = IntervallType.Weekdays,
                    NextDate = DateTimeOffset.Now,
                    Dosage = 1,
                    DosageType = MediType.Fluency,
                    MinimumStock = 2,
                    Stock = 22
                },
                new Medi {
                    Id = Guid.Parse("7be98ea0-fe14-4ea2-805f-db919ff0c0dc"),
                    Name = "Folsäure",
                    IntervallType = IntervallType.Depend,
                    DependsOn = Guid.Parse("c2a6321c-bd83-48fa-a3df-4369834b3782"),
                    NextDate = DateTimeOffset.MinValue,
                    Dosage = 2,
                    DosageType = MediType.Fluency,
                    IntervallInMinutes = 2 * 24 * 60,
                    Stock = 20,
                    MinimumStock = 3
                },
                new Medi {
                    Id = Guid.Parse("be09d674-85fe-4d2a-9e94-65ece36b4d0e"),
                    Name = "Paracethamol",
                    IntervallType = IntervallType.IfNedded,
                    LastDate = DateTimeOffset.MinValue,
                    DosageType = MediType.Tablet,
                    Stock = 50,
                    MinimumStock = 5
                },
                new Medi {
                    Id = Guid.Parse("be09d674-85fe-4d2a-9e94-65ec3f3b4d0e"),
                    Name = "Daily Medi",
                    IntervallType = IntervallType.DailyAppointment,
                    DailyAppointments = tpls,
                    LastDate = DateTimeOffset.MinValue,
                    NextDate = new DateTimeOffset(t),
                    DosageType = MediType.Tablet,
                    Dosage = 1,
                    Stock = 10,
                    MinimumStock = 1
                },
                new Medi {
                    Id = Guid.Parse("be10d674-85fe-4d2a-9e94-65ec3f3b4d0e"),
                    Name = "In 1 Minute",
                    IntervallType = IntervallType.Intervall,
                    LastDate = DateTimeOffset.MinValue,
                    NextDate = new DateTimeOffset(t),
                    DosageType = MediType.Tablet,
                    PureIntervall = 1,
                    IntervallTime = IntervallTime.Minute,
                    Dosage = 1,
                    Stock = 10,
                    MinimumStock = 1
                },
            };
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

        public Task SaveStore()
        {
            throw new NotImplementedException();
        }
    }
}
