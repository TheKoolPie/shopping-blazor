using Shopping.Shared.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.Services
{
    public interface IProductCategoryAccess
    {
        Task<List<ProductCategory>> GetAllAsync();
        Task<ProductCategory> GetAsync(string name);
        Task<ProductCategory> SaveAsync(ProductCategory category);
        Task<ProductCategory> UpdateAsync(string id, ProductCategory category);
        Task<bool> DeleteAsync(string id);

    }
}
