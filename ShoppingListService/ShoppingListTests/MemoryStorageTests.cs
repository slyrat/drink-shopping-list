using System;
using NUnit.Framework;
using ShoppingListService.Storage;
using ShoppingListService.Contracts;
using System.Collections.Generic;
using Moq;

namespace ShoppingListTests
{
    [TestFixture]
    public class MemoryStorageTests
    {
        public IMemoryStorageService Storage { get; private set; }

        [SetUp]
        public void Setup()
        {
            this.Storage = new MemoryStorageService();
        }

        [Test]
        public void StorageCanGetDrinkDict()
        {
            // Act
            Dictionary<string, Drink> result = Storage.GetDrinkStorage();

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void StorageCanSaveItem()
        {
            // Just test that no exceptions happen
            Storage.Add(new Drink());
        }

        [Test]
        public void AddingTwoItemsWithSameNameStoresLastAddedItem()
        {
            var firstDrink = new Drink
            {
                Name = "Aspalls",
                Number = 5
            };
            var secondDrink = new Drink
            {
                Name = "Aspalls",
                Number = 2
            };

            // Act
            this.Storage.Add(firstDrink);
            this.Storage.Add(secondDrink);
            var result = this.Storage.Get(firstDrink.Name);

            // Assert
            Assert.AreEqual(secondDrink, result);
        }

        [Test]
        public void AddItemToStorageCallsGetDrinkStorage()
        {
            var MockMemStorageService = new Mock<MemoryStorageService>
            {
                CallBase = true
            };

            // Act
            MockMemStorageService.Object.Add(new Drink());

            // Assert
            MockMemStorageService.Verify(mm => mm.GetDrinkStorage(), Times.Once);
        }

        [Test]
        public void StorageCanSaveAndRetrieveItem()
        {
            var cider = new Drink
            {
                Name = "Aspalls",
                Number = 2
            };

            Storage.Add(cider);

            // Act
            Drink result = Storage.Get(cider.Name);

            // Assert
            Assert.AreEqual(cider, result);
        }

        [Test]
        public void StorageCanSaveMultipleItemsAndGetList()
        {
            var cider = new Drink
            {
                Name = "Aspalls",
                Number = 2
            };
            var ale = new Drink
            {
                Name = "Boddingtons",
                Number = 3
            };

            Storage.Add(cider);
            Storage.Add(ale);

            // Act
            var result = Storage.GetAll();

            // Assert
            Assert.Contains(ale, result);
            Assert.Contains(cider, result);
        }
    }
}
