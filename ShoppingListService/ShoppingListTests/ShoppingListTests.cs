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
        public void GetListWithNameCallsGetByName()
        {
            var name = "Aspalls";

            // Act
            var result = this.ShoppingList.GetShoppingList(name);

            // Assert
            this.MemoryStorageServiceMock.Verify(m => m.Get(name), Times.Once);
        }

        [Test]
        public void AddDrinkCallsMemoryServiceAdd()
        {
            var mockDrink = new Drink();

            // Act
            this.ShoppingList.AddDrink(mockDrink.Name, mockDrink.Number.ToString());

            // Assert
            this.MemoryStorageServiceMock.Verify(m => m.Add(It.Is<Drink>(
                d => d.Number == mockDrink.Number && 
                d.Name == mockDrink.Name)), Times.Once);
        }

        [Test]
        public void AddDrinkCallsMemoryServiceAddWithZeroIfNumberInvalid()
        {
            var mockDrink = new Drink
            {
                Name = "Pepsi",
                Number = 2
            };

            // Act
            this.ShoppingList.AddDrink(mockDrink.Name, "Not a number");

            // Assert
            this.MemoryStorageServiceMock.Verify(m => m.Add(It.Is<Drink>(
                d => d.Number == 0 &&
                d.Name == mockDrink.Name)), Times.Once);
        }
    }
}