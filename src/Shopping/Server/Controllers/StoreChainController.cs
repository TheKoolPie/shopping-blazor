using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Linq;
using Shopping.Shared.Data;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Model.Account;
using Shopping.Shared.Results.Entities;
using Shopping.Shared.Services.Interfaces;
using Shopping.Shared.Services.Interfaces.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StoreChainController : ControllerBase
    {
        private readonly IStoreChainRepository _storeChainRepository;
        private readonly ICurrentUserProvider _currentUserProvider;

        public StoreChainController(IStoreChainRepository storeChainRepository, ICurrentUserProvider currentUserProvider)
        {
            _storeChainRepository = storeChainRepository;
            _currentUserProvider = currentUserProvider;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<StoreChainResult>> GetStoreChains()
        {
            List<StoreChain> chains = await _storeChainRepository.GetAllAsync();
            var result = new StoreChainResult
            {
                IsSuccessful = true,
                ResultData = chains
            };
            return result;
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<StoreChainResult>> GetStoreChain(string id)
        {
            StoreChain chain = null;
            StoreChainResult result = new StoreChainResult();
            try
            {
                chain = await _storeChainRepository.GetAsync(id);
                result.IsSuccessful = true;
                result.ResultData.Add(chain);
            }
            catch (ItemNotFoundException e)
            {
                result.IsSuccessful = false;
                result.ResultData = null;
                result.ErrorMessages.Add(e.Message);
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<StoreChainResult>> CreateStoreChain(StoreChain chain)
        {
            var user = await _currentUserProvider.GetUserAsync();
            chain.CreatedAt = DateTime.Now;
            chain.CreatorId = user.Id;

            StoreChainResult result = new StoreChainResult();
            try
            {
                StoreChain created = await _storeChainRepository.CreateAsync(chain);
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
        public async Task<ActionResult<StoreChainResult>> UpdateStoreChain(string id, [FromBody] StoreChain chain)
        {
            StoreChainResult result = new StoreChainResult();
            if (id != chain.StoreChainId)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add("Id does not match with object id");
                return BadRequest(result);
            }
            try
            {
                var update = await _storeChainRepository.UpdateAsync(id, chain);
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
        public async Task<ActionResult<StoreChainResult>> DeleteUserGroup(string id)
        {
            StoreChainResult result = new StoreChainResult();
            try
            {
                result.IsSuccessful = await _storeChainRepository.DeleteByIdAsync(id);
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
