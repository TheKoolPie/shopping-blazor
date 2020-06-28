using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopping.Server.Data;
using Shopping.Shared.Data;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Model.Account;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Server.Services.Implementations
{
    public class EfDataRepository : IDataRepository
    {
        private readonly ShoppingDbContext _context;
        private readonly ILogger<EfDataRepository> _logger;
        public EfDataRepository(ShoppingDbContext context, ILogger<EfDataRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        #region Category
        public async Task<List<ProductCategory>> GetCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }
        public async Task<ProductCategory> GetCategoryAsync(string id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(i => i.Id == id);
            if (category == null)
            {
                throw new ItemNotFoundException(typeof(ProductCategory), id);
            }
            return category;
        }
        public async Task<ProductCategory> CreateCategoryAsync(ProductCategory item)
        {
            if (CategoryAlreadyExists(item))
            {
                throw new ItemAlreadyExistsException(typeof(ProductCategory), item.Id);
            }
            _context.Categories.Add(item);
            await SaveChangesAsync();
            return item;
        }
        public async Task<ProductCategory> UpdateCategoryAsync(string id, ProductCategory item)
        {
            if (!CategoryCanBeUpdated(item))
            {
                throw new ItemAlreadyExistsException(typeof(ProductCategory), item.Id);
            }
            var existing = await GetCategoryAsync(id);

            existing.Name = item.Name;
            existing.ColorCode = item.ColorCode;

            await SaveChangesAsync();

            return existing;
        }
        public async Task<bool> DeleteCategoryAsync(string id)
        {
            var existing = await GetCategoryAsync(id);

            var products = await GetProductsWithCategory(id);
            if (products.Count > 0)
            {
                foreach (var product in products)
                {
                    var shoppinglists = await GetShoppingListsWithProduct(product.Id);
                    if (shoppinglists.Count > 0)
                    {
                        foreach (var list in shoppinglists)
                        {
                            var deleteItem = list.Items.FirstOrDefault(i => i.ProductItemId == product.Id);
                            if (deleteItem != null)
                            {
                                list.Items.Remove(deleteItem);
                            }
                        }
                    }
                    _context.Products.Remove(product);
                }
            }

            _context.Categories.Remove(existing);

            bool result = false;
            try
            {
                await SaveChangesAsync();
                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }
        public bool CategoryAlreadyExists(ProductCategory item)
        {
            var all = _context.Categories.ToList();
            return all
                    .Any(
                        c => c.Id == item.Id ||
                        c.Name.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase) ||
                        c.ColorCode.Equals(item.ColorCode, StringComparison.InvariantCultureIgnoreCase)
                        );
        }
        public bool CategoryCanBeUpdated(ProductCategory item)
        {
            var all = _context.Categories.ToList();
            var restWithOutCurrentItem = all.Where(c => c.Id != item.Id).ToList();

            return !(restWithOutCurrentItem
                        .Any(
                            c => c.Name.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase) ||
                            c.ColorCode.Equals(item.ColorCode, StringComparison.InvariantCultureIgnoreCase)
                            )
                    );
        }
        #endregion

        #region Product
        public async Task<List<ProductItem>> GetProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            foreach (var product in products)
            {
                product.Category = await GetCategoryAsync(product.CategoryId);
            }
            return products;
        }
        public async Task<ProductItem> GetProductAsync(string id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(i => i.Id == id);
            if (product == null)
            {
                throw new ItemNotFoundException(typeof(ProductItem), id);
            }
            product.Category = await GetCategoryAsync(product.CategoryId);
            return product;
        }
        public async Task<ProductItem> CreateProductAsync(ProductItem item)
        {
            if (ProductAlreadyExists(item))
            {
                throw new ItemAlreadyExistsException(typeof(ProductItem), item.Id);
            }

            item.Category = await GetCategoryAsync(item.CategoryId);

            _context.Products.Add(item);
            await SaveChangesAsync();
            return item;
        }
        public async Task<ProductItem> UpdateProductAsync(string id, ProductItem item)
        {
            if (!ProductCanBeUpdated(item))
            {
                throw new ItemAlreadyExistsException(typeof(ProductItem), item.Id);
            }
            var existing = await GetProductAsync(id);
            existing.Name = item.Name;
            existing.Unit = item.Unit;
            existing.CategoryId = item.CategoryId;
            existing.Category = await GetCategoryAsync(item.CategoryId);

            await SaveChangesAsync();

            return existing;
        }
        public async Task<bool> DeleteProductAsync(string id)
        {
            var existing = await GetProductAsync(id);
            var shoppinglists = await GetShoppingListsWithProduct(id);
            if (shoppinglists.Count > 0)
            {
                foreach (var list in shoppinglists)
                {
                    var deleteItem = list.Items.FirstOrDefault(i => i.ProductItemId == id);
                    if (deleteItem != null)
                    {
                        list.Items.Remove(deleteItem);
                    }
                }
            }
            _context.Products.Remove(existing);

            bool result = false;
            try
            {
                await SaveChangesAsync();
                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }
        public bool ProductAlreadyExists(ProductItem item)
        {
            var all = _context.Products.ToList();
            return all.Any(
                            p => p.Id == item.Id ||
                            p.Name.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase)
                          );
        }
        public bool ProductCanBeUpdated(ProductItem item)
        {
            var all = _context.Products.ToList();
            var restWithOutCurrentItem = all.Where(p => p.Id != item.Id).ToList();

            return !(restWithOutCurrentItem.Any(p => p.Name.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase)));
        }
        #endregion

        #region Shopping List
        public async Task<List<ShoppingList>> GetShoppingListsAsync()
        {
            var lists = await _context.ShoppingLists.ToListAsync();
            foreach (var list in lists)
            {
                foreach (var item in list.Items)
                {
                    item.ProductItem = await GetProductAsync(item.ProductItemId);
                }
            }
            return lists;
        }
        public async Task<ShoppingList> GetShoppingListAsync(string id)
        {
            var list = await _context.ShoppingLists.FirstOrDefaultAsync(i => i.Id == id);
            if (list == null)
            {
                throw new ItemNotFoundException(typeof(ShoppingList), id);
            }
            foreach (var item in list.Items)
            {
                item.ProductItem = await GetProductAsync(item.ProductItemId);
            }
            return list;
        }
        public async Task<ShoppingList> CreateShoppingListAsync(ShoppingList item)
        {
            if (ShoppingListAlreadyExists(item))
            {
                throw new ItemAlreadyExistsException(typeof(ShoppingList), item.Id);
            }

            _context.ShoppingLists.Add(item);
            await SaveChangesAsync();
            return item;
        }
        public async Task<ShoppingList> UpdateShoppingListAsync(string id, ShoppingList item)
        {
            if (!ShoppingListCanBeUpdated(item))
            {
                throw new ItemAlreadyExistsException(typeof(ShoppingList), item.Id);
            }
            var existing = await GetShoppingListAsync(id);

            existing.Name = item.Name;
            existing.ListDate = item.ListDate;
            existing.Owner = item.Owner;

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

            await SaveChangesAsync();

            return existing;
        }
        public async Task<bool> DeleteShoppingListAsync(string id)
        {
            var existing = await GetShoppingListAsync(id);

            await RemoveAssignmentsOfShoppingListAsync(id);

            _context.ShoppingLists.Remove(existing);

            bool result = false;
            try
            {
                await SaveChangesAsync();
                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }
        public bool ShoppingListAlreadyExists(ShoppingList item)
        {
            var lists = _context.ShoppingLists.ToList();

            return lists.Any(i => i.Id == item.Id || (i.Owner.Id == item.Owner.Id && i.Name == item.Name));
        }
        public bool ShoppingListCanBeUpdated(ShoppingList item)
        {
            var lists = _context.ShoppingLists.ToList();
            var listsOfOwner = lists.Where(i => i.Owner.Id == item.Owner.Id).ToList();
            var itemsWithOutCurrentItem = listsOfOwner.Where(i => i.Id != item.Id).ToList();

            return !(itemsWithOutCurrentItem.Any(i => i.Name == item.Name));
        }
        #endregion

        #region User Group
        public async Task<List<UserGroup>> GetUserGroupsAsync()
        {
            var groups = await _context.UserGroups.ToListAsync();
            return groups;
        }
        public async Task<UserGroup> GetUserGroupAsync(string id)
        {
            var group = await _context.UserGroups.FirstOrDefaultAsync(i => i.Id == id);
            if (group == null)
            {
                throw new ItemNotFoundException(typeof(UserGroup), id);
            }
            return group;
        }
        public async Task<UserGroup> CreateUserGroupAsync(UserGroup item)
        {
            if (UserGroupAlreadyExists(item))
            {
                throw new ItemAlreadyExistsException(typeof(UserGroup), item.Id);
            }

            _context.UserGroups.Add(item);
            await SaveChangesAsync();
            return item;
        }
        public async Task<UserGroup> UpdateUserGroupAsync(string id, UserGroup item)
        {
            if (!UserGroupCanBeUpdated(item))
            {
                throw new ItemAlreadyExistsException(typeof(UserGroup), item.Id);
            }
            var existing = await GetUserGroupAsync(id);
            existing.Name = item.Name;
            existing.Owner = item.Owner;
            existing.Members = new List<ShoppingUserModel>(item.Members);

            await SaveChangesAsync();

            return existing;
        }
        public async Task<bool> DeleteUserGroupAsync(string id)
        {
            var existing = await GetUserGroupAsync(id);

            await RemoveAssignmentsOfGroupAsync(id);

            _context.UserGroups.Remove(existing);

            bool result = false;
            try
            {
                await SaveChangesAsync();
                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }
        public bool UserGroupAlreadyExists(UserGroup item)
        {
            var groups = _context.UserGroups.ToList();
            return groups.Any(g => g.Id == item.Id || (g.Owner.Id == item.Id && g.Name == item.Name));
        }
        public bool UserGroupCanBeUpdated(UserGroup item)
        {
            var groups = _context.UserGroups.ToList();
            var groupsWithoutCurrentItem = groups.Where(g => g.Id == item.Id).ToList();
            var groupsOfOwner = groupsWithoutCurrentItem.Where(g => g.Owner.Id == item.Owner.Id).ToList();
            return !(groupsOfOwner.Any(g => g.Name == item.Name));
        }
        #endregion

        #region GroupList Assignments
        public async Task<List<UserGroupShoppingList>> GetGroupListAssignmentsAsync()
        {
            var assignments = await _context.UserGroupShoppingLists.ToListAsync();
            return assignments;
        }
        public async Task<UserGroupShoppingList> GetGroupListAssignmentAsync(string id)
        {
            var assignment = await _context.UserGroupShoppingLists.FirstOrDefaultAsync(a => a.Id == id);
            return assignment;
        }
        public async Task<UserGroupShoppingList> CreateGroupListAssignmentAsync(UserGroupShoppingList item)
        {
            if (GroupListAssignmentAlreadyExists(item))
            {
                throw new ItemAlreadyExistsException(typeof(UserGroupShoppingList), item.Id);
            }

            item.ShoppingList = await GetShoppingListAsync(item.ShoppingListId);
            item.UserGroup = await GetUserGroupAsync(item.UserGroupId);

            _context.UserGroupShoppingLists.Add(item);
            await SaveChangesAsync();
            return item;
        }
        public async Task<UserGroupShoppingList> UpdateGroupListAssignmentAsync(string id, UserGroupShoppingList item)
        {
            if (!GroupListAssignmentCanBeUpdated(item))
            {
                throw new ItemAlreadyExistsException(typeof(UserGroupShoppingList), id);
            }
            var existing = await GetGroupListAssignmentAsync(id);
            existing.ShoppingListId = item.ShoppingListId;
            existing.UserGroupId = item.UserGroupId;

            await SaveChangesAsync();

            return existing;
        }
        public async Task<bool> DeleteGroupListAssignmentAsync(UserGroupShoppingList assignment)
        {
            var existing = (await GetGroupListAssignmentsAsync())
                .FirstOrDefault(a => a.ShoppingListId == assignment.ShoppingListId && a.UserGroupId == assignment.UserGroupId);

            bool result = false;
            if (existing != null)
            {
                _context.UserGroupShoppingLists.Remove(existing);
                try
                {
                    await SaveChangesAsync();
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
        public bool GroupListAssignmentAlreadyExists(UserGroupShoppingList item)
        {
            var assignments = _context.UserGroupShoppingLists.ToList();
            return assignments.Any(a => a.Id == item.Id || (a.ShoppingListId == item.ShoppingListId && a.UserGroupId == item.UserGroupId));
        }
        public bool GroupListAssignmentCanBeUpdated(UserGroupShoppingList item)
        {
            var assignments = _context.UserGroupShoppingLists.ToList();
            var itemsWithoutCurrent = assignments.Where(a => a.Id != item.Id).ToList();

            return !(itemsWithoutCurrent.Any(a => a.ShoppingListId == item.ShoppingListId && a.UserGroupId == item.UserGroupId));
        }
        private async Task<bool> RemoveAssignmentsOfGroupAsync(string userGroupId)
        {
            var allAssignmentsOfGroup = (await GetGroupListAssignmentsAsync())
                .Where(a => a.UserGroupId == userGroupId)
                .ToList();

            return await DeleteAssignments(allAssignmentsOfGroup);
        }
        private async Task<bool> RemoveAssignmentsOfShoppingListAsync(string shoppingListId)
        {
            var allAssignmentsOfList = (await GetGroupListAssignmentsAsync())
                .Where(a => a.ShoppingListId == shoppingListId)
                .ToList();

            return await DeleteAssignments(allAssignmentsOfList);
        }
        private async Task<bool> DeleteAssignments(List<UserGroupShoppingList> assignments)
        {
            foreach (var assignment in assignments)
            {
                if (!(await DeleteGroupListAssignmentAsync(assignment)))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region Common Helpers
        public async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("Could not save changes");
                throw new PersistencyException("Could not save changes", e);
            }
        }
        private async Task<List<ProductItem>> GetProductsWithCategory(string categoryId)
        {
            var products = await _context.Products.ToListAsync();
            products = products
                .Where(p => p.CategoryId == categoryId)
                .ToList();

            return products;
        }
        private async Task<List<ShoppingList>> GetShoppingListsWithProduct(string productItemId)
        {
            var shoppinglist = await _context.ShoppingLists.ToListAsync();
            shoppinglist = shoppinglist
                .Where(i => i.Items.Any(p => p.ProductItemId == productItemId))
                .ToList();
            return shoppinglist;
        }
        #endregion

    }
}
