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
        public MemoryStorageService() : this(false)
        {

        }

        public MemoryStorageService(bool forceNewCache = false)
        {
            if (Storage == null || forceNewCache)
            {
                Storage = new MemoryCache("DrinkStorage");
            }
        }

        private static MemoryCache Storage { get; set; }

        private static CacheItemPolicy CachePolicy => new CacheItemPolicy();

        private const string DrinkStorageKey = "BestDrinkStorage";

        public void Add(Drink drink)
        {
            var drinkStorage = GetDrinkStorage();
            drinkStorage[drink.Name] = drink;
            Storage[DrinkStorageKey] = drinkStorage;
        }

        public Drink Get(string name)
        {
            var drinkStorage = GetDrinkStorage();
            if (!drinkStorage.ContainsKey(name))
            {
                return null;
            }

            var result = drinkStorage[name];
            return result;
        }

        public List<Drink> GetAll()
        {
            var results = GetDrinkStorage();
            return results.Values.Select(v => (v as Drink)).ToList();
        }

        public virtual Dictionary<string, Drink> GetDrinkStorage()
        {
            var result = (Storage.Get(DrinkStorageKey) as Dictionary<string, Drink>);
            if (result == null)
            {
                result = new Dictionary<string, Drink>();
                Storage.Add(DrinkStorageKey, result, CachePolicy);
            }

            return result;
        }

        public void Update(Drink cider)
        {
            var drinkStorage = GetDrinkStorage();
            if (drinkStorage.ContainsKey(cider.Name))
            {
                drinkStorage[cider.Name].Number = cider.Number;
                Storage[DrinkStorageKey] = drinkStorage;
            }
        }

        public void Delete(string name)
        {
            var drinkStorage = GetDrinkStorage();
            if (drinkStorage.ContainsKey(name))
            {
                drinkStorage.Remove(name);
                Storage[DrinkStorageKey] = drinkStorage;
            }
        }
    }
}