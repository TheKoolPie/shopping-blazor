using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopping.Server.Data;
using Shopping.Shared.Data;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Server.Services.Implementations
{
    public class ProductCategoryRepository : CRUDDbContextBaseImpl<ProductCategory>, IProductCategories
    {
        public ProductCategoryRepository(ShoppingDbContext context, ILogger<ProductCategory> logger)
            : base(context, logger)
        {
        }

        public override async Task<List<ProductCategory>> GetAllAsync()
        {
            List<ProductCategory> categories = new List<ProductCategory>();
            try
            {
                categories = await _context.Categories.ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not load categories");
            }
            return categories;
        }

        public override async Task<ProductCategory> GetAsync(string id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(i => i.Id == id);
            if (category == null)
            {
                throw new ItemNotFoundException();
            }
            return category;
        }

        public override async Task<bool> DeleteByIdAsync(string id)
        {
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
                    _context.Remove<ProductItem>(product);
                }
            }

            return await base.DeleteByIdAsync(id);
        }

        public override bool ItemAlreadyExists(ProductCategory item)
        {
            var categories = _context.Categories.ToList();
            return categories.Any(i => i.Id == item.Id ||
            i.Name.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase) ||
            i.ColorCode == item.ColorCode);
        }
        public override void UpdateExistingItem(ProductCategory existing, ProductCategory update)
        {
            existing.Name = update.Name;
            existing.ColorCode = update.ColorCode;
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
    }
}
