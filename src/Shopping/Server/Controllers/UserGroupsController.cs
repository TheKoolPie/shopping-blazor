using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Server.Services;
using Shopping.Shared.Data;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Model.Account;
using Shopping.Shared.Services.Interfaces;

namespace Shopping.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserGroupsController : ControllerBase
    {
        private readonly IUserGroups _userGroups;
        private readonly IUserProvider _users;
        public UserGroupsController(IUserGroups userGroups, IUserProvider users)
        {
            _userGroups = userGroups;
            _users = users;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserGroup>>> GetUserGroups()
        {
            List<UserGroup> groups = new List<UserGroup>();
            var user = await _users.GetUserAsync();
            if (await _users.IsUserAdminAsync())
            {
                groups = await _userGroups.GetAllAsync();
            }
            else
            {
                groups = await _userGroups.GetAllOfUserAsync(user.Id);
            }
            return Ok(groups);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserGroup>> GetUserGroup(string id)
        {
            UserGroup group = null;
            var user = await _users.GetUserAsync();
            try
            {
                group = await _userGroups.GetAsync(id);
                if (!(await _userGroups.UserIsInGroupAsync(id, user.Id)))
                {
                    return Unauthorized();
                }
            }
            catch (ItemNotFoundException e)
            {
                return NotFound(e.Message);
            }
            return Ok(group);
        }

        [HttpPost]
        public async Task<ActionResult<UserGroup>> CreateUserGroup(UserGroup group)
        {
            var user = await _users.GetUserAsync();
            group.OwnerId = user.Id;

            UserGroup created = null;
            try
            {
                created = await _userGroups.CreateAsync(group);
            }
            catch (ItemAlreadyExistsException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                throw e;
            }

            return Ok(created);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteUserGroup(string id)
        {
            var user = await _users.GetUserAsync();
            try
            {
                var group = await _userGroups.GetAsync(id);
                if (!(await _userGroups.UserIsInGroupAsync(id, user.Id)))
                {
                    return Unauthorized();
                }
                await _userGroups.DeleteByIdAsync(id);
            }
            catch (ItemNotFoundException e)
            {
                return NotFound(e.Message);
            }
            return Ok(true);
        }
    }
}
