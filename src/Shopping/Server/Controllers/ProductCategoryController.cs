using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shopping.Shared.Data;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Model.Account;
using Shopping.Shared.Results;
using Shopping.Shared.Services.Interfaces;

namespace Shopping.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = ShoppingUserPolicies.IsProductCategoryModifier)]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategories _categories;
        private readonly ILogger<ProductCategoryController> _logger;
        public ProductCategoryController(IProductCategories categories, ILogger<ProductCategoryController> logger)
        {
            _categories = categories;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ProductCategoryResult>> GetProductCategories()
        {
            var categories = await _categories.GetAllAsync();

            var result = new ProductCategoryResult()
            {
                IsSuccessful = true,
                ResultData = categories
            };

            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategoryResult>> GetProductCategory(string id)
        {
            ProductCategoryResult result = new ProductCategoryResult();
            try
            {
                var category = await _categories.GetAsync(id);
                result.IsSuccessful = true;
                result.ResultData = new List<ProductCategory>()
                {
                    category
                };
            }
            catch (ItemNotFoundException e)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add(e.Message);

                _logger?.LogError(e.Message);

                return NotFound(result);
            }
            catch(Exception e)
            {
                _logger.LogDebug($"Unknown error", e);
                throw e;
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ProductCategoryResult>> PostProductCategory(ProductCategory category)
        {
            ProductCategoryResult result = new ProductCategoryResult();
            try
            {
                var createdCategory = await _categories.CreateAsync(category);

                result.IsSuccessful = true;
                result.ResultData.Add(createdCategory);
            }
            catch (ItemAlreadyExistsException e)
            {
                _logger?.LogError(e.Message);

                result.IsSuccessful = false;
                result.ErrorMessages.Add(e.Message);

                return Conflict(result);
            }
            catch (Exception e)
            {
                _logger.LogDebug($"Unknown error", e);
                throw e;
            }

            return Created("", result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductCategoryResult>> UpdateProductCategory(string id, [FromBody] ProductCategory category)
        {
            ProductCategoryResult result = new ProductCategoryResult();

            if (id != category.Id)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add("Id does not match with object id");
                return BadRequest(result);
            }
  
            try
            {
                var updated = await _categories.UpdateAsync(id, category);
                result.IsSuccessful = true;
                result.ResultData.Add(updated);
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
                _logger.LogDebug($"Unknown error", e);
                throw e;
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductCategoryResult>> DeleteProductCategory(string id)
        {
            ProductCategoryResult result = new ProductCategoryResult();
            try
            {
                await _categories.DeleteByIdAsync(id);
                result.IsSuccessful = true;
            }
            catch (ItemNotFoundException e)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add(e.Message);
                return NotFound(result);
            }
            catch (Exception e)
            {
                _logger.LogDebug($"Unknown error", e);
                throw e;
            }

            return Ok(result);
        }
    }
}
