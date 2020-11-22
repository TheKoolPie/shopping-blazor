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
        Task<List<StoreProductCategory>> CreateAsync(List<StoreProductCategory> storeProductCats);
        Task<StoreProductCategory> UpdateAsync(string assignmentId, StoreProductCategory storeProductCat);
        Task<List<StoreProductCategory>> UpdateOfStore(string storeId, List<StoreProductCategory> assignments);
        Task<bool> DeleteByIdAsync(string assignmentId);
        Task<bool> DeleteAllOfStore(string storeId);
    }
}
