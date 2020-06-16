﻿using Microsoft.EntityFrameworkCore;
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
    public class ProductRepository : CRUDDbContextBaseImpl<ProductItem>, IProducts
    {
        private readonly IProductCategories _categories;
        public ProductRepository(ShoppingDbContext context,
            ILogger<ProductItem> logger, IProductCategories categories)
            : base(context, logger)
        {
            _categories = categories;
        }

        public override async Task<ProductItem> CreateAsync(ProductItem item)
        {
            item.Category = await _categories.GetAsync(item.CategoryId);
            return await base.CreateAsync(item);
        }

        public override async Task<List<ProductItem>> GetAllAsync()
        {
            var products = await _context.Products.ToListAsync();
            foreach (var product in products)
            {
                product.Category = await _categories.GetAsync(product.CategoryId);
            }
            return products;
        }

        public override async Task<ProductItem> GetAsync(string id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(i => i.Id == id);
            if (product == null)
            {
                throw new ItemNotFoundException();
            }
            product.Category = await _categories.GetAsync(product.CategoryId);
            return product;
        }

        public override bool ItemAlreadyExists(ProductItem item)
        {
            var products = _context.Products.ToList();
            return products.Any(i => i.Id == item.Id || i.Name.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase));
        }

        public override void UpdateExistingItem(ProductItem existing, ProductItem update)
        {
            existing.Name = update.Name;
            existing.Unit = update.Unit;
            existing.CategoryId = update.CategoryId;
            existing.Category = update.Category;
        }
    }
}
