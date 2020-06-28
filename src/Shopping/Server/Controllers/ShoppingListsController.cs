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
        private readonly IUserGroupShoppingLists _userGroupShoppingLists;
        private readonly ILogger<ShoppingListsController> _logger;

        public ShoppingListsController(IShoppingLists lists, ICurrentUserProvider users,
            ILogger<ShoppingListsController> logger, IUserGroupShoppingLists userGroupShoppingLists)
        {
            _lists = lists;
            _users = users;
            _userGroupShoppingLists = userGroupShoppingLists;
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
            var user = await _users.GetUserAsync();
            try
            {
                list = await _lists.GetAsync(id);

                bool isAdmin = await _users.IsUserAdminAsync();
                bool isInList = await _lists.IsOfUserAsync(list, user.Id);

                if (!(isInList || isAdmin))
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
            shoppingList.Owner = new ShoppingUserModel()
            {
                Id = user.Id
            };
            ShoppingList item;
            try
            {
                item = await _lists.CreateAsync(shoppingList);
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
        [HttpPost("AddItem/{id}")]
        public async Task<ActionResult<ShoppingListItem>> AddItemToList(string id, [FromBody] ShoppingListItem item)
        {
            var user = await _users.GetUserAsync();
            ShoppingList list = null;
            try
            {
                list = await _lists.GetAsync(id);
            }
            catch (ItemNotFoundException e)
            {
                return NotFound(e.Message);
            }
            if (!(await _lists.IsOfUserAsync(list, user.Id)))
            {
                return Unauthorized();
            }
            ShoppingListItem createdItem = null;
            try
            {
                createdItem = await _lists.AddOrUpdateItemAsync(list.Id, item);
            }
            catch (ItemNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (PersistencyException e)
            {
                return Conflict(e.Message);
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

            ShoppingList updatedList = null;
            try
            {
                updatedList = await _lists.UpdateAsync(id, list);
            }
            catch (ItemNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (PersistencyException e)
            {
                return Conflict(e.Message);
            }

            return Ok(updatedList);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteList(string id)
        {
            try
            {
                var list = await _lists.GetAsync(id);
                var user = await _users.GetUserAsync();

                bool isAdmin = await _users.IsUserAdminAsync();
                bool isOwner = await _lists.IsOfUserAsync(list, user.Id);

                if (!(isAdmin || isOwner))
                {
                    return Unauthorized(false);
                }

                await _lists.DeleteByIdAsync(id);
            }
            catch (ItemNotFoundException)
            {
                return NotFound(false);
            }
            return Ok(true);
        }
    }
}
