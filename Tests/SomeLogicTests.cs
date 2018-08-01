using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;
using OneMediPlan.Helpers;
using OneMediPlan;
using Ninject;
using System.Threading.Tasks;
using System.Linq;

namespace com.b_velop.OneMediPlan.Tests
{
    [TestClass]
    public class SomeLogicTests
    {
        [TestMethod]
        public async Task SomeLogic_Foo_Bar()
        {
            App.Initialize();
            var sut = App.Container.Get<SomeLogic>();
            var store = App.Container.Get<MockDataStore>();

            var medi = await store.GetItemAsync(Guid.Parse("c2a6321c-bd83-48fa-a3df-4369834b3782"));

            await sut.HandleIntoke(medi);
            var ustore = App.Container.Get<MockDataStore>();

            var meds = await ustore.GetItemsAsync();
            var medis = meds.ToList();

            await medi.CalculateNewWeekdayIntervall();
        }
    }
}
