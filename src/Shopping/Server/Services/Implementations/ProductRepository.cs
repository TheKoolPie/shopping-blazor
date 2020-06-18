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
    public class ProductRepository : IProducts
    {
        private readonly IDataRepository _data;
        public ProductRepository(IDataRepository data)
        {
            _data = data;
        }
        public async Task<List<ProductItem>> GetAllAsync()
        {
            return await _data.GetProductsAsync();
        }
        public async Task<ProductItem> GetAsync(string id)
        {
            return await _data.GetProductAsync(id);
        }

        public async Task<ProductItem> CreateAsync(ProductItem item)
        {
            return await _data.CreateProductAsync(item);
        }

        public async Task<ProductItem> UpdateAsync(string id, ProductItem item)
        {
            return await _data.UpdateProductAsync(id, item);
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            return await _data.DeleteProductAsync(id);
        }
    }
}
