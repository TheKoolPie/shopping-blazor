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
    public class UserGroupsShoppingListsApiAccess : IUserGroupShoppingLists
    {
        protected readonly ILogger _logger;
        protected readonly IAuthService _authService;
        private string _baseUri;
        public UserGroupsShoppingListsApiAccess(IAuthService authService, ILogger<UserGroupsShoppingListsApiAccess> logger)
        {
            _authService = authService;
            _logger = logger;
            _baseUri = "api/UserGroupShoppingLists";
        }
        public async Task<UserGroupShoppingList> CreateAssignmentAsync(UserGroupShoppingList assignment)
        {
            UserGroupShoppingList retVal = null;

            var client = await _authService.GetHttpClientAsync();

            var response = await client.PostAsJsonAsync<UserGroupShoppingList>(_baseUri, assignment);
            if (response.IsSuccessStatusCode)
            {
                retVal = await response.Content.ReadFromJsonAsync<UserGroupShoppingList>();
            }
            else
            {
                _logger.LogError(response.ReasonPhrase);
            }
            return retVal;
        }

        public async Task<List<ShoppingList>> GetShoppingListsOfUserGroupAsync(string userGroupId)
        {
            List<ShoppingList> lists = null;
            var client = await _authService.GetHttpClientAsync();
            var response = await client.GetAsync($"{_baseUri}/ShoppingListsOfGroup/{userGroupId}");
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
            var response = await client.GetAsync($"{_baseUri}/UserGroupsOfShoppingList/{shoppingListId}");
            if (response.IsSuccessStatusCode)
            {
                userGroups = await response.Content.ReadFromJsonAsync<List<UserGroup>>();
            }
            return userGroups;
        }

        public async Task<bool> RemoveAssignmentAsync(UserGroupShoppingList assignment)
        {
            var client = await _authService.GetHttpClientAsync();
            var response = await client.DeleteAsync($"{_baseUri}/{assignment.UserGroupId}/{assignment.ShoppingListId}");
            return response.IsSuccessStatusCode;
        }
    }
}
