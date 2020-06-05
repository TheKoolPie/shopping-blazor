using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            var lists = await _lists.GetAllOfUserAsync(user.Id);

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
        [HttpGet("GetAll")]
        [Authorize(Policy = ShoppingUserPolicies.IsAdmin)]
        public async Task<ActionResult<List<ShoppingList>>> GetAllLists()
        {
            var lists = await _lists.GetAllAsync();
            return Ok(lists);
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingList>> PostList(ShoppingList shoppingList)
        {
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

        [HttpPut]
        public async Task<ActionResult<ShoppingList>> PutList(string id, ShoppingList shoppingList)
        {
            if (id != shoppingList.Id)
            {
                return BadRequest();
            }

            ShoppingList update;
            try
            {
                update = await _lists.UpdateAsync(id, shoppingList);
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(update);
        }
    }
}
