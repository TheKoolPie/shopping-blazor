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
        public async Task<ActionResult<List<ShoppingList>>> GetLists()
        {
            var user = await _users.GetUserAsync();

            List<ShoppingList> lists = new List<ShoppingList>();

            if (await _users.IsUserAdminAsync())
            {
                lists = await _lists.GetAllAsync();
            }
            else
            {
                lists = await _lists.GetAllOfUserAsync(user.Id);
            }
            return Ok(lists);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingList>> GetList(string id)
        {
            ShoppingList list;
            try
            {
                list = await _lists.GetAsync(id);
                if (!(await IsUserAuthorizedToAccessList(list.Id)))
                {
                    return Unauthorized();
                }
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }

            return Ok(list);
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingList>> CreateList(ShoppingList shoppingList)
        {
            var user = await _users.GetUserAsync();
            shoppingList.OwnerId = user.Id;

            ShoppingList item;
            try
            {
                item = await _lists.CreateAsync(shoppingList);
            }
            catch (ItemAlreadyExistsException)
            {
                return Conflict();
            }
            catch (PersistencyException)
            {
                return Conflict();
            }
            return Ok(item);
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
        public async Task<ActionResult<ShoppingList>> UpdateList(string id, ShoppingList list)
        {
            if (id != list.Id)
            {
                return BadRequest();
            }
            ShoppingList updatedList;
            try
            {
                if (!(await IsUserAuthorizedToAccessList(list.Id)))
                {
                    return Unauthorized();
                }
                updatedList = await _lists.UpdateAsync(id, list);
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }
            catch (PersistencyException)
            {
                return Conflict();
            }

            return Ok(updatedList);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteList(string id)
        {
            try
            {
                if (!(await IsUserAuthorizedToAccessList(id)))
                {
                    return Unauthorized();
                }

                await _lists.DeleteByIdAsync(id);
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }
            catch (PersistencyException)
            {
                return Conflict();
            }
            return Ok();
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
