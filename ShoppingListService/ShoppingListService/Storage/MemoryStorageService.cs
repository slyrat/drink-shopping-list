using ShoppingListService.Contracts;
using System.Runtime.Caching;
using System.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingListService.Storage
{
    public class MemoryStorageService : IMemoryStorageService
    {
        public MemoryStorageService()
        {
            if (Storage == null)
            {
                Storage = new MemoryCache("DrinkStorage");
            }
        }

        private static MemoryCache Storage { get; set; }

        private static CacheItemPolicy CachePolicy => new CacheItemPolicy();

        public void Add(Drink drink)
        {
            Storage.Add(drink.Name, drink, CachePolicy);
        }

        public Drink Get(string name)
        {
            var result = Storage.Get(name);
            return (result as Drink);
        }

        public List<Drink> GetAll()
        {
            var results = Storage.GetValues(;
            return results.Values.Select(v => (v as Drink)).ToList();
        }
    }
}