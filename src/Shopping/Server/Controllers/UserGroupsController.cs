﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Server.Models;
using Shopping.Server.Services;
using Shopping.Shared.Data;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Model.Account;
using Shopping.Shared.Model.Results;
using Shopping.Shared.Services.Interfaces;

namespace Shopping.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserGroupsController : ControllerBase
    {
        private readonly IUserGroups _userGroups;
        private readonly IUserProvider _userProvider;
        public UserGroupsController(IUserGroups userGroups, IUserProvider users)
        {
            _userGroups = userGroups;
            _userProvider = users;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserGroup>>> GetUserGroups()
        {
            List<UserGroup> groups = new List<UserGroup>();
            var user = await _userProvider.GetUserAsync();
            if (await _userProvider.IsUserAdminAsync())
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
            var user = await _userProvider.GetUserAsync();
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
        [HttpGet("GetUsersInGroup/{id}")]
        public async Task<ActionResult<ShoppingUserResult>> GetUsersInGroup(string id)
        {
            ShoppingUserResult result = new ShoppingUserResult();
            var user = await _userProvider.GetUserAsync();
            try
            {
                bool isInGroup = await _userGroups.UserIsInGroupAsync(id, user.Id);
                bool isAdmin = await _userProvider.IsUserAdminAsync();

                if (!isAdmin && !isInGroup)
                {
                    return Unauthorized();
                }

                var usersInGroup = await _userGroups.GetUsersInGroup(id);
                result.IsSuccessful = true;
                result.ResultData = usersInGroup;
            }
            catch (ItemNotFoundException e)
            {
                result.IsSuccessful = false;
                result.Message = e.Message;
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<UserGroup>> CreateUserGroup(UserGroup group)
        {
            var user = await _userProvider.GetUserAsync();
            group.Owner = new ShoppingUserModel()
            {
                Id = user.Id,
            };

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

        [HttpPut("AddUser/{id}")]
        public async Task<ActionResult<UserGroupResult>> AddUserToGroup(string id, [FromBody] ShoppingUserModel user)
        {
            var result = new UserGroupResult();

            var currentUser = await _userProvider.GetUserAsync();
            if (!(await _userGroups.UserIsInGroupAsync(id, currentUser.Id)))
            {
                return Unauthorized();
            }
            try
            {
                var group = await _userGroups.AddUserToGroup(id, user);
                result.IsSuccessful = true;
                result.ResultData.Add(group);
            }
            catch (Exception e)
            {
                result.IsSuccessful = false;
                result.Message = e.Message;
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteUserGroup(string id)
        {
            var user = await _userProvider.GetUserAsync();
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
