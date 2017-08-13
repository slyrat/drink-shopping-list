using System;
using NUnit.Framework;
using ShoppingListService.Storage;
using ShoppingListService.Contracts;
using System.Collections.Generic;
using Moq;
using ShoppingListService.Paging;

namespace ShoppingListTests
{
    [TestFixture]
    public class PagingExtensionsTests
    {
        /// <summary>
        /// Gets a list of 5 drinks
        /// </summary>
        /// <returns></returns>
        public List<Drink> GetDrinkData()
        {
            return new List<Drink>
            {
                new Drink
                {
                    Name = "Coke",
                    Number = 4
                },
                new Drink
                {
                    Name = "Pepsi",
                    Number = 5
                },
                new Drink
                {
                    Name = "Aspalls",
                    Number = 6
                },
                new Drink
                {
                    Name = "Strongbow",
                    Number = 2
                },
                new Drink
                {
                    Name = "Guinness",
                    Number = 1
                }
            };
        }

        [Test]
        public void PageAndSort_ReturnsAllItemsWithNoPagingParams()
        {
            var data = GetDrinkData();
            var initialCount = data.Count;

            // Act
            data.PageAndSort();

            // Assert
            Assert.AreEqual(initialCount, data.Count);
        }

        [Test]
        public void PageAndSort_SortsByThePropertyGiven()
        {
            var data = GetDrinkData();
            var firstItem = data[0];

            // Act
            data = data.PageAndSort(sortByProperty: nameof(firstItem.Name));

            // Assert
            Assert.AreEqual("Aspalls", data[0].Name);
        }

        [TestCase(1, 1, 1, "Coke")]
        [TestCase(2, 1, 2, "Coke")]
        [TestCase(2, 2, 2, "Aspalls")]
        [TestCase(2, 3, 1, "Guinness")]
        [TestCase(3, 3, 0, "")] // Assumes that if you try to get beyond end, just give them empty list
        public void PageAndSort_PagesCorrectly(int pageSize, int page, int ExpectedCount, string ExpectedNameOfFirstItem)
        {
            var data = GetDrinkData();
            
            // Act
            data = data.PageAndSort(page: page, pageSize: pageSize);

            // Assert
            Assert.AreEqual(ExpectedCount, data.Count, "count did not match");
            if (ExpectedCount > 0)
            {
                Assert.AreEqual(ExpectedNameOfFirstItem, data[0].Name);
            }
        }
    }
}
