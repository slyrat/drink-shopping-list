using System;
using NUnit.Framework;
using ShoppingListService.Storage;
using ShoppingListService.Contracts;
using System.Collections.Generic;

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
        public void StorageCanSaveItem()
        {
            // Just test that no exceptions happen
            Storage.Add(new Drink());
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
