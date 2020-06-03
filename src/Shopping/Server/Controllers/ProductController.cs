using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Shared.Data;
using Shopping.Server.Data;
using Shopping.Shared.Model.Account;

namespace Shopping.Server.Controllers
{
    [Authorize(Policy = ShoppingUserPolicies.IsProductCategoryModifier)]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ShoppingDbContext _context;

        public ProductController(ShoppingDbContext context)
        {
            _context = context;

        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<ProductItem>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            var productCategories = await _context.Categories.ToListAsync();
            foreach (var product in products)
            {
                product.Category = productCategories.FirstOrDefault(c => c.Id.Equals(product.Category.Id));
            }

            return products;
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductItem>> GetProduct(string id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(e => e.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<ProductItem>> PostProduct(ProductItem product)
        {
            product.Category = await _context.Categories.FirstOrDefaultAsync(c => c.Name.Equals(product.Category.Name));

            _context.Products.Add(product);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(product.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductItem>> DeleteProduct(string id)
        {
            var products = await _context.Products.ToListAsync();
            var product = products.FirstOrDefault(c => c.Id.Equals(id));
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        private bool ProductExists(string name)
        {
            return _context.Products.Any(e => e.Name.Equals(name));
        }
    }
}
