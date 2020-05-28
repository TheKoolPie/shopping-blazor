using Microsoft.Extensions.Logging;
using Shopping.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.Services.ShoppingList
{
    public class ShoppingListsApiAccess : IShoppingLists
    {
        private readonly IShoppingListItems _shoppingListItems;
        private readonly ILogger<ShoppingListsApiAccess> _logger;

        public ShoppingListsApiAccess(IShoppingListItems shoppingListItems, ILogger<ShoppingListsApiAccess> logger)
        {
            _shoppingListItems = shoppingListItems;
            _logger = logger;
        }

        public async Task<List<ShoppingListModel>> GetAllAsync()
        {
            return (await _shoppingListItems.GetAllAsync())
                .GroupBy(c => c.CreatedAt)
                .Select(g => new ShoppingListModel(g.Key, g.ToList()))
                .ToList();
        }

        public async Task<ShoppingListModel> GetByDate(DateTime Date)
        {
            var itemsOfDate = (await _shoppingListItems.GetAllAsync())
                .Where(l => l.CreatedAt.Date == Date.Date)
                .ToList();

            return new ShoppingListModel(Date.Date, itemsOfDate);
        }
    }
}
