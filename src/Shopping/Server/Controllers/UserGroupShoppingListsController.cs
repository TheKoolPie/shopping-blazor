using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shopping.Server.Services;
using Shopping.Shared.Data;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Results;
using Shopping.Shared.Services.Interfaces;

namespace Shopping.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserGroupShoppingListsController : ControllerBase
    {
        private readonly IUserGroupShoppingLists _userGroupShoppingLists;
        private readonly ICurrentUserProvider _users;
        private readonly IShoppingLists _shoppingLists;
        private readonly IUserGroupRepository _userGroups;
        public UserGroupShoppingListsController(IUserGroupShoppingLists userGroupShoppingLists, ICurrentUserProvider users,
            IShoppingLists shoppingLists, IUserGroupRepository userGroups)
        {
            _userGroupShoppingLists = userGroupShoppingLists;
            _users = users;
            _shoppingLists = shoppingLists;
            _userGroups = userGroups;
        }

        [HttpGet("ShoppingListsOfGroup/{id}")]
        public async Task<ActionResult<ShoppingListResult>> GetShoppingListsOfUserGroup(string id)
        {
            var result = new ShoppingListResult();

            var user = await _users.GetUserAsync();

            bool isAdmin = await _users.IsUserAdminAsync();
            bool isInGroup = await _userGroups.UserIsInGroupAsync(id, user.Id);

            if (!(isInGroup || isAdmin))
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add("Not authorized");
                return Unauthorized(result);
            }

            result.IsSuccessful = true;
            result.ResultData = await _userGroupShoppingLists.GetShoppingListsOfUserGroupAsync(id);

            return Ok(result);
        }
        [HttpGet("UserGroupsOfShoppingList/{id}")]
        public async Task<ActionResult<UserGroupResult>> GetUserGroupsOfShoppingList(string id)
        {
            var result = new UserGroupResult();

            var user = await _users.GetUserAsync();

            bool isAdmin = await _users.IsUserAdminAsync();
            bool isInList = await _shoppingLists.IsOfUserAsync(id, user.Id);

            if (!(isInList || isAdmin))
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add("Not authorized");
                return Unauthorized(result);
            }
            result.IsSuccessful = true;
            result.ResultData = await _userGroupShoppingLists.GetUserGroupsOfShoppingListAsync(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<UserGroupShoppingListResult>> CreateAssignment([FromBody] UserGroupShoppingList assignment)
        {
            var result = new UserGroupShoppingListResult();
            try
            {
                result.IsSuccessful = true;
                var created = await _userGroupShoppingLists.CreateAsync(assignment);
                result.ResultData.Add(created);
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
        [HttpDelete("{groupId}/{listId}")]
        public async Task<ActionResult<UserGroupShoppingListResult>> DeleteAssignment(string groupId, string listId)
        {
            var result = new UserGroupShoppingListResult();
            var assignment = new UserGroupShoppingList()
            {
                UserGroupId = groupId,
                ShoppingListId = listId
            };
            try
            {
                bool deleteResult = await _userGroupShoppingLists.DeleteAsync(assignment);
                if (!deleteResult)
                {
                    result.IsSuccessful = false;
                    result.ErrorMessages.Add($"Could not delete assignment Group:'{groupId}'<-->List:'{listId}'");
                    return UnprocessableEntity(result);
                }
                result.IsSuccessful = true;
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
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
