using Shopping.Shared.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.Services.Interfaces
{
    public interface IUserGroupShoppingLists
    {
        Task<bool> CreateAssignmentAsync(UserGroupShoppingList assignment);
        Task<bool> RemoveAssignmentAsync(UserGroupShoppingList assignment);
        Task<bool> RemoveAssignmentsOfGroupAsync(string userGroupId);
        Task<bool> RemoveAssignmentsOfShoppingListAsync(string shoppingListId);
        Task<List<ShoppingList>> GetShoppingListsOfUserGroupAsync(string userGroupId);
        Task<List<UserGroup>> GetUserGroupsOfShoppingListAsync(string shoppingListId);
    }
}
