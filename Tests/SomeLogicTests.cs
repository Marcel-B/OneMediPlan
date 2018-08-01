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
            var sut = new SomeLogic();
            var store = App.Container.Get<MockDataStore>();
            var meds = await store.GetItemsAsync();
            var medis = meds.ToList();
            var medi = await store.GetItemAsync(Guid.Parse("c2a6321c-bd83-48fa-a3df-4369834b3782"));

            sut.HandleIntoke(medi, medis);
            medis.Sort();
        }
    }
}
