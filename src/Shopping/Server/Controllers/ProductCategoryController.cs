using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Server.Data;
using Shopping.Shared.Data;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Model.Account;
using Shopping.Shared.Services;

namespace Shopping.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = ShoppingUserPolicies.IsProductCategoryModifier)]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategories _categories;
        public ProductCategoryController(IProductCategories categories)
        {
            _categories = categories;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<ProductCategory>>> GetProductCategories()
        {
            var categories = await _categories.GetAllAsync();
            return Ok(categories);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategory>> GetProductCategory(string id)
        {
            ProductCategory category;
            try
            {
                category = await _categories.GetAsync(id);
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<ProductCategory>> PostProductCategory(ProductCategory category)
        {
            ProductCategory createdCategory;
            try
            {
                createdCategory = await _categories.CreateAsync(category);
            }
            catch (ItemAlreadyExistsException)
            {
                return Conflict();
            }
            catch (Exception)
            {
                throw;
            }

            return Ok(createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductCategory>> UpdateProductCategory(string id, [FromBody] ProductCategory category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }
            try
            {
                await _categories.UpdateAsync(id, category);
            }
            catch (ItemNotFoundException)
            {
                return NotFound($"Could not find category with id {id}");
            }
            catch (ItemAlreadyExistsException)
            {
                return Conflict();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductCategory(string id)
        {
            try
            {
                await _categories.DeleteByIdAsync(id);
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }

            return Ok(true);
        }
    }
}
