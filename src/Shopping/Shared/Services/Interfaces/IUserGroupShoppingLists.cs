using Shopping.Shared.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.Services.Interfaces
{
    public interface IUserGroupShoppingLists : ICRUDAccess<UserGroupShoppingList>
    {
        Task<bool> DeleteAsync(UserGroupShoppingList assignment);
        Task<List<ShoppingList>> GetShoppingListsOfUserGroupAsync(string userGroupId);
        Task<List<UserGroup>> GetUserGroupsOfShoppingListAsync(string shoppingListId);
    }
}
