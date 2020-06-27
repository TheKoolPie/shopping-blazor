using Shopping.Shared.Data;
using Shopping.Shared.Model.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.Services.Interfaces
{
    public interface IDataRepository
    {

        #region Common
        Task SaveChangesAsync();
        #endregion

        #region Category
        Task<List<ProductCategory>> GetCategoriesAsync();
        Task<ProductCategory> GetCategoryAsync(string id);
        Task<ProductCategory> CreateCategoryAsync(ProductCategory item);
        Task<ProductCategory> UpdateCategoryAsync(string id, ProductCategory item);
        Task<bool> DeleteCategoryAsync(string id);
        bool CategoryAlreadyExists(ProductCategory item);
        bool CategoryCanBeUpdated(ProductCategory item);
        #endregion

        #region Product
        Task<List<ProductItem>> GetProductsAsync();
        Task<ProductItem> GetProductAsync(string id);
        Task<ProductItem> CreateProductAsync(ProductItem item);
        Task<ProductItem> UpdateProductAsync(string id, ProductItem item);
        Task<bool> DeleteProductAsync(string id);
        bool ProductAlreadyExists(ProductItem item);
        bool ProductCanBeUpdated(ProductItem item);
        #endregion

        #region Shopping List
        Task<List<ShoppingList>> GetShoppingListsAsync();
        Task<ShoppingList> GetShoppingListAsync(string id);
        Task<ShoppingList> CreateShoppingListAsync(ShoppingList item);
        Task<ShoppingList> UpdateShoppingListAsync(string id, ShoppingList item);
        Task<bool> DeleteShoppingListAsync(string id);
        bool ShoppingListAlreadyExists(ShoppingList item);
        bool ShoppingListCanBeUpdated(ShoppingList item);
        #endregion

        #region User Group
        Task<List<UserGroup>> GetUserGroupsAsync();
        Task<UserGroup> GetUserGroupAsync(string id);
        Task<UserGroup> CreateUserGroupAsync(UserGroup item);
        Task<UserGroup> UpdateUserGroupAsync(string id, UserGroup item);
        Task<bool> DeleteUserGroupAsync(string id);
        bool UserGroupAlreadyExists(UserGroup item);
        bool UserGroupCanBeUpdated(UserGroup item);
        #endregion

        #region GroupListAssignment
        Task<List<UserGroupShoppingList>> GetGroupListAssignmentsAsync();
        Task<UserGroupShoppingList> GetGroupListAssignmentAsync(string id);
        Task<UserGroupShoppingList> CreateGroupListAssignmentAsync(UserGroupShoppingList item);
        Task<UserGroupShoppingList> UpdateGroupListAssignmentAsync(string id, UserGroupShoppingList item);
        Task<bool> DeleteGroupListAssignmentAsync(UserGroupShoppingList assignment);
        bool GroupListAssignmentAlreadyExists(UserGroupShoppingList item);
        bool GroupListAssignmentCanBeUpdated(UserGroupShoppingList item);
        #endregion
    }
}
