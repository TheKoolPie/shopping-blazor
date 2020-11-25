using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopping.Server.Data;
using Shopping.Shared.Data;
using Shopping.Shared.Data.Abstractions;
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
        private readonly IShoppingDataRepository _context;
        public ProductCategoryRepository(IShoppingDataRepository context)
        {
            _context = context;
        }

        public async Task<List<ProductCategory>> GetAllAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }

        public async Task<ProductCategory> GetAsync(string id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(i => i.Id == id);
            if (category == null)
            {
                throw new ItemNotFoundException(typeof(ProductCategory), id);
            }
            return category;
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            var existing = await GetAsync(id);

            var products = await GetProductsWithCategory(id);
            if (products.Count > 0)
            {
                foreach (var product in products)
                {
                    var shoppinglists = await GetShoppingListsWithProduct(product.Id);
                    if (shoppinglists.Count > 0)
                    {
                        foreach (var list in shoppinglists)
                        {
                            var deleteItem = list.Items.FirstOrDefault(i => i.ProductItemId == product.Id);
                            if (deleteItem != null)
                            {
                                list.Items.Remove(deleteItem);
                            }
                        }
                    }
                    _context.Products.Remove(product);
                }
            }

            _context.Categories.Remove(existing);

            bool result = false;
            try
            {
                await _context.SaveChangesAsync();
                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public async Task<ProductCategory> CreateAsync(ProductCategory item)
        {
            if (ItemAlreadyExists(item))
            {
                throw new ItemAlreadyExistsException(typeof(ProductCategory), item.Id);
            }
            _context.Categories.Add(item);

            await _context.SaveChangesAsync();

            await CreateStoreCatAssignmentsForCategory(item);

            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<ProductCategory> UpdateAsync(string id, ProductCategory item)
        {
            if (!ItemCanBeUpdated(item))
            {
                throw new ItemAlreadyExistsException(typeof(ProductCategory), item.Id);
            }
            var existing = await GetAsync(id);

            existing.Name = item.Name;

            await _context.SaveChangesAsync();

            return existing;
        }

        public bool ItemAlreadyExists(ProductCategory item)
        {
            var all = _context.Categories.ToList();
            return all.Any(c => c.Id == item.Id || c.Name.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase));
        }
        public bool ItemCanBeUpdated(ProductCategory item)
        {
            var all = _context.Categories.ToList();
            var restWithOutCurrentItem = all.Where(c => c.Id != item.Id).ToList();

            return !(restWithOutCurrentItem.Any(c => c.Name.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase)));
        }

        private async Task<List<ProductItem>> GetProductsWithCategory(string categoryId)
        {
            var products = await _context.Products.ToListAsync();
            products = products
                .Where(p => p.CategoryId == categoryId)
                .ToList();

            return products;
        }
        private async Task<List<ShoppingList>> GetShoppingListsWithProduct(string productItemId)
        {
            var shoppinglist = await _context.ShoppingLists.ToListAsync();
            shoppinglist = shoppinglist
                .Where(i => i.Items.Any(p => p.ProductItemId == productItemId))
                .ToList();
            return shoppinglist;
        }

        private async Task CreateStoreCatAssignmentsForCategory(ProductCategory category)
        {
            var assignments = await _context.StoreProductCategories.ToListAsync();

            var storeIds = assignments.Select(a => a.StoreId).Distinct().ToList();

            foreach (var storeId in storeIds)
            {
                int highestRankingValueInStore = assignments
                    .Where(a => a.StoreId == storeId)
                    .Select(a => a.RankingValue)
                    .Max();
                _context.StoreProductCategories.Add(
                    new StoreProductCategory
                    {
                        ProductCategoryId = category.Id,
                        RankingValue = highestRankingValueInStore + 1,
                        StoreId = storeId
                    });
            }
        }
    }
}
