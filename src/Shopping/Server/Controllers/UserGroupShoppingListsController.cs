using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Server.Services;
using Shopping.Shared.Data;
using Shopping.Shared.Services.Interfaces;

namespace Shopping.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserGroupShoppingListsController : ControllerBase
    {
        private readonly IUserGroupShoppingLists _userGroupShoppingLists;
        private readonly IUserProvider _users;
        private readonly IShoppingLists _shoppingLists;
        private readonly IUserGroups _userGroups;
        public UserGroupShoppingListsController(IUserGroupShoppingLists userGroupShoppingLists, IUserProvider users,
            IShoppingLists shoppingLists, IUserGroups userGroups)
        {
            _userGroupShoppingLists = userGroupShoppingLists;
            _users = users;
            _shoppingLists = shoppingLists;
            _userGroups = userGroups;
        }

        [HttpGet("ShoppingListsOfGroup/{id}")]
        public async Task<ActionResult<List<ShoppingList>>> GetShoppingListsOfUserGroup(string id)
        {
            var user = await _users.GetUserAsync();

            if (!(await _userGroups.UserIsInGroupAsync(id, user.Id)))
            {
                return Unauthorized();
            }

            var lists = await _userGroupShoppingLists.GetShoppingListsOfUserGroupAsync(id);
            return Ok(lists);
        }
        [HttpGet("UserGroupsOfShoppingList/{id}")]
        public async Task<ActionResult<List<UserGroup>>> GetUserGroupsOfShoppingList(string id)
        {
            var user = await _users.GetUserAsync();

            if (!(await _shoppingLists.IsOfUserAsync(id, user.Id)))
            {
                return Unauthorized();
            }

            var groups = await _userGroupShoppingLists.GetUserGroupsOfShoppingListAsync(id);
            return Ok(groups);
        }
    }
}
