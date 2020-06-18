using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    public class ProductCategoryRepository : IProductCategories
    {
        private readonly IDataRepository _data;
        public ProductCategoryRepository(IDataRepository data)
        {
            _data = data;
        }

        public async Task<List<ProductCategory>> GetAllAsync()
        {
            return await _data.GetCategoriesAsync();
        }

        public async Task<ProductCategory> GetAsync(string id)
        {
           return await _data.GetCategoryAsync(id);
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            return await _data.DeleteCategoryAsync(id);
        }

        public async Task<ProductCategory> CreateAsync(ProductCategory item)
        {
            return await _data.CreateCategoryAsync(item);
        }

        public async Task<ProductCategory> UpdateAsync(string id, ProductCategory item)
        {
            return await _data.UpdateCategoryAsync(id, item);
        }
    }
}
