using Shopping.Shared.Data;
using Shopping.Shared.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Shared.Services
{
    public interface IShoppingLists
    {
        List<ShoppingListModel> GetAllAsync();
        ShoppingListModel GetByDate(DateTime Date);
    }
}
