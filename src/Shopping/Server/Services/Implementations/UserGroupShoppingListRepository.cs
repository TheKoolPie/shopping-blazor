using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopping.Server.Data;
using Shopping.Shared.Data;
using Shopping.Shared.Data.Abstractions;
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
        private readonly IShoppingDataRepository _context;

        public UserGroupShoppingListRepository(IShoppingDataRepository context,
            ILogger<UserGroupShoppingListRepository> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<List<UserGroupShoppingList>> GetAllAsync()
        {
            var assignments = await _context.UserGroupShoppingLists.ToListAsync();
            return assignments;
        }

        public async Task<UserGroupShoppingList> GetAsync(string id)
        {
            var assignment = await _context.UserGroupShoppingLists.FirstOrDefaultAsync(a => a.Id == id);
            return assignment;
        }

        public async Task<UserGroupShoppingList> CreateAsync(UserGroupShoppingList item)
        {
            if (ItemAlreadyExists(item))
            {
                throw new ItemAlreadyExistsException(typeof(UserGroupShoppingList), item.Id);
            }

            item.ShoppingList = await GetShoppingListAsync(item.ShoppingListId);
            item.UserGroup = await GetUserGroupAsync(item.UserGroupId);

            _context.UserGroupShoppingLists.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<UserGroupShoppingList> UpdateAsync(string id, UserGroupShoppingList item)
        {
            if (!ItemCanBeUpdated(item))
            {
                throw new ItemAlreadyExistsException(typeof(UserGroupShoppingList), id);
            }
            var existing = await GetAsync(id);
            existing.ShoppingListId = item.ShoppingListId;
            existing.UserGroupId = item.UserGroupId;

            await _context.SaveChangesAsync();

            return existing;
        }
        public async Task<bool> DeleteAsync(UserGroupShoppingList assignment)
        {
            var existing = (await GetAllAsync())
                .FirstOrDefault(a => a.ShoppingListId == assignment.ShoppingListId && a.UserGroupId == assignment.UserGroupId);

            bool result = false;
            if (existing != null)
            {
                _context.UserGroupShoppingLists.Remove(existing);
                try
                {
                    await _context.SaveChangesAsync();
                    result = true;
                }
                catch
                {
                    result = false;
                }
            }
            else
            {
                _logger.LogWarning($"No assignment found between list '{assignment.ShoppingListId}' and group '{assignment.UserGroupId}'");
            }

            return result;
        }
        public Task<bool> DeleteByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public bool ItemAlreadyExists(UserGroupShoppingList item)
        {
            var assignments = _context.UserGroupShoppingLists.ToList();
            return assignments.Any(a => a.Id == item.Id || (a.ShoppingListId == item.ShoppingListId && a.UserGroupId == item.UserGroupId));
        }

        public bool ItemCanBeUpdated(UserGroupShoppingList item)
        {
            var assignments = _context.UserGroupShoppingLists.ToList();
            var itemsWithoutCurrent = assignments.Where(a => a.Id != item.Id).ToList();

            return !(itemsWithoutCurrent.Any(a => a.ShoppingListId == item.ShoppingListId && a.UserGroupId == item.UserGroupId));
        }

        public async Task<List<ShoppingList>> GetShoppingListsOfUserGroupAsync(string userGroupId)
        {
            _logger.LogDebug($"Search for shopping list ids for user group {userGroupId}");
            var shoppingListIds = (await GetAllAsync())
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
            var userGroupIds = (await GetAllAsync())
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




        private async Task<ShoppingList> GetShoppingListAsync(string id)
        {
            return await _context.ShoppingLists.FirstOrDefaultAsync(i => i.Id == id);
        }
        private async Task<UserGroup> GetUserGroupAsync(string id)
        {
            return await _context.UserGroups.FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
