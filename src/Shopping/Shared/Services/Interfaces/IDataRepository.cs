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
        #region Category
        Task<List<ProductCategory>> GetCategoriesAsync();
        Task<ProductCategory> GetCategoryAsync(string id);
        Task<ProductCategory> CreateCategoryAsync(ProductCategory category);
        Task<ProductCategory> UpdateCategoryAsync(string id, ProductCategory category);
        Task<bool> DeleteCategoryAsync(string id);
        bool CategoryAlreadyExists(ProductCategory category);
        bool CategoryCanBeUpdated(ProductCategory category);
        #endregion

        #region Product
        Task<List<ProductItem>> GetProductsAsync();
        Task<ProductItem> GetProductAsync(string id);
        Task<ProductItem> CreateProductAsync(ProductItem product);
        Task<ProductItem> UpdateProductAsync(string id, ProductItem product);
        Task<bool> DeleteProductAsync(string id);
        bool ProductAlreadyExists(ProductItem product);
        bool ProductCanBeUpdated(ProductItem product);
        #endregion
    }
}
