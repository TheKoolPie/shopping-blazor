using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Server;
using Shopping.Shared.Data;

namespace Shopping.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingListItemsController : ControllerBase
    {
        private readonly ShoppingDbContext _context;

        public ShoppingListItemsController(ShoppingDbContext context)
        {
            _context = context;
        }

        // GET: api/ShoppingListItems
        [HttpGet]
        public async Task<ActionResult<List<ShoppingListItem>>> GetShoppingListItems()
        {
            return await _context.ShoppingListItems.ToListAsync();
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

            return shoppingListItem;
        }

        // PUT: api/ShoppingListItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingListItem(string id, ShoppingListItem shoppingListItem)
        {
            if (id != shoppingListItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(shoppingListItem).State = EntityState.Modified;

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

            return NoContent();
        }

        // POST: api/ShoppingListItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ShoppingListItem>> PostShoppingListItem(ShoppingListItem shoppingListItem)
        {
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
    }
}
