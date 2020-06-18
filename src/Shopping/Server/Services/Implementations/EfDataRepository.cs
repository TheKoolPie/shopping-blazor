using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopping.Server.Data;
using Shopping.Shared.Data;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Server.Services.Implementations
{
    public class EfDataRepository : IDataRepository
    {
        private readonly ShoppingDbContext _context;
        private readonly ILogger<EfDataRepository> _logger;
        public EfDataRepository(ShoppingDbContext context, ILogger<EfDataRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        #region Category
        public async Task<List<ProductCategory>> GetCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }
        public async Task<ProductCategory> GetCategoryAsync(string id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(i => i.Id == id);
            if (category == null)
            {
                throw new ItemNotFoundException(typeof(ProductCategory), id);
            }
            return category;
        }
        public async Task<ProductCategory> CreateCategoryAsync(ProductCategory category)
        {
            if (CategoryAlreadyExists(category))
            {
                throw new ItemAlreadyExistsException(typeof(ProductCategory), category.Id);
            }
            _context.Categories.Add(category);
            await SaveChangesAsync();
            return category;
        }
        public async Task<ProductCategory> UpdateCategoryAsync(string id, ProductCategory category)
        {
            if (!CategoryCanBeUpdated(category))
            {
                throw new ItemAlreadyExistsException(typeof(ProductCategory), category.Id);
            }
            var existing = await GetCategoryAsync(id);

            existing.Name = category.Name;
            existing.ColorCode = category.ColorCode;

            await SaveChangesAsync();

            return existing;
        }
        public async Task<bool> DeleteCategoryAsync(string id)
        {
            var existing = await GetCategoryAsync(id);

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
                await SaveChangesAsync();
                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }
        public bool CategoryAlreadyExists(ProductCategory category)
        {
            var all = _context.Categories.ToList();
            return all
                    .Any(
                        c => c.Id == category.Id ||
                        c.Name.Equals(category.Name, StringComparison.InvariantCultureIgnoreCase) ||
                        c.ColorCode.Equals(category.ColorCode, StringComparison.InvariantCultureIgnoreCase)
                        );
        }
        public bool CategoryCanBeUpdated(ProductCategory category)
        {
            var all = _context.Categories.ToList();
            var restWithOutCurrentItem = all.Where(c => c.Id != category.Id).ToList();

            return !(restWithOutCurrentItem
                        .Any(
                            c => c.Name.Equals(category.Name, StringComparison.InvariantCultureIgnoreCase) ||
                            c.ColorCode.Equals(category.ColorCode, StringComparison.InvariantCultureIgnoreCase)
                            )
                    );
        }
        #endregion

        #region Product
        public async Task<List<ProductItem>> GetProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            foreach (var product in products)
            {
                product.Category = await GetCategoryAsync(product.CategoryId);
            }
            return products;
        }
        public async Task<ProductItem> GetProductAsync(string id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(i => i.Id == id);
            product.Category = await GetCategoryAsync(product.CategoryId);
            return product;
        }
        public async Task<ProductItem> CreateProductAsync(ProductItem product)
        {
            if (ProductAlreadyExists(product))
            {
                throw new ItemAlreadyExistsException(typeof(ProductItem), product.Id);
            }

            product.Category = await GetCategoryAsync(product.CategoryId);

            _context.Products.Add(product);
            await SaveChangesAsync();
            return product;
        }
        public async Task<ProductItem> UpdateProductAsync(string id, ProductItem product)
        {
            if (!ProductCanBeUpdated(product))
            {
                throw new ItemAlreadyExistsException(typeof(ProductItem), product.Id);
            }
            var existing = await GetProductAsync(id);
            existing.Name = product.Name;
            existing.Unit = product.Unit;
            existing.CategoryId = product.CategoryId;
            existing.Category = await GetCategoryAsync(product.CategoryId);

            await SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            var existing = await GetProductAsync(id);
            var shoppinglists = await GetShoppingListsWithProduct(id);
            if (shoppinglists.Count > 0)
            {
                foreach (var list in shoppinglists)
                {
                    var deleteItem = list.Items.FirstOrDefault(i => i.ProductItemId == id);
                    if (deleteItem != null)
                    {
                        list.Items.Remove(deleteItem);
                    }
                }
            }
            _context.Products.Remove(existing);

            bool result = false;
            try
            {
                await SaveChangesAsync();
                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public bool ProductAlreadyExists(ProductItem product)
        {
            var all = _context.Products.ToList();
            return all.Any(
                            p => p.Id == product.Id ||
                            p.Name.Equals(product.Name, StringComparison.InvariantCultureIgnoreCase)
                          );
        }

        public bool ProductCanBeUpdated(ProductItem product)
        {
            var all = _context.Products.ToList();
            var restWithOutCurrentItem = all.Where(p => p.Id != product.Id).ToList();

            return !(restWithOutCurrentItem.Any(p => p.Name.Equals(product.Name, StringComparison.InvariantCultureIgnoreCase)));
        }
        #endregion



        #region Common Helpers
        private async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("Could not save changes");
                throw new PersistencyException("Could not save changes", e);
            }
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
        #endregion

    }
}
