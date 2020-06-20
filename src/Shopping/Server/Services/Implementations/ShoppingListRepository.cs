using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Serilog.Configuration;
using Shopping.Server.Data;
using Shopping.Shared.Data;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Services;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Server.Services.Implementations
{
    public class ShoppingListRepository : IShoppingLists
    {
        private readonly IDataRepository _data;
        private readonly IUserGroupShoppingLists _groupListAssignments;
        private readonly IUserGroupRepository _userGroups;

        public ShoppingListRepository(IDataRepository data, IUserGroupShoppingLists groupListAssignments, IUserGroupRepository userGroups)
        {
            _data = data;
            _groupListAssignments = groupListAssignments;
            _userGroups = userGroups;
        }

        public async Task<List<ShoppingList>> GetAllAsync()
        {
            return await _data.GetShoppingListsAsync();
        }

        public async Task<ShoppingList> GetAsync(string id)
        {
            return await _data.GetShoppingListAsync(id);
        }

        public async Task<ShoppingListItem> AddOrUpdateItemAsync(string listId, ShoppingListItem item)
        {
            var list = await GetAsync(listId);
            item.ProductItem = await _data.GetProductAsync(item.ProductItemId);

            var existingItem = list.Items.FirstOrDefault(i => i.ProductItem.Name == item.ProductItem.Name);
            if (existingItem == null)
            {
                list.Items.Add(item);
            }
            else
            {
                existingItem.Amount = item.Amount;
                existingItem.Done = item.Done;
            }

            await _data.SaveChangesAsync();

            return item;
        }

        public async Task<bool> RemoveItemAsync(string listId, string itemId)
        {
            var list = await GetAsync(listId);
            if (list == null)
            {
                throw new ItemNotFoundException(typeof(ShoppingList), listId);
            }
            var deleteItem = list.Items.FirstOrDefault(i => i.Id == itemId);
            if (deleteItem == null)
            {
                throw new ItemNotFoundException(typeof(ShoppingListItem), itemId);
            }

            list.Items.Remove(deleteItem);

            await _data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveItemAsync(string listId, ShoppingListItem item)
        {
            return await RemoveItemAsync(listId, item.Id);
        }

        public async Task<List<ShoppingList>> GetAllOfUserAsync(string userId)
        {
            var allLists = await GetAllAsync();

            for (int i = allLists.Count - 1; i >= 0; i--)
            {
                var list = allLists[i];

                if (!(await IsOfUserAsync(allLists[i], userId)))
                {
                    allLists.Remove(list);
                }
            }
            return allLists;
        }

        public async Task<bool> IsOfUserAsync(ShoppingList list, string userId)
        {
            bool isListOwner = list.Owner.Id == userId;
            if (isListOwner) { return true; }

            var userGroupsOfList = await _groupListAssignments.GetUserGroupsOfShoppingListAsync(list.Id);
            foreach (var group in userGroupsOfList)
            {
                if (await _userGroups.UserIsInGroupAsync(group.Id, userId))
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> IsOfUserAsync(string listId, string userId)
        {
            var list = await GetAsync(listId);
            return await IsOfUserAsync(list, userId);
        }

        public async Task<ShoppingList> CreateAsync(ShoppingList item)
        {
            return await _data.CreateShoppingListAsync(item);
        }

        public async Task<ShoppingList> UpdateAsync(string id, ShoppingList item)
        {
            return await _data.UpdateShoppingListAsync(id, item);
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            return await _data.DeleteShoppingListAsync(id);
        }
    }
}
