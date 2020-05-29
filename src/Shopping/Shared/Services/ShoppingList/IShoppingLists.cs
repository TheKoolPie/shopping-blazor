using Shopping.Shared.Data;
using Shopping.Shared.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.Services
{
    public interface IShoppingLists
    {
        Task<List<ShoppingListModel>> GetAllAsync();
        Task<ShoppingListModel> GetByDate(DateTime Date);

        Task<ShoppingListModel> SaveShoppingList(ShoppingListModel model);
    }
}
