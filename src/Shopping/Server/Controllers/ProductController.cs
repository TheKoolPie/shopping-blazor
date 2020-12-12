using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.Shared.Data;
using Shopping.Shared.Model.Account;
using Shopping.Shared.Services.Interfaces;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Results;
using Microsoft.Extensions.Logging;

namespace Shopping.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = ShoppingUserPolicies.IsProductCategoryModifier)]
    public class ProductController : ControllerBase
    {
        private readonly IProducts _products;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProducts products, ILogger<ProductController> logger)
        {
            _products = products;
            _logger = logger;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ProductItemResult>> GetProducts()
        {
            var products = await _products.GetAllAsync();

            var result = new ProductItemResult()
            {
                IsSuccessful = true,
                ResultData = products
            };

            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductItemResult>> GetProduct(string id)
        {
            ProductItemResult result = new ProductItemResult();
            try
            {
                var item = await _products.GetAsync(id);
                result.IsSuccessful = true;
                result.ResultData = new List<ProductItem>()
                {
                    item
                };
            }
            catch (ItemNotFoundException e)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add(e.Message);

                _logger?.LogError(e.Message);

                return NotFound(result);
            }
            catch (Exception e)
            {
                _logger.LogDebug($"Unknown error", e);
                throw e;
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ProductItemResult>> PostProduct(ProductItem product)
        {
            ProductItemResult result = new ProductItemResult();
            try
            {
                var item = await _products.CreateAsync(product);
                result.IsSuccessful = true;
                result.ResultData.Add(item);
            }
            catch (ItemAlreadyExistsException e)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add(e.Message);

                _logger?.LogError(e, "Error");

                return Conflict(result);
            }
            catch (Exception e)
            {
                _logger?.LogDebug($"Unknown error", e);
                throw e;
            }
            return Created("", result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductItemResult>> UpdateProduct(string id, [FromBody] ProductItem product)
        {
            ProductItemResult result = new ProductItemResult();

            if (id != product.Id)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add("Id does not match with object id");
                return BadRequest(result);
            }
            try
            {
                var update = await _products.UpdateAsync(id, product);
                result.IsSuccessful = true;
                result.ResultData.Add(update);
            }
            catch (ItemNotFoundException e)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add(e.Message);

                return NotFound(result);
            }
            catch (ItemAlreadyExistsException e)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add(e.Message);

                return Conflict(result);
            }
            catch (Exception e)
            {
                _logger?.LogDebug($"Unknown error", e);
                throw e;
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductItemResult>> DeleteProduct(string id)
        {
            ProductItemResult result = new ProductItemResult();
            try
            {
                var deleteResult = await _products.DeleteByIdAsync(id);
                result.IsSuccessful = deleteResult;
                if (!result.IsSuccessful)
                {
                    result.ErrorMessages.Add($"Something went wrong while deleting '{id}'");
                    return UnprocessableEntity(result);
                }
            }
            catch (ItemNotFoundException e)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add(e.Message);
                return NotFound(result);
            }
            catch (Exception e)
            {
                _logger?.LogDebug($"Unknown error", e);
                throw e;
            }

            return Ok(result);
        }
    }
}
