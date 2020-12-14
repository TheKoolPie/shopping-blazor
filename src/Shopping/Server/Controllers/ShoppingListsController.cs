using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shopping.Server.Services;
using Shopping.Shared.Data;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Model.Account;
using Shopping.Shared.Results;
using Shopping.Shared.Services;
using Shopping.Shared.Services.Interfaces;

namespace Shopping.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ShoppingListsController : ControllerBase
    {
        private readonly IShoppingLists _lists;
        private readonly ICurrentUserProvider _users;
        private readonly ILogger<ShoppingListsController> _logger;

        public ShoppingListsController(IShoppingLists lists, ICurrentUserProvider users,
            ILogger<ShoppingListsController> logger)
        {
            _lists = lists;
            _users = users;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ShoppingListResult>> GetLists()
        {
            var user = await _users.GetUserAsync();

            var result = new ShoppingListResult();
            result.IsSuccessful = true;
            if (await _users.IsUserAdminAsync())
            {
                result.ResultData = await _lists.GetAllAsync();
            }
            else
            {
                result.ResultData = await _lists.GetAllOfUserAsync(user.Id);
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingListResult>> GetList(string id)
        {
            var result = new ShoppingListResult();
            try
            {
                if (!(await IsUserAuthorizedToAccessList(id)))
                {
                    result.IsSuccessful = true;
                    result.ErrorMessages.Add("Not authorized");
                    return Unauthorized(result);
                }

                var list = await _lists.GetAsync(id);
                result.IsSuccessful = true;
                result.ResultData.Add(list);
            }
            catch (ItemNotFoundException e)
            {
                result.IsSuccessful = true;
                result.ErrorMessages.Add(e.Message);
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingListResult>> CreateList(ShoppingList shoppingList)
        {
            var user = await _users.GetUserAsync();
            shoppingList.OwnerId = user.Id;

            var result = new ShoppingListResult();
            try
            {
                result.IsSuccessful = true;
                var item = await _lists.CreateAsync(shoppingList);
                result.IsSuccessful = true;
                result.ResultData.Add(item);
            }
            catch (ItemAlreadyExistsException e)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add(e.Message);
                return Conflict(result);
            }
            catch (PersistencyException e)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add(e.Message);
                return Conflict(result);
            }
            return Ok(result);
        }
        [HttpPost("AddItem/{id}")]
        public async Task<ActionResult<ShoppingListItem>> AddItemToList(string id, [FromBody] ShoppingListItem item)
        {
            ShoppingListItem createdItem;
            try
            {
                if (!(await IsUserAuthorizedToAccessList(id)))
                {
                    return Unauthorized();
                }
                createdItem = await _lists.AddOrUpdateItemAsync(id, item);
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }
            catch (PersistencyException)
            {
                return Conflict();
            }

            return Ok(createdItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ShoppingListResult>> UpdateList(string id, ShoppingList list)
        {
            var result = new ShoppingListResult();
            if (id != list.Id)
            {
                result.IsSuccessful = true;
                result.ErrorMessages.Add("Id does not match");
                return BadRequest(result);
            }
            try
            {
                if (!(await IsUserAuthorizedToAccessList(list.Id)))
                {
                    result.IsSuccessful = false;
                    result.ErrorMessages.Add("Not authorized");
                    return Unauthorized(result);
                }
                var updatedList = await _lists.UpdateAsync(id, list);
            }
            catch (ItemNotFoundException e)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add(e.Message);
                return NotFound(result);
            }
            catch (PersistencyException e)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add(e.Message);
                return Conflict(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ShoppingListResult>> DeleteList(string id)
        {
            var result = new ShoppingListResult();
            try
            {
                if (!(await IsUserAuthorizedToAccessList(id)))
                {
                    result.IsSuccessful = false;
                    result.ErrorMessages.Add("Not authorized");
                    return Unauthorized(result);
                }

                var deleteResult = await _lists.DeleteByIdAsync(id);
                if (!deleteResult)
                {
                    result.IsSuccessful = false;
                    result.ErrorMessages.Add($"Could not delete list '{id}'");
                    return UnprocessableEntity(result);
                }
            }
            catch (ItemNotFoundException e)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add(e.Message);
                return NotFound(result);
            }
            catch (PersistencyException e)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add(e.Message);
                return Conflict(result);
            }
            return Ok(result);
        }


        private async Task<bool> IsUserAuthorizedToAccessList(string listId)
        {
            var currentUser = await _users.GetUserAsync();

            var isAdmin = await _users.IsUserAdminAsync();
            var listIsOfUser = await _lists.IsOfUserAsync(listId, currentUser.Id);

            return isAdmin || listIsOfUser;
        }
    }
}
