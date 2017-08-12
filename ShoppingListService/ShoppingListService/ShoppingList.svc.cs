using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ShoppingListService.Contracts;
using ShoppingListService.Storage;

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

        public void AddDrink(Drink drink)
        {
            this.StorageService.Add(drink);
        }

        public List<Drink> GetShoppingList()
        {
            return this.StorageService.GetAll();
        }
    }
}
