using Microsoft.Extensions.Logging;
using Shopping.Client.Services.Interfaces;
using Shopping.Shared.Data;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Shopping.Shared.Services;

namespace Shopping.Client.Services.Implementations
{
    public class UserGroupsShoppingListsApiAccess : CRUDApiAccessBaseImpl<UserGroupShoppingList>, IUserGroupShoppingLists
    {
        public UserGroupsShoppingListsApiAccess(IAuthService authService, ILogger<UserGroupsShoppingListsApiAccess> logger) : base(authService, logger)
        {
            BaseAddress = "api/UserGroupShoppingLists";
        }

        public async Task<List<ShoppingList>> GetShoppingListsOfUserGroupAsync(string userGroupId)
        {
            List<ShoppingList> lists = null;
            var client = await _authService.GetHttpClientAsync();
            var response = await client.GetAsync($"{BaseAddress}/ShoppingListsOfGroup/{userGroupId}");
            if (response.IsSuccessStatusCode)
            {
                lists = await response.Content.ReadFromJsonAsync<List<ShoppingList>>();
            }
            return lists;
        }

        public async Task<List<UserGroup>> GetUserGroupsOfShoppingListAsync(string shoppingListId)
        {
            List<UserGroup> userGroups = null;
            var client = await _authService.GetHttpClientAsync();
            var response = await client.GetAsync($"{BaseAddress}/UserGroupsOfShoppingList/{shoppingListId}");
            if (response.IsSuccessStatusCode)
            {
                userGroups = await response.Content.ReadFromJsonAsync<List<UserGroup>>();
            }
            return userGroups;
        }

        public async Task<bool> DeleteAsync(UserGroupShoppingList assignment)
        {
            var client = await _authService.GetHttpClientAsync();
            var response = await client.DeleteAsync($"{BaseAddress}/{assignment.UserGroupId}/{assignment.ShoppingListId}");
            return response.IsSuccessStatusCode;
        }
    }
}
