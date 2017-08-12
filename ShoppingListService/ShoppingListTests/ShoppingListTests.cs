using System;
using NUnit.Framework;
using ShoppingListService.Storage;
using ShoppingListService.Contracts;
using ShoppingListService;
using System.Collections.Generic;
using Moq;

namespace ShoppingListTests
{
    /// <summary>
    /// Tests the Shopping List service calls
    /// </summary>
    [TestFixture]
    public class ShoppingListTests
    {
        public Mock<IMemoryStorageService> MemoryStorageServiceMock { get; private set; }
        public IShoppingList ShoppingList { get; private set; }

        [SetUp]
        public void Setup()
        {
            this.MemoryStorageServiceMock = new Mock<IMemoryStorageService>();

            this.ShoppingList = new ShoppingList(MemoryStorageServiceMock.Object);
        }

        [Test]
        public void GetListCallsMemoryService()
        {
            // Act
            var result = this.ShoppingList.GetShoppingList();

            // Assert
            this.MemoryStorageServiceMock.Verify(m => m.GetAll(), Times.Once);
        }

        [Test]
        public void AddDrinkCallsMemoryServiceAdd()
        {
            var mockDrink = new Drink();

            // Act
            this.ShoppingList.AddDrink(mockDrink);

            // Assert
            this.MemoryStorageServiceMock.Verify(m => m.Add(mockDrink), Times.Once);
        }
    }
}