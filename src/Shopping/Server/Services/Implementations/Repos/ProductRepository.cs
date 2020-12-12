using Shopping.Shared.Data;
using Shopping.Shared.Data.Abstractions;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Server.Services.Implementations
{
    public class ProductRepository : IProducts
    {
        private readonly IShoppingDataRepository _context;
        public ProductRepository(IShoppingDataRepository context)
        {
            _context = context;
        }
        public async Task<List<ProductItem>> GetAllAsync()
        {
            var products = await _context.Products.ToListAsync();
            foreach (var product in products)
            {
                product.Category = await GetCategoryAsync(product.CategoryId);
            }
            return products;
        }
        public async Task<ProductItem> GetAsync(string id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(i => i.Id == id);
            if (product == null)
            {
                throw new ItemNotFoundException(typeof(ProductItem), id);
            }
            product.Category = await GetCategoryAsync(product.CategoryId);
            return product;
        }

        public async Task<ProductItem> CreateAsync(ProductItem item)
        {
            if (ItemAlreadyExists(item))
            {
                throw new ItemAlreadyExistsException(typeof(ProductItem), item.Id);
            }

            item.Category = await GetCategoryAsync(item.CategoryId);

            _context.Products.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<ProductItem> UpdateAsync(string id, ProductItem item)
        {
            if (!ItemCanBeUpdated(item))
            {
                throw new ItemAlreadyExistsException(typeof(ProductItem), item.Id);
            }
            var existing = await GetAsync(id);
            existing.Name = item.Name;
            existing.Unit = item.Unit;
            existing.CategoryId = item.CategoryId;
            existing.Category = await GetCategoryAsync(item.CategoryId);

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            var existing = await GetAsync(id);
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
                await _context.SaveChangesAsync();
                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public bool ItemAlreadyExists(ProductItem item)
        {
            var all = _context.Products.ToList();
            return all.Any(
                            p => p.Id == item.Id ||
                            p.Name.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase)
                          );
        }
        public bool ItemCanBeUpdated(ProductItem item)
        {
            var all = _context.Products.ToList();
            var restWithOutCurrentItem = all.Where(p => p.Id != item.Id).ToList();

            return !(restWithOutCurrentItem.Any(p => p.Name.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase)));
        }

        private async Task<ProductCategory> GetCategoryAsync(string id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        private async Task<List<ShoppingList>> GetShoppingListsWithProduct(string productItemId)
        {
            var shoppinglist = await _context.ShoppingLists.ToListAsync();
            foreach (var list in shoppinglist)
            {
                list.Items = (await _context.ShoppingListItems.ToListAsync())
                    .Where(p => p.ProductItemId == productItemId)
                    .ToList();
            }
            shoppinglist = shoppinglist
                .Where(i => i.Items.Any(p => p.ProductItemId == productItemId))
                .ToList();
            return shoppinglist;
        }
    }
}
