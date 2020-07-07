using Shopping.Shared.Data;
using Shopping.Shared.Data.Abstractions;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Server.Services.Implementations
{
    public class ShoppingListRepository : IShoppingLists
    {
        private readonly IUserRepository _userRepository;
        private readonly IShoppingDataRepository _context;
        private readonly IUserGroupShoppingLists _groupListAssignments;
        private readonly IUserGroupRepository _userGroups;

        public ShoppingListRepository(IUserRepository userRepository, IShoppingDataRepository context,
            IUserGroupShoppingLists groupListAssignments, IUserGroupRepository userGroups)
        {
            _userRepository = userRepository;
            _context = context;
            _groupListAssignments = groupListAssignments;
            _userGroups = userGroups;
        }

        public async Task<List<ShoppingList>> GetAllAsync()
        {
            var lists = await _context.ShoppingLists.ToListAsync();
            foreach (var list in lists)
            {
                list.Owner = await _userRepository.GetUserByIdAsync(list.OwnerId);
                foreach (var item in list.Items)
                {
                    item.ProductItem = await GetProductAsync(item.ProductItemId);
                }
            }
            return lists;
        }

        public async Task<ShoppingList> GetAsync(string id)
        {
            var list = await _context.ShoppingLists.FirstOrDefaultAsync(i => i.Id == id);
            if (list == null)
            {
                throw new ItemNotFoundException(typeof(ShoppingList), id);
            }
            list.Owner = await _userRepository.GetUserByIdAsync(list.OwnerId);
            foreach (var item in list.Items)
            {
                item.ProductItem = await GetProductAsync(item.ProductItemId);
            }
            return list;
        }

        public async Task<ShoppingListItem> AddOrUpdateItemAsync(string listId, ShoppingListItem item)
        {
            var list = await GetAsync(listId);
            item.ProductItem = await GetProductAsync(item.ProductItemId);

            var allItems = await _context.ShoppingListItems.ToListAsync();
            var itemsOfList = allItems.Where(i => i.ShoppingListId == listId);

            var existing = itemsOfList.FirstOrDefault(i => i.ProductItemId == item.ProductItemId);
            if (existing == null)
            {
                _context.ShoppingListItems.Add(item);
            }
            else
            {
                existing.Done = item.Done;
                existing.Amount = item.Amount;
            }

            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<bool> RemoveItemAsync(string listId, string itemId)
        {
            var list = await GetAsync(listId);
            if (list == null)
            {
                throw new ItemNotFoundException(typeof(ShoppingList), listId);
            }

            var items = await _context.ShoppingListItems.FirstOrDefaultAsync(i => i.ShoppingListId == listId && i.ProductItemId == itemId);
            if (items != null)
            {
                _context.ShoppingListItems.Remove(items);
            }

            await _context.SaveChangesAsync();

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
            bool isListOwner = list.OwnerId == userId;
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
            if (ItemAlreadyExists(item))
            {
                throw new ItemAlreadyExistsException(typeof(ShoppingList), item.Id);
            }

            _context.ShoppingLists.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<ShoppingList> UpdateAsync(string id, ShoppingList item)
        {
            if (!ItemCanBeUpdated(item))
            {
                throw new ItemAlreadyExistsException(typeof(ShoppingList), item.Id);
            }
            var existing = await GetAsync(id);

            existing.Name = item.Name;
            existing.ListDate = item.ListDate;
            existing.OwnerId = item.OwnerId;

            var deleteItems = existing.Items
                .Where(i => !item.Items.Any(j => j.ProductItemId == i.ProductItemId))
                .Select(i => i.Id)
                .ToList();

            foreach (var deleteId in deleteItems)
            {
                var delete = existing.Items.FirstOrDefault(i => i.Id == deleteId);
                if (delete != null)
                {
                    existing.Items.Remove(delete);
                }
            }

            foreach (var updateItem in item.Items)
            {
                var existingItem = existing.Items.FirstOrDefault(i => i.Id == updateItem.Id);
                if (existing == null)
                {
                    existing.Items.Add(updateItem);
                }
                else
                {
                    existingItem.Amount = updateItem.Amount;
                    existingItem.Done = updateItem.Done;
                }
            }

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            var existing = await GetAsync(id);

            await RemoveAssignmentsOfShoppingListAsync(id);

            _context.ShoppingLists.Remove(existing);

            bool result = false;
            try
            {
                await _context.SaveChangesAsync();
                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public async Task<bool> DeleteAllOfUser(string userId)
        {
            var allLists = await GetAllAsync();
            var owningLists = allLists.Where(u => u.Owner.Id == userId)
                .Select(u => u.Id)
                .ToList();

            foreach (var listId in owningLists)
            {
                if (!await DeleteByIdAsync(listId))
                {
                    return false;
                }
            }
            return true;
        }

        public bool ItemAlreadyExists(ShoppingList item)
        {
            var lists = _context.ShoppingLists.ToList();

            return lists.Any(i => i.Id == item.Id || (i.Owner.Id == item.Owner.Id && i.Name == item.Name));
        }

        public bool ItemCanBeUpdated(ShoppingList item)
        {
            var lists = _context.ShoppingLists.ToList();
            var listsOfOwner = lists.Where(i => i.Owner.Id == item.Owner.Id).ToList();
            var itemsWithOutCurrentItem = listsOfOwner.Where(i => i.Id != item.Id).ToList();

            return !(itemsWithOutCurrentItem.Any(i => i.Name == item.Name));
        }

        private async Task<ProductItem> GetProductAsync(string id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }
        private async Task<bool> RemoveAssignmentsOfShoppingListAsync(string shoppingListId)
        {
            var allAssignmentsOfList = (await _groupListAssignments.GetAllAsync())
                .Where(a => a.ShoppingListId == shoppingListId)
                .ToList();

            return await DeleteAssignments(allAssignmentsOfList);
        }
        private async Task<bool> DeleteAssignments(List<UserGroupShoppingList> assignments)
        {
            foreach (var assignment in assignments)
            {
                if (!(await _groupListAssignments.DeleteAsync(assignment)))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
