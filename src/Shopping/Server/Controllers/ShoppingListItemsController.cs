using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopping.Server.Data;
using Shopping.Shared.Data;

namespace Shopping.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingListItemsController : ControllerBase
    {
        private readonly ShoppingDbContext _context;
        private readonly ILogger<ShoppingListItemsController> _logger;
        public ShoppingListItemsController(ShoppingDbContext context, ILogger<ShoppingListItemsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/ShoppingListItems
        [HttpGet]
        public async Task<ActionResult<List<ShoppingListItem>>> GetShoppingListItems()
        {
            var listItems = await _context.ShoppingListItems.ToListAsync();
            var products = await _context.Products.ToListAsync();
            foreach (var item in listItems)
            {
                item.ProductItem = products.FirstOrDefault(p => p.Id == item.ProductItemId);
            }
            return listItems;
        }

        // GET: api/ShoppingListItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingListItem>> GetShoppingListItem(string id)
        {
            var shoppingListItem = await _context.ShoppingListItems.FindAsync(id);
            if (shoppingListItem == null)
            {
                return NotFound();
            }

            shoppingListItem.ProductItem = await _context.Products.FirstOrDefaultAsync(p => p.Id == shoppingListItem.ProductItemId);

            return shoppingListItem;
        }

        // PUT: api/ShoppingListItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<ShoppingListItem>> PutShoppingListItem(string id, ShoppingListItem shoppingListItem)
        {
            if (id != shoppingListItem.Id)
            {
                _logger.LogError($"Target id does not match item id: {id} --> {shoppingListItem.Id}");
                return BadRequest();
            }

            var existingItem = await _context.ShoppingListItems.FirstOrDefaultAsync(i => i.Id == id);
            if (HasChanged(existingItem, shoppingListItem))
            {
                Change(existingItem, shoppingListItem);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingListItemExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return shoppingListItem;
        }

        // POST: api/ShoppingListItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ShoppingListItem>> PostShoppingListItem(ShoppingListItem shoppingListItem)
        {
            shoppingListItem.ProductItem = await GetProductItem(shoppingListItem.ProductItem.Id);
            _context.ShoppingListItems.Add(shoppingListItem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ShoppingListItemExists(shoppingListItem.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetShoppingListItem", new { id = shoppingListItem.Id }, shoppingListItem);
        }

        // DELETE: api/ShoppingListItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ShoppingListItem>> DeleteShoppingListItem(string id)
        {
            var shoppingListItem = await _context.ShoppingListItems.FindAsync(id);
            if (shoppingListItem == null)
            {
                return NotFound();
            }

            _context.ShoppingListItems.Remove(shoppingListItem);
            await _context.SaveChangesAsync();

            return shoppingListItem;
        }

        private bool ShoppingListItemExists(string id)
        {
            return _context.ShoppingListItems.Any(e => e.Id == id);
        }

        private async Task<ProductItem> GetProductItem(string id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        private bool HasChanged(ShoppingListItem dbItem, ShoppingListItem targetItem)
        {
            return dbItem.Amount != targetItem.Amount || dbItem.Done != targetItem.Done;
        }
        private void Change(ShoppingListItem dbItem, ShoppingListItem targetItem)
        {
            dbItem.Amount = targetItem.Amount;
            dbItem.Done = targetItem.Done;
        }
    }
}
