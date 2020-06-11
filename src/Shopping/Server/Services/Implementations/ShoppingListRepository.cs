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
        private readonly IUserGroupShoppingLists _userGroupShoppingLists;
        private readonly IProducts _products;
        private readonly IUserGroupRepository _userGroups;

        public ShoppingListRepository(ShoppingDbContext context, ILogger<ShoppingList> logger,
            IUserGroupShoppingLists userGroupShoppingLists, IProducts products, IUserGroupRepository userGroups)
            : base(context, logger)
        {
            _userGroupShoppingLists = userGroupShoppingLists;
            _products = products;
            _userGroups = userGroups;
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
                throw new ItemNotFoundException(typeof(ShoppingList),id);
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
            if (list == null)
            {
                throw new ItemNotFoundException(typeof(ShoppingList), listId);
            }

            item.ProductItem = await _products.GetAsync(item.ProductItemId);

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

        public override bool ItemAlreadyExists(ShoppingList item)
        {
            var list = _context.ShoppingLists
                        .FirstOrDefault(i => i.Id == item.Id || (i.OwnerId == item.OwnerId && i.Name == item.Name));
            return list != null;
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

        public override void UpdateExistingItem(ShoppingList existing, ShoppingList update)
        {
            existing.Name = update.Name;
            existing.ListDate = update.ListDate;
            existing.OwnerId = update.OwnerId;
            existing.Items = update.Items;
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
            bool isListOwner = list.OwnerId == userId;
            if (isListOwner) { return true; }

            var userGroupsOfList = await _userGroupShoppingLists.GetUserGroupsOfShoppingListAsync(list.Id);
            foreach (var group in userGroupsOfList)
            {
                if(await _userGroups.UserIsInGroupAsync(group.Id, userId))
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
    }
}
