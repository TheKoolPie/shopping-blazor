using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.Shared.Data;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Results.Entities;
using Shopping.Shared.Services.Interfaces;
using Shopping.Shared.Services.Interfaces.Repos;
using System;
using System.Threading.Tasks;

namespace Shopping.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StoreController : ControllerBase
    {
        private readonly IStoreRepository _storeRepository;
        private readonly ICurrentUserProvider _currentUserProvider;

        public StoreController(IStoreRepository storeRepository, ICurrentUserProvider currentUserProvider)
        {
            _storeRepository = storeRepository;
            _currentUserProvider = currentUserProvider;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<StoreResult>> GetStores()
        {
            var stores = await _storeRepository.GetAllAsync();
            var result = new StoreResult
            {
                IsSuccessful = true,
                ResultData = stores
            };
            return Ok(result);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<StoreResult>> GetStore(string id)
        {
            StoreResult result = new StoreResult();
            try
            {
                var store = await _storeRepository.GetAsync(id);
                result.IsSuccessful = true;
                result.ResultData.Add(store);
            }
            catch (ItemNotFoundException e)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add(e.Message);
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<StoreResult>> CreateStore(Store store)
        {
            var user = await _currentUserProvider.GetUserAsync();
            store.CreatedAt = DateTime.Now;
            store.CreatorId = user.Id;

            StoreResult result = new StoreResult();
            try
            {
                var created = await _storeRepository.CreateAsync(store);
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
        [HttpPut("{id}")]
        public async Task<ActionResult<StoreResult>> UpdateStore(string id, [FromBody] Store store)
        {
            StoreResult result = new StoreResult();
            if (id != store.StoreId)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add("Id does not mathc with object id");
                return BadRequest(result);
            }
            try
            {
                var update = await _storeRepository.UpdateAsync(id, store);
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
                throw e;
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<StoreResult>> DeleteResult(string id)
        {
            StoreResult result = new StoreResult();
            try
            {
                result.IsSuccessful = await _storeRepository.DeleteByIdAsync(id);
            }
            catch (ItemNotFoundException e)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add(e.Message);
            }
            catch (Exception e)
            {
                throw e;
            }
            return Ok(result);
        }

    }
}
