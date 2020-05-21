using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Server;
using Shopping.Shared;

namespace Shopping.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingItemsController : ControllerBase
    {
        private readonly ShoppingDbContext _context;

        public ShoppingItemsController(ShoppingDbContext context)
        {
            _context = context;
        }

        // GET: api/ShoppingItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingItem>>> GetItems()
        {
            return await _context.Items.ToListAsync();
        }

        // GET: api/ShoppingItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingItem>> GetShoppingItem(string id)
        {
            var shoppingItem = await _context.Items.FindAsync(id);

            if (shoppingItem == null)
            {
                return NotFound();
            }

            return shoppingItem;
        }

        // PUT: api/ShoppingItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingItem(string id, ShoppingItem shoppingItem)
        {
            if (id != shoppingItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(shoppingItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingItemExists(id))
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

        // POST: api/ShoppingItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ShoppingItem>> PostShoppingItem(ShoppingItem shoppingItem)
        {
            _context.Items.Add(shoppingItem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ShoppingItemExists(shoppingItem.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetShoppingItem", new { id = shoppingItem.Id }, shoppingItem);
        }

        // DELETE: api/ShoppingItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ShoppingItem>> DeleteShoppingItem(string id)
        {
            var shoppingItem = await _context.Items.FindAsync(id);
            if (shoppingItem == null)
            {
                return NotFound();
            }

            _context.Items.Remove(shoppingItem);
            await _context.SaveChangesAsync();

            return shoppingItem;
        }

        private bool ShoppingItemExists(string id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
