using Microsoft.Extensions.Logging;
using Shopping.Client.Services.Implementations.Base;
using Shopping.Shared.Data;
using Shopping.Shared.Results.Entities;
using Shopping.Shared.Services;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Client.Services.Implementations
{
    public class StoreProductCatApiAccess : BaseShoppingApiImpl<StoreProductCategory, StoreProductCategoryResult>, IStoreProductCatRepository
    {
        public StoreProductCatApiAccess(IAuthService authService, ILogger<StoreProductCatApiAccess> logger)
            : base("StoreProductCategory", authService, logger)
        {
        }

        public async Task<bool> DeleteAllOfStore(string storeId)
        {
            return await SendDelete($"{_baseUri}/AllOfStore/{storeId}");
        }

        public Task<List<StoreProductCategory>> GetAssignmentsByStoreIdAsync(string storeId)
        {
            throw new NotImplementedException();
        }

        public Task<StoreProductCategory> GetAsync(string storeId, string productCatId)
        {
            throw new NotImplementedException();
        }
    }
}
