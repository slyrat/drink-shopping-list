using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingListService.Paging
{
    public static class PagingExtensions
    {
        public static List<T> PageAndSort<T>(
            this List<T> data, 
            int? pageSize = null, 
            int? page = null, 
            string sortByProperty = null, 
            bool sortAscending = true)
            where T:  new()
        {
            if (!string.IsNullOrWhiteSpace(sortByProperty))
            {
                var propertyInfo = typeof(T).GetProperty(sortByProperty);
                // do sorting
                if (propertyInfo != null)
                {
                    if (sortAscending)
                    {
                        data = data.OrderBy(d => propertyInfo.GetValue(d)).ToList();
                    }
                    else
                    {
                        data = data.OrderByDescending(d => propertyInfo.GetValue(d)).ToList();
                    }
                }
            }

            if (pageSize.HasValue && page.HasValue &&
                page > 0 && pageSize > 0)
            {
                // do paging
                var totalNumber = data.Count;
                // only need to do something if the page is smaller than total
                if (pageSize < totalNumber)
                {
                    // if >= total it means we are trying to start
                    // after the last item in the list
                    if (((page - 1) * pageSize) < totalNumber)
                    {
                        data = data.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();
                    }
                    else
                    {
                        // trying to get beyond end, give them nothing back
                        data = new List<T>();
                    }
                }
                else
                {
                    // trying to get beyond end, give them nothing back
                    data = new List<T>();
                }
            }

            return data;
        }
    }
}