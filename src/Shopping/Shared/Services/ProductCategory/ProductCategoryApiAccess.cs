using Microsoft.Extensions.Logging;
using Shopping.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.Services
{
    public class ProductCategoryApiAccess : IProductCategoryAccess
    {
        private string baseAddress = "api/ProductCategory";

        private readonly HttpClient _client;
        private readonly ILogger<ProductCategoryApiAccess> _logger;
        public ProductCategoryApiAccess(HttpClient httpClient, ILogger<ProductCategoryApiAccess> logger)
        {
            _client = httpClient;
            _logger = logger;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var response = await _client.DeleteAsync($"{baseAddress}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                _logger?.LogError(response.ReasonPhrase);
                return false;
            }
            return true;
        }

        public async Task<List<ProductCategory>> GetAllAsync()
        {
            var categories = await _client.GetFromJsonAsync<List<ProductCategory>>(baseAddress);
            return categories;
        }

        public async Task<ProductCategory> GetAsync(string name)
        {
            return await _client.GetFromJsonAsync<ProductCategory>($"{baseAddress}/{name}");
        }

        public async Task<ProductCategory> SaveAsync(ProductCategory category)
        {
            ProductCategory retVal = null;
            var response = await _client.PostAsJsonAsync<ProductCategory>(baseAddress, category);
            if (response.IsSuccessStatusCode)
            {
                retVal = await response.Content.ReadFromJsonAsync<ProductCategory>();
            }
            else
            {
                _logger.LogError(response.ReasonPhrase);
            }
            return retVal;
        }

        public Task<ProductCategory> UpdateAsync(string id, ProductCategory category)
        {
            throw new NotImplementedException();
        }
    }
}
