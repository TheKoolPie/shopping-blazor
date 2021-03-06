﻿using Shopping.Shared.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.Services.Interfaces
{
    public interface IShoppingLists : ICRUDAccess<ShoppingList>
    {
        Task<List<ShoppingList>> GetAllOfUserAsync(string userId);
        Task<ShoppingListItem> AddOrUpdateItemAsync(string listId, ShoppingListItem item);
        Task<bool> RemoveItemAsync(string listId, string itemId);
        Task<bool> RemoveItemAsync(string listId, ShoppingListItem item);
        Task<bool> IsOfUserAsync(ShoppingList list, string userId);
        Task<bool> IsOfUserAsync(string listId, string userId);
        Task<bool> DeleteAllOfUser(string userId);
    }
}
