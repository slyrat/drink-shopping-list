using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ShoppingListService.Contracts;

namespace ShoppingListService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IShoppingList
    {

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "ShoppingList/{name}")]
        List<Drink> GetShoppingList(string name = null);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "ShoppingList?pageSize={pageSize}&page={page}&sortAscending={sortAscending}&sortBy={sortBy}")]
        List<Drink> GetShoppingListAll(int pageSize = 0, int page = 0, bool sortAscending = true, string sortBy = null);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "ShoppingList/{name}/{number}")]
        void AddDrink(string name, string number);

        [OperationContract]
        [WebInvoke(Method = "PUT",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "ShoppingList/{name}/{number}")]
        void UpdateDrink(string name, string number);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "ShoppingList/{nameToDelete}")]
        void Delete(string nameToDelete);
    }
}
