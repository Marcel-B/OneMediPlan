using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneMediPlan.Models;
using System;
using System.Collections.Generic;

namespace com.b_velop.OneMediPlan.Tests
{
    [TestClass]
    public class MediTests
    {
        private List<Medi> GetMedis(){
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
    }
}
