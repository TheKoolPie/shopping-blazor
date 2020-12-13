using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using Shopping.Shared.Data;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Model.Account;
using Shopping.Shared.Results;
using Shopping.Shared.Services.Interfaces;

namespace Shopping.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserGroupsController : ControllerBase
    {
        private readonly IUserGroupRepository _userGroups;
        private readonly ICurrentUserProvider _userProvider;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserGroupsController> _logger;
        public UserGroupsController(IUserGroupRepository userGroups, ICurrentUserProvider users,
            IUserRepository userRepository, ILogger<UserGroupsController> logger)
        {
            _userGroups = userGroups;
            _userProvider = users;
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<UserGroupResult>> GetUserGroups()
        {
            UserGroupResult result = new UserGroupResult();

            List<UserGroup> groups = new List<UserGroup>();
            var user = await _userProvider.GetUserAsync();

            bool isAdmin = await _userProvider.IsUserAdminAsync();

            if (isAdmin)
            {
                groups = await _userGroups.GetAllAsync();
            }
            else
            {
                groups = await _userGroups.GetAllOfUserAsync(user.Id);
            }

            foreach (var group in groups)
            {
                group.Owner = await _userRepository.GetUserByIdAsync(group.OwnerId);
            }

            result.IsSuccessful = true;
            result.ResultData = groups;

            return Ok(result);
        }
        [HttpGet("GetAllOfUser/{id}")]
        public async Task<ActionResult<UserGroupResult>> GetAllOfUser(string id)
        {
            UserGroupResult result = new UserGroupResult();
            if (string.IsNullOrEmpty(id))
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add("No user id provided");
                return BadRequest(result);
            }
            var user = await _userProvider.GetUserAsync();
            bool isAdmin = await _userProvider.IsUserAdminAsync();
            bool isCurrentUser = id == user.Id;

            if (!(isAdmin || isCurrentUser))
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add("Not authorized to access");
                return Unauthorized(result);
            }
            result.IsSuccessful = true;
            result.ResultData = await _userGroups.GetAllOfUserAsync(id);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserGroupResult>> GetUserGroup(string id)
        {
            UserGroupResult result = new UserGroupResult();

            var user = await _userProvider.GetUserAsync();
            try
            {
                bool isAdmin = await _userProvider.IsUserAdminAsync();
                bool isUserInGroup = await _userGroups.UserIsInGroupAsync(id, user.Id);
                if (!(isAdmin || isUserInGroup))
                {
                    result.IsSuccessful = false;
                    result.ErrorMessages.Add("User requesting this resource is not allowed to access");
                    return Unauthorized(result);
                }

                var group = await _userGroups.GetAsync(id);
                group.Owner = await _userRepository.GetUserByIdAsync(group.OwnerId);
                foreach (var member in group.Members)
                {
                    var dbUser = await _userRepository.GetUserByIdAsync(member.Id);
                    member.UserName = dbUser.UserName;
                    member.Email = dbUser.Email;
                }

                result.ResultData.Add(group);
                result.IsSuccessful = true;
            }
            catch (ItemNotFoundException e)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add(e.Message);
                return NotFound(result);
            }

            return Ok(result);
        }
        [HttpGet("GetUsersInGroup/{id}")]
        public async Task<ActionResult<ShoppingUserResult>> GetUsersInGroup(string id)
        {
            ShoppingUserResult result = new ShoppingUserResult();
            var user = await _userProvider.GetUserAsync();
            try
            {
                bool isUserInGroup = await _userGroups.UserIsInGroupAsync(id, user.Id);
                bool isAdmin = await _userProvider.IsUserAdminAsync();

                if (!(isAdmin || isUserInGroup))
                {
                    result.IsSuccessful = false;
                    result.ErrorMessages.Add("Not authorized");
                    return Unauthorized(result);
                }

                var usersInGroup = await _userGroups.GetUsersInGroup(id);
                result.IsSuccessful = true;
                result.ResultData = usersInGroup;
            }
            catch (ItemNotFoundException e)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add(e.Message);
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<UserGroupResult>> CreateUserGroup(UserGroup group)
        {
            UserGroupResult result = new UserGroupResult();

            var user = await _userProvider.GetUserAsync();

            group.OwnerId = user.Id;
            try
            {
                var createdGroup = await _userGroups.CreateAsync(group);
                result.IsSuccessful = true;
                result.ResultData.Add(createdGroup);
            }
            catch (ItemAlreadyExistsException e)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add("Item already exists");
                result.ErrorMessages.Add(e.Message);
                return Conflict(result);
            }
            catch (Exception e)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add("Internal server error");
                result.ErrorMessages.Add(e.Message);
                throw e;
            }

            return Ok(result);
        }

        [HttpPut("AddUser/{id}")]
        public async Task<ActionResult<UserGroupResult>> AddUserToGroup(string id, [FromBody] ShoppingUserModel user)
        {
            var result = new UserGroupResult();

            var currentUser = await _userProvider.GetUserAsync();

            bool isUserInGroup = await _userGroups.UserIsInGroupAsync(id, currentUser.Id);
            bool isAdmin = await _userProvider.IsUserAdminAsync();

            if (!(isAdmin || isUserInGroup))
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add("Not authorized");
                return Unauthorized(result);
            }
            try
            {
                var group = await _userGroups.AddUserToGroup(id, user);
                group.Owner = await _userRepository.GetUserByIdAsync(group.OwnerId);
                foreach (var member in group.Members)
                {
                    var dbUser = await _userRepository.GetUserByIdAsync(member.Id);
                    member.UserName = dbUser.UserName;
                    member.Email = dbUser.Email;
                }

                result.IsSuccessful = true;
                result.ResultData.Add(group);
            }
            catch (Exception e)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add(e.Message);
                throw e;
            }
            return Ok(result);
        }
        [HttpPut("RemoveUser/{id}")]
        public async Task<ActionResult<UserGroupResult>> RemoveUserFromGroup(string id, [FromBody] ShoppingUserModel user)
        {
            var result = new UserGroupResult();
            var currentUser = await _userProvider.GetUserAsync();

            bool isUserInGroup = await _userGroups.UserIsInGroupAsync(id, currentUser.Id);
            bool isAdmin = await _userProvider.IsUserAdminAsync();

            if (!(isAdmin || isUserInGroup))
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add("Not authorized");
                return Unauthorized(result);
            }
            try
            {
                var group = await _userGroups.RemoveUserFromGroup(id, user);
                group.Owner = await _userRepository.GetUserByIdAsync(group.OwnerId);
                foreach (var member in group.Members)
                {
                    var dbUser = await _userRepository.GetUserByIdAsync(member.Id);
                    member.UserName = dbUser.UserName;
                    member.Email = dbUser.Email;
                }

                result.IsSuccessful = true;
                result.ResultData.Add(group);
            }
            catch (Exception e)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add(e.Message);
                throw e;
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserGroupResult>> DeleteUserGroup(string id)
        {
            var result = new UserGroupResult();
            var currentUser = await _userProvider.GetUserAsync();

            bool isUserInGroup = await _userGroups.UserIsInGroupAsync(id, currentUser.Id);
            bool isAdmin = await _userProvider.IsUserAdminAsync();

            if (!(isAdmin || isUserInGroup))
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add("Not authorized");
                return Unauthorized(result);
            }

            try
            {
                var group = await _userGroups.GetAsync(id);
                if (!(await _userGroups.DeleteByIdAsync(id)))
                {
                    result.IsSuccessful = false;
                    result.ErrorMessages.Add($"Could not delete item '{id}'");
                    return UnprocessableEntity(result);
                }
                if (!(await _userRepository.RemoveStandardUserGroupId(id)))
                {
                    _logger.LogWarning($"Could not remove '{id}' from at least one user");
                }
            }
            catch (ItemNotFoundException e)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add(e.Message);
                return NotFound(result);
            }

            result.IsSuccessful = true;

            return Ok(result);
        }
    }
}
