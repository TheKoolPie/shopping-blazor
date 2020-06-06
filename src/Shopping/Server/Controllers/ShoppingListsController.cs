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
using Shopping.Shared.Services.Interfaces;

namespace Shopping.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ShoppingListsController : ControllerBase
    {
        private readonly IShoppingLists _lists;
        private readonly IUserProvider _users;
        private readonly ILogger<ShoppingListsController> _logger;

        public ShoppingListsController(IShoppingLists lists, IUserProvider users, ILogger<ShoppingListsController> logger)
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
            var user = await _users.GetUserAsync();
            try
            {
                list = await _lists.GetAsync(id);
                if (!(await _lists.CheckIfListIsFromUser(list, user.Id)))
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
            catch (Exception)
            {
                throw;
            }
            return Ok(item);
        }
        [HttpPost("AddItem/{id}")]
        public async Task<ActionResult<ShoppingList>> AddItemToList(string id, [FromBody] ShoppingListItem item)
        {
            var user = await _users.GetUserAsync();
            var list = await _lists.GetAsync(id);
            if (!(await _lists.CheckIfListIsFromUser(list, user.Id)))
            {
                return Unauthorized();
            }

            var createdItem = await _lists.AddOrUpdateItemAsync(list.Id, item);

            return Ok(createdItem);
        }
        [HttpPost("AddUserGroup/{id}")]
        public async Task<ActionResult<UserGroup>> AddGroupToList(string id, [FromBody] string userGroupId)
        {
            var user = await _users.GetUserAsync();
            var list = await _lists.GetAsync(id);
            if (!(await _lists.CheckIfListIsFromUser(list, user.Id)))
            {
                return Unauthorized();
            }

            var addedGroup = await _lists.AddUserGroupAsync(list.Id, userGroupId);

            return Ok(addedGroup);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<bool>> DeleteList(string id)
        {
            try
            {
                var list = await _lists.GetAsync(id);
                var user = await _users.GetUserAsync();

                bool isAdmin = await _users.IsUserAdminAsync();
                bool isOwner = await _lists.CheckIfListIsFromUser(list, user.Id);

                if (isAdmin || isOwner)
                {
                    await _lists.DeleteByIdAsync(id);
                }
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }
            return Ok(true);
        }
    }
}
