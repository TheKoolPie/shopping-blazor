using Shopping.Shared.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopping.Shared.Services.Interfaces
{
    public interface IStoreProductCatRepository
    {
        Task<StoreProductCategory> GetAsync(string assignmentId);
        Task<StoreProductCategory> GetAsync(string storeId, string productCatId);
        Task<List<StoreProductCategory>> GetAssignmentsByStoreIdAsync(string storeId);
        Task<StoreProductCategory> CreateAsync(StoreProductCategory storeProductCat);
        Task<StoreProductCategory> UpdateAsync(string assignmentId, StoreProductCategory storeProductCat);
        Task<bool> DeleteByIdAsync(string assignmentId);
    }
}
