using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShoppingListService.Contracts;

namespace ShoppingListService.Storage
{
    public interface IMemoryStorageService
    {
        void Add(Drink drink);
        Drink Get(string name);
        List<Drink> GetAll();
        Dictionary<string, Drink> GetDrinkStorage();
    }
}