using Shopping.Shared.Data;
using Shopping.Shared.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Shared.Services.ShoppingList
{
    public interface IShoppingList
    {
        List<ShoppingListModel> GetShoppingListFromItems(List<ShoppingListItem> Items);
        ShoppingListModel GetShoppingListOfDate(DateTime Date, List<ShoppingListItem> Items);
    }
}
