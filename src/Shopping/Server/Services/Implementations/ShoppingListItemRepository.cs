using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;
using Shopping.Server.Data;
using Shopping.Shared.Data;
using Shopping.Shared.Services;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Server.Services.Implementations
{
    public class ShoppingListItemRepository : CRUDDbContextBaseImpl<ShoppingListItem>, IShoppingListItems
    {
        private readonly IUserGroups _userGroups;
        public ShoppingListItemRepository(ShoppingDbContext context,
            ILogger<ShoppingListItem> logger, IUserGroups userGroups)
            : base(context, logger)
        {
            _userGroups = userGroups;
        }

        public override async Task<List<ShoppingListItem>> GetAllAsync()
        {
            var items = await _context.ShoppingListItems.ToListAsync();
            await AddGroupsToList(items);
            return items;
        }

        public async Task<List<ShoppingListItem>> GetAllOfUser(string userId)
        {
            var userGroupIdsOfUser = (await _userGroups.GetContainingUserAsync(userId))
                .Select(i => i.Id);
            var allItems = await _context.ShoppingListItems.ToListAsync();

            for (int i = allItems.Count - 1; i >= 0; i--)
            {
                var item = allItems[i];
                if (item.OwnerId != userId)
                {
                    var commonUserGroupIds = item.UserGroupIds.Intersect(userGroupIdsOfUser).ToList();
                    if (commonUserGroupIds.Count == 0)
                    {
                        allItems.Remove(item);
                    }
                }
            }

            await AddGroupsToList(allItems);

            return allItems;
        }

        public override async Task<ShoppingListItem> GetAsync(string id)
        {
            var item = await _context.ShoppingListItems.FirstOrDefaultAsync(i => i.Id == id);
            if (item != null)
            {
                await AddGroupsToToItem(item);
            }
            return item;
        }

        public override bool ItemAlreadyExists(ShoppingListItem item)
        {
            throw new NotImplementedException();
        }

        public override bool ItemHasChanged(ShoppingListItem existing, ShoppingListItem updated)
        {
            throw new NotImplementedException();
        }

        public override void UpdateExistingItem(ShoppingListItem existing, ShoppingListItem update)
        {
            throw new NotImplementedException();
        }

        private async Task AddGroupsToList(List<ShoppingListItem> items)
        {
            foreach (var item in items)
            {
                await AddGroupsToToItem(item);
            }
        }
        private async Task AddGroupsToToItem(ShoppingListItem item)
        {
            item.UserGroups = new List<UserGroup>();
            foreach (var groupId in item.UserGroupIds)
            {
                var group = await _userGroups.GetAsync(groupId);
                if (group != null)
                {
                    item.UserGroups.Add(group);
                }
            }
        }
    }
}
