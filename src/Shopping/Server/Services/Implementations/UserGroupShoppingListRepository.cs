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
        private readonly ShoppingDbContext _context;
        private readonly ILogger<UserGroupShoppingListRepository> _logger;

        public UserGroupShoppingListRepository(ShoppingDbContext context,
            ILogger<UserGroupShoppingListRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> CreateAssignmentAsync(UserGroupShoppingList assignment)
        {
            if (!ItemExists(assignment.UserGroupId, assignment.ShoppingListId))
            {
                _logger.LogDebug($"Searching for user group with id {assignment.UserGroupId}");
                var group = await GetUserGroupAsync(assignment.UserGroupId);

                _logger.LogDebug($"Searching for shopping list with id {assignment.ShoppingListId}");
                var list = await GetShoppingListAsync(assignment.ShoppingListId);

                assignment.UserGroup = group;
                assignment.ShoppingList = list;
                _logger.LogDebug("Create assignment");
                _context.UserGroupShoppingLists.Add(assignment);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw new PersistencyException($"Could not save assignment between Group '{assignment.UserGroupId}' and List '{assignment.ShoppingListId}'", e);
                }
            }
            else
            {
                throw new ItemAlreadyExistsException($"Assignment between Group '{assignment.UserGroupId}' and List '{assignment.ShoppingListId}' already exists");
            }
            return true;
        }

        public async Task<List<ShoppingList>> GetShoppingListsOfUserGroupAsync(string userGroupId)
        {
            _logger.LogDebug($"Search for shopping list ids for user group {userGroupId}");
            var shoppingListIds = _context.UserGroupShoppingLists
                .Where(x => x.UserGroupId == userGroupId)
                .Select(x => x.ShoppingListId)
                .ToList();
            _logger.LogDebug($"Found: {shoppingListIds.Count} shopping list(s)");

            List<ShoppingList> shoppingLists = new List<ShoppingList>();
            foreach (var id in shoppingListIds)
            {
                _logger.LogDebug($"Get shopping list {id} from DB");
                var shoppingList = await GetShoppingListAsync(id);
                shoppingLists.Add(shoppingList);
            }
            return shoppingLists;
        }

        public async Task<List<UserGroup>> GetUserGroupsOfShoppingListAsync(string shoppingListId)
        {
            _logger.LogDebug($"Search for user groups for shopping list {shoppingListId}");
            var userGroupIds = _context.UserGroupShoppingLists
                .Where(x => x.ShoppingListId == shoppingListId)
                .Select(x => x.UserGroupId)
                .ToList();
            _logger.LogDebug($"Found: {userGroupIds.Count} user group(s)");

            List<UserGroup> userGroups = new List<UserGroup>();
            foreach (var id in userGroupIds)
            {
                _logger.LogDebug($"Get user group {id} from DB");
                var group = await GetUserGroupAsync(id);
                userGroups.Add(group);
            }
            return userGroups;
        }

        public async Task<bool> RemoveAssignmentAsync(UserGroupShoppingList assignment)
        {
            if (ItemExists(assignment.UserGroupId, assignment.ShoppingListId))
            {
                var item = await _context.UserGroupShoppingLists
                    .FirstOrDefaultAsync(i => i.UserGroupId == assignment.UserGroupId && i.ShoppingListId == assignment.ShoppingListId);
                _context.UserGroupShoppingLists.Remove(item);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw new PersistencyException($"Could not delete assignment between Group '{assignment.UserGroupId}' and List '{assignment.ShoppingListId}'", e);
                }
            }
            else
            {
                throw new ItemNotFoundException($"Assignment between Group '{assignment.UserGroupId}' and List '{assignment.ShoppingListId}' not found");
            }
            return true;
        }

        private bool ItemExists(string userGroupId, string shoppingListId)
        {
            return _context.UserGroupShoppingLists.Any(s => s.UserGroupId == userGroupId && s.ShoppingListId == shoppingListId);
        }

        private async Task<ShoppingList> GetShoppingListAsync(string id)
        {
            var list = await _context.ShoppingLists.FirstOrDefaultAsync(i => i.Id == id);
            if (list == null)
            {
                throw new ItemNotFoundException(typeof(ShoppingList), id);
            }
            return list;
        }
        private async Task<UserGroup> GetUserGroupAsync(string id)
        {
            var group = await _context.UserGroups.FirstOrDefaultAsync(i => i.Id == id);
            if (group == null)
            {
                throw new ItemNotFoundException(typeof(UserGroup), id);
            }
            return group;
        }
    }
}
