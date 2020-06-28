using Microsoft.EntityFrameworkCore.Internal;
using Shopping.Shared.Data;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Model.Account;

namespace Shopping.Server.UnitTests.Mocks
{
    public class DataRepoMock : IDataRepository
    {
        private List<ProductCategory> _categories;
        private List<ProductItem> _products;
        private List<ShoppingList> _shoppingLists;
        private List<UserGroup> _userGroups;
        private List<UserGroupShoppingList> _userGroupShoppingLists;

        public DataRepoMock()
        {
            _categories = new List<ProductCategory>();
            _products = new List<ProductItem>();
            _shoppingLists = new List<ShoppingList>();
            _userGroups = new List<UserGroup>();
            _userGroupShoppingLists = new List<UserGroupShoppingList>();
        }
        public DataRepoMock(string filePath) : this()
        {

        }


        #region Category
        public async Task<List<ProductCategory>> GetCategoriesAsync()
        {
            return await Task.FromResult(_categories.ToList());
        }
        public async Task<ProductCategory> GetCategoryAsync(string id)
        {
            return await (Task.FromResult(_categories.FirstOrDefault(c => c.Id == id)));
        }
        public async Task<ProductCategory> CreateCategoryAsync(ProductCategory item)
        {
            if (CategoryAlreadyExists(item))
            {
                throw new ItemAlreadyExistsException(typeof(ProductCategory), item.Id);
            }
            _categories.Add(item);
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
                    _products.Remove(product);
                }
            }

            _categories.Remove(existing);

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
            return _categories.Any(
                        c => c.Id == item.Id ||
                        c.Name.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase) ||
                        c.ColorCode.Equals(item.ColorCode, StringComparison.InvariantCultureIgnoreCase)
                        );
        }
        public bool CategoryCanBeUpdated(ProductCategory item)
        {
            var items = _categories.Where(c => c.Id != item.Id).ToList();
            return !(items.Any(
                            c => c.Name.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase) ||
                            c.ColorCode.Equals(item.ColorCode, StringComparison.InvariantCultureIgnoreCase)
                            ));
        }
        #endregion

        #region Product
        public async Task<List<ProductItem>> GetProductsAsync()
        {
            return await Task.FromResult(_products.ToList());
        }
        public async Task<ProductItem> GetProductAsync(string id)
        {
            return await (Task.FromResult(_products.FirstOrDefault(c => c.Id == id)));
        }
        public async Task<ProductItem> CreateProductAsync(ProductItem item)
        {
            if (ProductAlreadyExists(item))
            {
                throw new ItemAlreadyExistsException(typeof(ProductItem), item.Id);
            }

            item.Category = await GetCategoryAsync(item.CategoryId);

            _products.Add(item);
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

            return item;
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
            _products.Remove(existing);

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
            return _products.Any(
                            p => p.Id == item.Id ||
                            p.Name.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase)
                          );
        }
        public bool ProductCanBeUpdated(ProductItem item)
        {
            var restWithOutCurrentItem = _products.Where(p => p.Id != item.Id).ToList();

            return !(restWithOutCurrentItem.Any(p => p.Name.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase)));
        }

        #endregion

        #region Shopping List
        public async Task<List<ShoppingList>> GetShoppingListsAsync()
        {
            return await Task.FromResult(_shoppingLists.ToList());
        }
        public async Task<ShoppingList> GetShoppingListAsync(string id)
        {
            return await (Task.FromResult(_shoppingLists.FirstOrDefault(c => c.Id == id)));
        }
        public async Task<ShoppingList> CreateShoppingListAsync(ShoppingList item)
        {
            if (ShoppingListAlreadyExists(item))
            {
                throw new ItemAlreadyExistsException(typeof(ShoppingList), item.Id);
            }

            _shoppingLists.Add(item);
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
            _shoppingLists.Remove(existing);

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
            return _shoppingLists.Any(i => i.Id == item.Id || (i.Owner.Id == item.Owner.Id && i.Name == item.Name));
        }
        public bool ShoppingListCanBeUpdated(ShoppingList item)
        {
            var listsOfOwner = _shoppingLists.Where(i => i.Owner.Id == item.Owner.Id).ToList();
            var itemsWithOutCurrentItem = listsOfOwner.Where(i => i.Id != item.Id).ToList();

            return !(itemsWithOutCurrentItem.Any(i => i.Name == item.Name));
        }
        #endregion

        #region User Group
        public async Task<List<UserGroup>> GetUserGroupsAsync()
        {
            return await Task.FromResult(_userGroups.ToList());
        }
        public async Task<UserGroup> GetUserGroupAsync(string id)
        {
            return await Task.FromResult(_userGroups.FirstOrDefault(c => c.Id == id));
        }
        public async Task<UserGroup> CreateUserGroupAsync(UserGroup item)
        {
            if (UserGroupAlreadyExists(item))
            {
                throw new ItemAlreadyExistsException(typeof(UserGroup), item.Id);
            }

            _userGroups.Add(item);
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
            _userGroups.Remove(existing);

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
            return _userGroups.Any(g => g.Id == item.Id || (g.Owner.Id == item.Id && g.Name == item.Name));
        }
        public bool UserGroupCanBeUpdated(UserGroup item)
        {
            var groupsWithoutCurrentItem = _userGroups.Where(g => g.Id == item.Id).ToList();
            var groupsOfOwner = groupsWithoutCurrentItem.Where(g => g.Owner.Id == item.Owner.Id).ToList();
            return !(groupsOfOwner.Any(g => g.Name == item.Name));
        }
        #endregion

        #region GroupList Assignments
        public async Task<List<UserGroupShoppingList>> GetGroupListAssignmentsAsync()
        {
            return await Task.FromResult(_userGroupShoppingLists.ToList());
        }
        public async Task<UserGroupShoppingList> GetGroupListAssignmentAsync(string id)
        {
            return await Task.FromResult(_userGroupShoppingLists.FirstOrDefault(c => c.Id == id));
        }
        public async Task<UserGroupShoppingList> CreateGroupListAssignmentAsync(UserGroupShoppingList item)
        {
            if (GroupListAssignmentAlreadyExists(item))
            {
                throw new ItemAlreadyExistsException(typeof(UserGroupShoppingList), item.Id);
            }

            item.ShoppingList = await GetShoppingListAsync(item.ShoppingListId);
            item.UserGroup = await GetUserGroupAsync(item.UserGroupId);

            _userGroupShoppingLists.Add(item);
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
            var existing = _userGroupShoppingLists
                .FirstOrDefault(a => a.ShoppingListId == assignment.ShoppingListId && a.UserGroupId == assignment.UserGroupId);

            _userGroupShoppingLists.Remove(existing);

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

        public bool GroupListAssignmentAlreadyExists(UserGroupShoppingList item)
        {
            return _userGroupShoppingLists
                .Any(a => a.Id == item.Id || (a.ShoppingListId == item.ShoppingListId && a.UserGroupId == item.UserGroupId));
        }
        public bool GroupListAssignmentCanBeUpdated(UserGroupShoppingList item)
        {
            var assignments = _userGroupShoppingLists.ToList();
            var itemsWithoutCurrent = assignments.Where(a => a.Id != item.Id).ToList();

            return !(itemsWithoutCurrent.Any(a => a.ShoppingListId == item.ShoppingListId && a.UserGroupId == item.UserGroupId));
        }
        #endregion

        public Task SaveChangesAsync()
        {
            return Task.FromResult(true);
        }
        private async Task<List<ProductItem>> GetProductsWithCategory(string categoryId)
        {
            var products = _products.ToList();
            products = products
                .Where(p => p.CategoryId == categoryId)
                .ToList();

            return await Task.FromResult(products);
        }
        private async Task<List<ShoppingList>> GetShoppingListsWithProduct(string productItemId)
        {
            var shoppinglist = _shoppingLists.ToList();
            shoppinglist = shoppinglist
                .Where(i => i.Items.Any(p => p.ProductItemId == productItemId))
                .ToList();
            return await Task.FromResult(shoppinglist);
        }


    }
}
