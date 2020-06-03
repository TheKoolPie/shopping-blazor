using Shopping.Shared.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopping.Shared.Services
{
    public interface IShoppingListItems : ICRUDAccess<ShoppingListItem>
    {
        Task<List<ShoppingListItem>> GetAllOfUser(string userId);
    }
}
