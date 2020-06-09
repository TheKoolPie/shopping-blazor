using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Server.Services;
using Shopping.Shared.Data;
using Shopping.Shared.Exceptions;
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

        [HttpPost]
        public async Task<ActionResult<bool>> CreateAssignment([FromBody] UserGroupShoppingList assignment)
        {
            bool result = false;
            try
            {
                result = await _userGroupShoppingLists.CreateAssignmentAsync(assignment);
            }
            catch (ItemAlreadyExistsException e)
            {
                return Conflict(e.Message);
            }
            return result;
        }
        [HttpDelete("{groupId}/{listId}")]
        public async Task<ActionResult<bool>> DeleteAssignment(string groupId, string listId)
        {
            var assignment = new UserGroupShoppingList()
            {
                UserGroupId = groupId,
                ShoppingListId = listId
            };
            bool result = false;
            try
            {
                result = await _userGroupShoppingLists.RemoveAssignmentAsync(assignment);
            }
            catch(ItemNotFoundException e)
            {
                return NotFound(e.Message);
            }
            return result;
        }
    }
}
