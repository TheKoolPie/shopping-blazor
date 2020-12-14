using Microsoft.Extensions.Logging;
using Shopping.Client.Services.Interfaces;
using Shopping.Shared.Data;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Shopping.Client.Services.Implementations.Base;
using Shopping.Shared.Results;

namespace Shopping.Client.Services.Implementations
{
    public class UserGroupsShoppingListsApiAccess : BaseShoppingApiImpl<UserGroupShoppingList,UserGroupShoppingListResult>, IUserGroupShoppingLists
    {
        public UserGroupsShoppingListsApiAccess(IAuthService authService, ILogger<UserGroupsShoppingListsApiAccess> logger) 
            : base("UserGroupShoppingLists", authService, logger)
        {
        }

        public async Task<List<ShoppingList>> GetShoppingListsOfUserGroupAsync(string userGroupId)
        {
            var client = await GetApiClient();
            var response = await client.GetAsync($"{_baseUri}/ShoppingListsOfGroup/{userGroupId}");

            CheckForUnauthorized(response);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ShoppingListResult>();
                if (result.IsSuccessful)
                {
                    return result.ResultData;
                }
                else
                {
                    _logger.LogError("Result is not set to successful", result.ErrorMessages);
                }
            }
            else
            {
                _logger.LogError($"Response has no success status code: {response.StatusCode}");
            }

            return null;
        }

        public async Task<List<UserGroup>> GetUserGroupsOfShoppingListAsync(string shoppingListId)
        {
            var client = await GetApiClient();
            var response = await client.GetAsync($"{_baseUri}/UserGroupsOfShoppingList/{shoppingListId}");

            CheckForUnauthorized(response);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<UserGroupResult>();
                if (result.IsSuccessful)
                {
                    return result.ResultData;
                }
                else
                {
                    _logger.LogError("Result is not set to successful", result.ErrorMessages);
                }
            }
            else
            {
                _logger.LogError($"Response has no success status code: {response.StatusCode}");
            }

            return null;
        }

        public async Task<bool> DeleteAsync(UserGroupShoppingList assignment)
        {
            return await SendDelete($"{_baseUri}/{assignment.UserGroupId}/{assignment.ShoppingListId}");
        }
    }
}
