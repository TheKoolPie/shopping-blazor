using Shopping.Shared.Data;
using Shopping.Shared.Data.Abstractions;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Server.Services.Implementations.Repos
{
    public class StoreProductCatRepository : IStoreProductCatRepository
    {
        private readonly IShoppingDataRepository _context;
        public StoreProductCatRepository(IShoppingDataRepository context)
        {
            _context = context;
        }
        public async Task<StoreProductCategory> GetAsync(string assignmentId)
        {
            var existing = await _context.StoreProductCategories
                .FirstOrDefaultAsync(a => a.StoreProductCategoryId == assignmentId);
            if (existing == null)
            {
                throw new ItemNotFoundException(typeof(StoreProductCategory), assignmentId);
            }
            return existing;
        }
        public async Task<StoreProductCategory> GetAsync(string storeId, string productCatId)
        {
            var existing = await _context.StoreProductCategories
                .FirstOrDefaultAsync(a => a.StoreId == storeId && a.ProductCategoryId == productCatId);
            if (existing == null)
            {
                throw new ItemNotFoundException(typeof(StoreProductCategory),
                    $"[StoreId:{storeId}, ProductCategoryId:{productCatId}]");
            }
            return existing;
        }
        public async Task<List<StoreProductCategory>> GetAssignmentsByStoreIdAsync(string storeId)
        {
            var assignments = await _context.StoreProductCategories.ToListAsync();
            return assignments
                .Where(a => a.StoreId == storeId)
                .ToList();
        }
        public async Task<StoreProductCategory> CreateAsync(StoreProductCategory storeProductCat)
        {
            if (AssignmentExists(storeProductCat))
            {
                throw new ItemAlreadyExistsException(typeof(StoreProductCategory), storeProductCat.StoreProductCategoryId);
            }
            _context.StoreProductCategories.Add(storeProductCat);
            await _context.SaveChangesAsync();

            return storeProductCat;
        }
        public async Task<List<StoreProductCategory>> CreateAsync(List<StoreProductCategory> storeProductCats)
        {
            foreach (var assignment in storeProductCats)
            {
                if (AssignmentExists(assignment))
                {
                    throw new ItemAlreadyExistsException(typeof(StoreProductCategory), assignment.StoreProductCategoryId);
                }
                _context.StoreProductCategories.Add(assignment);
            }
            await _context.SaveChangesAsync();

            return storeProductCats;
        }
        public async Task<StoreProductCategory> UpdateAsync(string assignmentId, StoreProductCategory storeProductCat)
        {
            var existing = await GetAsync(assignmentId);
            existing.RankingValue = storeProductCat.RankingValue;

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteByIdAsync(string assignmentId)
        {
            bool result = false;
            try
            {
                var item = await GetAsync(assignmentId);
                _context.StoreProductCategories.Remove(item);
                await _context.SaveChangesAsync();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public async Task<bool> DeleteAllOfStore(string storeId)
        {
            var allAssignmentsOfStore = await GetAssignmentsByStoreIdAsync(storeId);

            foreach (var assignment in allAssignmentsOfStore)
            {
                _context.StoreProductCategories.Remove(assignment);
            }
            bool result = false;
            try
            {
                await _context.SaveChangesAsync();
                result = true;
            }
            catch { result = false; }

            return result;
        }

        private bool AssignmentExists(StoreProductCategory storeProductCat)
        {
            var assignments = _context.StoreProductCategories.ToList();
            return assignments.Any(a =>
                a.StoreProductCategoryId == storeProductCat.StoreProductCategoryId ||
                (a.ProductCategoryId == storeProductCat.ProductCategoryId &&
                a.StoreId == storeProductCat.StoreId)
            );
        }

        public async Task<List<StoreProductCategory>> UpdateOfStore(string storeId, List<StoreProductCategory> assignments)
        {
            var assignmentsOfStore = (await GetAssignmentsByStoreIdAsync(storeId))
                .OrderBy(a => a.StoreProductCategoryId);

            foreach (var assignment in assignmentsOfStore)
            {
                var updatedAssignment = assignments.FirstOrDefault(a => a.StoreProductCategoryId == assignment.StoreProductCategoryId);
                if (updatedAssignment == null)
                {
                    throw new ItemNotFoundException(typeof(StoreProductCategory), assignment.StoreProductCategoryId);
                }
                assignment.RankingValue = updatedAssignment.RankingValue;
            }

            await _context.SaveChangesAsync();

            return assignmentsOfStore.ToList();
        }
    }
}
