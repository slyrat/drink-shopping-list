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
            // Setup
            this.MemoryStorageServiceMock.Setup(m => m.GetAll()).Returns(new List<Drink>());

            // Act
            var result = this.ShoppingList.GetShoppingList();

            // Assert
            this.MemoryStorageServiceMock.Verify(m => m.GetAll(), Times.Once);
        }

        [Test]
        public void GetListCanPageAndSort()
        {
            // Setup
            this.MemoryStorageServiceMock.Setup(m => m.GetAll()).Returns(new List<Drink>());

            // Act
            var result = this.ShoppingList.GetShoppingListAll(pageSize: 3, page: 1, sortBy: "Name", sortAscending: false);

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

        [Test]
        public void UpdateCallsMemoryUpdate()
        {
            var testName = "test";
            var testNumber = 3;

            // Act
            this.ShoppingList.UpdateDrink(testName, testNumber.ToString());

            // Assert
            this.MemoryStorageServiceMock.Verify(m => m.Update(It.Is<Drink>(
                d => d.Name == testName && d.Number == testNumber)), Times.Once);
        }

        [Test]
        public void UpdateDrinkCallsMemoryServiceUpdateWithZeroIfNumberInvalid()
        {
            var mockDrink = new Drink
            {
                Name = "Pepsi",
                Number = 2
            };

            // Act
            this.ShoppingList.UpdateDrink(mockDrink.Name, "Not a number");

            // Assert
            this.MemoryStorageServiceMock.Verify(m => m.Update(It.Is<Drink>(
                d => d.Number == 0 &&
                d.Name == mockDrink.Name)), Times.Once);
        }

        [Test]
        public void DeleteCallsMemoryServiceCorrectly()
        {
            var nameToDelete = "deleteMe";

            // Act
            this.ShoppingList.Delete(nameToDelete);

            // Assert
            this.MemoryStorageServiceMock.Verify(m => m.Delete(nameToDelete), Times.Once);
        }
    }
}