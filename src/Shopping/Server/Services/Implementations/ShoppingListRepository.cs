using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Serilog.Configuration;
using Shopping.Server.Data;
using Shopping.Shared.Data;
using Shopping.Shared.Exceptions;
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

        public ShoppingListRepository(ShoppingDbContext context, ILogger<ShoppingList> logger, IUserGroups userGroups)
            : base(context, logger)
        {
            _userGroups = userGroups;
        }

        public override async Task<List<ShoppingList>> GetAllAsync()
        {
            return await _context.ShoppingLists.ToListAsync();
        }

        public override async Task<ShoppingList> GetAsync(string id)
        {
            var list = await _context.ShoppingLists.FirstOrDefaultAsync(i => i.Id == id);
            if (list == null)
            {
                throw new ItemNotFoundException();
            }
            return list;
        }

        public async Task<ShoppingListItem> AddOrUpdateItemAsync(string listId, ShoppingListItem item)
        {
            var list = await GetAsync(listId);
            list.AddOrUpdateItem(item);

            await UpdateAsync(listId, list);

            return item;
        }

        public async Task<UserGroup> AddUserGroupAsync(string listId, string userGroupId)
        {
            var list = await GetAsync(listId);
            var group = await _userGroups.GetAsync(userGroupId);
            list.AddUserGroup(group);

            await UpdateAsync(listId, list);

            return group;
        }



        public override bool ItemAlreadyExists(ShoppingList item)
        {
            return _context.ShoppingLists.Any(i => i.Id == item.Id);
        }

        public override bool ItemHasChanged(ShoppingList existing, ShoppingList updated)
        {
            return existing != updated;
        }

        public async Task<bool> RemoveItemAsync(string listId, string itemId)
        {
            var list = await GetAsync(listId);
            list.RemoveItem(itemId);

            await UpdateAsync(listId, list);

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

            await UpdateAsync(listId, list);

            return true;
        }

        public override void UpdateExistingItem(ShoppingList existing, ShoppingList update)
        {
            existing.Update(update);
        }
    }
}
