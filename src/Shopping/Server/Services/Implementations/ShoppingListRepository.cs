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
    public class ShoppingListRepository : CRUDDbContextBaseImpl<ShoppingList>, IShoppingLists
    {
        private readonly IUserGroups _userGroups;
        private readonly IProducts _products;

        public ShoppingListRepository(ShoppingDbContext context, ILogger<ShoppingList> logger,
            IUserGroups userGroups, IProducts products)
            : base(context, logger)
        {
            _userGroups = userGroups;
            _products = products;
        }

        public override async Task<List<ShoppingList>> GetAllAsync()
        {
            var lists = await _context.ShoppingLists.ToListAsync();
            foreach (var list in lists)
            {
                foreach (var item in list.Items)
                {
                    item.ProductItem = await _products.GetAsync(item.ProductItemId);
                }
            }
            return lists;
        }

        public override async Task<ShoppingList> GetAsync(string id)
        {
            var list = await _context.ShoppingLists.FirstOrDefaultAsync(i => i.Id == id);
            if (list == null)
            {
                throw new ItemNotFoundException();
            }
            foreach (var item in list.Items)
            {
                item.ProductItem = await _products.GetAsync(item.ProductItemId);
            }
            return list;
        }

        public async Task<ShoppingListItem> AddOrUpdateItemAsync(string listId, ShoppingListItem item)
        {
            var list = await GetAsync(listId);

            item.ProductItem = await _products.GetAsync(item.ProductItemId);

            list.AddOrUpdateItem(item);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new PersistencyException("Could not update item", e);
            }
            return item;
        }

        public async Task<UserGroup> AddUserGroupAsync(string listId, string userGroupId)
        {
            var list = await GetAsync(listId);
            var group = await _userGroups.GetAsync(userGroupId);
            list.AddUserGroup(group);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new PersistencyException("Could not update item", e);
            }
            return group;
        }



        public override bool ItemAlreadyExists(ShoppingList item)
        {
            var list = _context.ShoppingLists
                        .FirstOrDefault(i => i.Id == item.Id || (i.OwnerId == item.OwnerId && i.Name == item.Name));
            return list != null;
        }

        public async Task<bool> RemoveItemAsync(string listId, string itemId)
        {
            var list = await GetAsync(listId);
            list.RemoveItem(itemId);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new PersistencyException("Could not update item", e);
            }
            return true;
        }

        public async Task<bool> RemoveItemAsync(string listId, ShoppingListItem item)
        {
            return await RemoveItemAsync(listId, item.Id);
        }

        public async Task<bool> RemoveUserGroupAsync(string listId, string userGroupId)
        {
            var list = await GetAsync(listId);
            list.RemoveUserGroup(userGroupId);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new PersistencyException("Could not update item", e);
            }
            return true;
        }

        public override void UpdateExistingItem(ShoppingList existing, ShoppingList update)
        {
            existing.Update(update);
        }

        public async Task<List<ShoppingList>> GetAllOfUserAsync(string userId)
        {
            var allLists = await GetAllAsync();

            for (int i = allLists.Count - 1; i >= 0; i--)
            {
                var list = allLists[i];

                if (!(await CheckIfListIsFromUser(allLists[i], userId)))
                {
                    allLists.Remove(list);
                }
            }
            return allLists;
        }

        public async Task<bool> CheckIfListIsFromUser(ShoppingList list, string userId)
        {
            var allGroupIdsOfUser = (await _userGroups.GetAllOfUserAsync(userId)).Select(g => g.Id);

            var isOwner = list.OwnerId == userId;
            var hasUserGroupIdInCommon = list.UserGroupIds.Intersect(allGroupIdsOfUser).Count() != 0;

            return isOwner || hasUserGroupIdInCommon;
        }
    }
}
