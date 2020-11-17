using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.Shared.Data;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Results.Entities;
using Shopping.Shared.Services;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopping.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StoreProductCategoryController : ControllerBase
    {
        private readonly IStoreProductCatRepository _storeProductCatRepository;
        private readonly ICurrentUserProvider _currentUserProvider;
        public StoreProductCategoryController(IStoreProductCatRepository storeProductCatRepository, ICurrentUserProvider currentUserProvider)
        {
            _storeProductCatRepository = storeProductCatRepository;
            _currentUserProvider = currentUserProvider;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<StoreProductCategoryResult>> GetAssignment(string id)
        {
            var assignment = await _storeProductCatRepository.GetAsync(id);
            var result = new StoreProductCategoryResult();
            result.IsSuccessful = true;
            result.ResultData.Add(assignment);
            return Ok(result);
        }
        [HttpGet("GetByStoreId/{id}")]
        public async Task<ActionResult<StoreProductCategoryResult>> GetAssignmentsByStoreId(string id)
        {
            var assignments = await _storeProductCatRepository.GetAssignmentsByStoreIdAsync(id);
            var result = new StoreProductCategoryResult();
            result.IsSuccessful = true;
            result.ResultData = assignments;
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<StoreProductCategoryResult>> CreateAssignment(StoreProductCategory assignment)
        {
            assignment.CreatedAt = DateTime.Now;

            StoreProductCategoryResult result = new StoreProductCategoryResult();
            try
            {
                var created = await _storeProductCatRepository.CreateAsync(assignment);
                result.IsSuccessful = true;
                result.ResultData.Add(created);
            }
            catch (ItemAlreadyExistsException e)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add(e.Message);
                return Conflict(result);
            }
            catch (Exception e)
            {
                throw e;
            }
            return Ok(result);
        }
        [HttpPost("CreateAssignments")]
        public async Task<ActionResult<StoreProductCategoryResult>> CreateAssignments(List<StoreProductCategory> assignments)
        {
            StoreProductCategoryResult result = new StoreProductCategoryResult();
            foreach (var assignment in assignments)
            {
                assignment.CreatedAt = DateTime.Now;
                try
                {
                    var created = await _storeProductCatRepository.CreateAsync(assignment);
                    result.IsSuccessful = true;
                    result.ResultData.Add(created);
                }
                catch (ItemAlreadyExistsException e)
                {
                    result.IsSuccessful = false;
                    result.ResultData = null;
                    result.ErrorMessages.Add(e.Message);
                    return Conflict(result);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<StoreProductCategoryResult>> UpdateAssignment(string id, [FromBody] StoreProductCategory assignment)
        {
            StoreProductCategoryResult result = new StoreProductCategoryResult();
            if (id != assignment.StoreProductCategoryId)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add("Id does not match value in object");
                return BadRequest(result);
            }
            try
            {
                var update = await _storeProductCatRepository.UpdateAsync(id, assignment);
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
                return NotFound(result);
            }
            catch (Exception e)
            {
                throw e;
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<StoreProductCategoryResult>> DeleteAssignment(string id)
        {
            StoreProductCategoryResult result = new StoreProductCategoryResult();
            try
            {
                result.IsSuccessful = await _storeProductCatRepository.DeleteByIdAsync(id);
            }
            catch (ItemNotFoundException e)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add(e.Message);
                return NotFound(result);
            }
            catch (Exception e)
            {
                throw e;
            }
            return Ok(result);
        }
        [HttpDelete("AllOfStore/{id}")]
        public async Task<ActionResult<StoreProductCategoryResult>> DeleteAllOfStore(string id)
        {
            StoreProductCategoryResult result = new StoreProductCategoryResult();
            try
            {
                result.IsSuccessful = await _storeProductCatRepository.DeleteAllOfStore(id);
            }
            catch (ItemNotFoundException e)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add(e.Message);
                return NotFound(result);
            }
            catch (Exception e)
            {
                throw e;
            }
            return Ok(result);
        }
    }
}
