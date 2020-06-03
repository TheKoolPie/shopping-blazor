using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopping.Server.Data;
using Shopping.Shared.Data;
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
            return await _context.Categories.ToListAsync();
        }

        public override async Task<ProductCategory> GetAsync(string id)
        {
            return await _context.Categories.FirstOrDefaultAsync(i => i.Id == id);
        }

        public override bool ItemAlreadyExists(ProductCategory item)
        {
            return _context.Categories.Any(i => i.Id == item.Id ||
            i.Name.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase) ||
            i.ColorCode == item.ColorCode);
        }

        public override bool ItemHasChanged(ProductCategory existing, ProductCategory updated)
        {
            return existing.Name != updated.Name || existing.ColorCode != updated.ColorCode;
        }

        public override void UpdateExistingItem(ProductCategory existing, ProductCategory update)
        {
            existing.Name = update.Name;
            existing.ColorCode = update.ColorCode;
        }
    }
}
