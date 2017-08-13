using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ShoppingListService.Contracts;
using ShoppingListService.Storage;
using ShoppingListService.Paging;

namespace ShoppingListService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ShoppingList : IShoppingList
    {
        private readonly IMemoryStorageService StorageService;

        public ShoppingList()
            : this(new MemoryStorageService())
        {
            
        }

        public ShoppingList(IMemoryStorageService storage)
        {
            this.StorageService = storage;
        }

        public void AddDrink(string name, string number)
        {
            if (!int.TryParse(number, out int amount))
            {
                amount = 0;
            }

            this.StorageService.Add(new Drink
            {
                Name = name,
                Number = amount
            });
        }

        public void Delete(string nameToDelete)
        {
            this.StorageService.Delete(nameToDelete);
        }

        public List<Drink> GetShoppingList(string name = null)
        {
            if (name == null)
            {
                return this.StorageService.GetAll();
            }
            else
            {
                return new List<Drink> { this.StorageService.Get(name) };
            }
        }

        public List<Drink> GetShoppingListAll(
            int pageSize = 0, 
            int page = 0,
            bool sortAscending = true,
            string sortBy = null)
        {
            var result = GetShoppingList();
            return result.PageAndSort(pageSize, page, sortBy, sortAscending);
        }

        public void UpdateDrink(string name, string number)
        {
            if (!int.TryParse(number, out int amount))
            {
                amount = 0;
            }

            this.StorageService.Update(new Drink
            {
                Name = name,
                Number = amount
            });
        }
    }
}
