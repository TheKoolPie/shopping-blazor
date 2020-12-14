using Microsoft.Extensions.Logging;
using Shopping.Client.Services.Implementations.Base;
using Shopping.Shared.Data;
using Shopping.Shared.Results;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Shopping.Client.Services.Implementations
{
    public class ShoppingListsApiAccess : BaseShoppingApiImpl<ShoppingList, ShoppingListResult>, IShoppingLists
    {
        public ShoppingListsApiAccess(IAuthService authService, ILogger<ShoppingListsApiAccess> logger)
            : base("ShoppingLists", authService, logger)
        {
        }

        public async Task<ShoppingListItem> AddOrUpdateItemAsync(string listId, ShoppingListItem item)
        {
            var client = await GetApiClient();
            ShoppingListItem createdItem = null;
            var response = await client.PostAsJsonAsync<ShoppingListItem>($"{_baseUri}/AddItem/{listId}", item);
            if (response.IsSuccessStatusCode)
            {
                createdItem = await response.Content.ReadFromJsonAsync<ShoppingListItem>();
            }
            else
            {
                _logger.LogError($"Could not add item to list {listId}");
            }
            return createdItem;
        }

        public Task<UserGroup> AddUserGroupAsync(string listId, string userGroupId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllOfUser(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ShoppingList>> GetAllOfUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsOfUserAsync(ShoppingList list, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsOfUserAsync(string listId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveItemAsync(string listId, string itemId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveItemAsync(string listId, ShoppingListItem item)
        {
            throw new NotImplementedException();
        }
    }
}
