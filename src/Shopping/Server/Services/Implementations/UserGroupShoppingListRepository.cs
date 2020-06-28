using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopping.Server.Data;
using Shopping.Shared.Data;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Server.Services.Implementations
{
    public class UserGroupShoppingListRepository : IUserGroupShoppingLists
    {
        private readonly ILogger<UserGroupShoppingListRepository> _logger;
        private readonly IDataRepository _data;

        public UserGroupShoppingListRepository(IDataRepository data,
            ILogger<UserGroupShoppingListRepository> logger)
        {
            _data = data;
            _logger = logger;
        }
        public async Task<UserGroupShoppingList> CreateAssignmentAsync(UserGroupShoppingList assignment)
        {
            return await _data.CreateGroupListAssignmentAsync(assignment);
        }

        public async Task<List<ShoppingList>> GetShoppingListsOfUserGroupAsync(string userGroupId)
        {
            _logger.LogDebug($"Search for shopping list ids for user group {userGroupId}");
            var shoppingListIds = (await _data.GetGroupListAssignmentsAsync())
                .Where(x => x.UserGroupId == userGroupId)
                .Select(x => x.ShoppingListId)
                .ToList();
            _logger.LogDebug($"Found: {shoppingListIds.Count} shopping list(s)");

            List<ShoppingList> shoppingLists = new List<ShoppingList>();
            foreach (var id in shoppingListIds)
            {
                _logger.LogDebug($"Get shopping list {id} from DB");
                var shoppingList = await _data.GetShoppingListAsync(id);
                shoppingLists.Add(shoppingList);
            }
            return shoppingLists;
        }

        public async Task<List<UserGroup>> GetUserGroupsOfShoppingListAsync(string shoppingListId)
        {
            _logger.LogDebug($"Search for user groups for shopping list {shoppingListId}");
            var userGroupIds = (await _data.GetGroupListAssignmentsAsync())
                .Where(x => x.ShoppingListId == shoppingListId)
                .Select(x => x.UserGroupId)
                .ToList();
            _logger.LogDebug($"Found: {userGroupIds.Count} user group(s)");

            List<UserGroup> userGroups = new List<UserGroup>();
            foreach (var id in userGroupIds)
            {
                _logger.LogDebug($"Get user group {id} from DB");
                var group = await _data.GetUserGroupAsync(id);
                userGroups.Add(group);
            }
            return userGroups;
        }

        public async Task<bool> RemoveAssignmentAsync(UserGroupShoppingList assignment)
        {
            return await _data.DeleteGroupListAssignmentAsync(assignment);
        }
    }
}
