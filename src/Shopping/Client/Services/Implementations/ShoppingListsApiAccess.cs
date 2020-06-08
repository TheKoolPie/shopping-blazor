using Microsoft.Extensions.Logging;
using Shopping.Client.Services.Interfaces;
using Shopping.Shared.Data;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Client.Services.Implementations
{
    public class ShoppingListsApiAccess : CRUDAccessBaseImpl<ShoppingList>, IShoppingLists
    {
        public ShoppingListsApiAccess(HttpClient httpClient, ITokenProvider tokenProvider, ILogger<ShoppingListsApiAccess> logger) : base(httpClient, tokenProvider, logger)
        {
            BaseAddress = "api/ShoppingLists";
        }

        public async Task<ShoppingListItem> AddOrUpdateItemAsync(string listId, ShoppingListItem item)
        {
            var client = await GetHttpClientAsync();
            ShoppingListItem createdItem = null;
            var response = await client.PostAsJsonAsync<ShoppingListItem>($"{BaseAddress}/AddItem/{listId}", item);
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

        public Task<List<ShoppingList>> GetAllOfUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsOfUserAsync(ShoppingList list, string userId)
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
