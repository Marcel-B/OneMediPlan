using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneMediPlan.Models;
using OneMediPlan.Helpers;
using System;
using System.Collections.Generic;
using OneMediPlan;
using Ninject;
using System.Threading.Tasks;

namespace com.b_velop.OneMediPlan.Tests
{
    [TestClass]
    public class MediTests
    {
        private List<Medi> GetMedis()
        {
            return new List<Medi>{
                new Medi{
                    Id = Guid.NewGuid(),
                    NextDate = DateTimeOffset.MinValue,
                    Name = "Three"
                },
                new Medi{
                    Id = Guid.NewGuid(),
                    NextDate = DateTimeOffset.Now.AddDays(5),
                    Name = "Two"
                },
                new Medi{
                    Id = Guid.NewGuid(),
                    NextDate = DateTimeOffset.Now,
                    Name = "One"
                }
            };
        }

        [TestMethod]
        public void MediTests_Sort_MediWithSmallestNextDateWhichIsNotMinValueIsFirstInList()
        {
            // Arrange
            var list = GetMedis();
            var expected = Guid.NewGuid();
            list[2].Id = expected;

            // Act
            list.Sort();

            // Assert
            var actual = list[0];
            Assert.AreEqual(expected, actual.Id);
        }

        [TestMethod]
        public void MediTests_Sort_MediWithMinValueIsLastInList()
        {
            // Arrange
            var list = GetMedis();
            var expected = Guid.NewGuid();
            list[0].Id = expected;

            // Act
            list.Sort();

            // Assert
            var actual = list[2];
            Assert.AreEqual(expected, actual.Id);
        }

        [TestMethod]
        public async Task Medi_GetDependend_ReturnMediWithHasDependenciesOnCurrent()
        {
            // Arrange
            App.Initialize();
            var expected = Guid.Parse("c2a6321c-bd83-48fa-a3df-4369834b3782");
            var store = App.Container.Get<MockDataStore>();
            var medi = await store.GetItemAsync(Guid.Parse("7be98ea0-fe14-4ea2-805f-db919ff0c0dc"));

            // Act
            var mediDepend = await medi.GetDependend();
            var actual = mediDepend.Id;

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
