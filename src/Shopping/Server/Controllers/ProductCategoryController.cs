﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Shared.Data;

namespace Shopping.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly ShoppingDbContext _context;

        public ProductCategoryController(ShoppingDbContext context)
        {
            _context = context;
            
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductCategory>>> GetProductCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategory>> GetProductCategory(string name)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(e => e.Name.Equals(name));
            if(category == null)
            {
                return NotFound();
            }
            return category;
        }

        [HttpPost]
        public async Task<ActionResult<ProductCategory>> PostProductCategory(ProductCategory category)
        {
            _context.Categories.Add(category);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CategoryExists(category.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductCategory>> DeleteProductCategory(string id)
        {
            var categories = await _context.Categories.ToListAsync();
            var category = categories.FirstOrDefault(c => c.Id.ToString().Equals(id));
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return category;
        }

        private bool CategoryExists(string name) 
        {
            return _context.Categories.Any(e => e.Name.Equals(name));
        }
    }
}
