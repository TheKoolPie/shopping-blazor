using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Shared.Data;
using Shopping.Server.Data;
using Shopping.Shared.Model.Account;
using Shopping.Shared.Services;
using Shopping.Shared.Exceptions;

namespace Shopping.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = ShoppingUserPolicies.IsProductCategoryModifier)]
    public class ProductController : ControllerBase
    {
        private readonly IProducts _products;
        public ProductController(IProducts products)
        {
            _products = products;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<ProductItem>>> GetProducts()
        {
            var products = await _products.GetAllAsync();
            return Ok(products);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductItem>> GetProduct(string id)
        {
            ProductItem item;
            try
            {
                item = await _products.GetAsync(id);
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<ProductItem>> PostProduct(ProductItem product)
        {
            ProductItem item;
            try
            {
                item = await _products.CreateAsync(product);
            }
            catch (ItemAlreadyExistsException)
            {
                return Conflict();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(item);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductItem>> UpdateProduct(string id, [FromBody] ProductItem product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            try
            {
                await _products.UpdateAsync(id, product);
            }
            catch (ItemNotFoundException)
            {
                return NotFound($"Could not find product with id {id}");
            }
            catch (ItemAlreadyExistsException)
            {
                return Conflict();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(product);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteProduct(string id)
        {
            try
            {
                await _products.DeleteByIdAsync(id);
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }

            return Ok(true);
        }
    }
}
