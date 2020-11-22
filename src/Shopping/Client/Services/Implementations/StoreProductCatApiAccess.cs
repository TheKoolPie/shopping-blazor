using Microsoft.Extensions.Logging;
using Shopping.Client.Services.Implementations.Base;
using Shopping.Shared.Data;
using Shopping.Shared.Results.Entities;
using Shopping.Shared.Services;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Shopping.Client.Services.Implementations
{
    public class StoreProductCatApiAccess : BaseShoppingApiImpl<StoreProductCategory, StoreProductCategoryResult>, IStoreProductCatRepository
    {
        public StoreProductCatApiAccess(IAuthService authService, ILogger<StoreProductCatApiAccess> logger)
            : base("StoreProductCategory", authService, logger)
        {
        }

        public async Task<List<StoreProductCategory>> CreateAsync(List<StoreProductCategory> storeProductCats)
        {
            var client = await GetApiClient();

            var response = await client.PostAsJsonAsync($"{_baseUri}/CreateAssignments", storeProductCats);
            CheckForUnauthorized(response);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<StoreProductCategoryResult>();
                if (result.IsSuccessful)
                {
                    return result.ResultData;
                }
                else
                {
                    _logger.LogError("Result is not set to successful", result.ErrorMessages);
                }
            }
            else
            {
                _logger.LogError($"Response has no success status code: {response.StatusCode}");
            }
            return null;
        }

        public async Task<bool> DeleteAllOfStore(string storeId)
        {
            return await SendDelete($"{_baseUri}/AllOfStore/{storeId}");
        }

        public async Task<List<StoreProductCategory>> GetAssignmentsByStoreIdAsync(string storeId)
        {
            var client = await GetApiClient();
            var response = await client.GetAsync($"{_baseUri}/GetByStoreId/{storeId}");
            CheckForUnauthorized(response);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<StoreProductCategoryResult>();
                if (result.IsSuccessful)
                {
                    return result.ResultData;
                }
                else
                {
                    _logger.LogError("Result is not set to successful", result.ErrorMessages);
                }
            }
            else
            {
                _logger.LogError($"Response has no success status code: {response.StatusCode}");
            }

            return null;
        }

        public Task<StoreProductCategory> GetAsync(string storeId, string productCatId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<StoreProductCategory>> UpdateOfStore(string storeId, List<StoreProductCategory> assignments)
        {
            var client = await GetApiClient();

            var response = await client.PutAsJsonAsync($"{_baseUri}/UpdateOfStore/{storeId}", assignments);
            CheckForUnauthorized(response);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<StoreProductCategoryResult>();
                if (result.IsSuccessful)
                {
                    return result.ResultData;
                }
                else
                {
                    _logger.LogError("Result is not set to successful", result.ErrorMessages);
                }
            }
            else
            {
                _logger.LogError($"Response has no success status code: {response.StatusCode}");
            }
            return null;
        }
    }
}
