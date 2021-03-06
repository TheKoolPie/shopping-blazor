﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Shopping.Server.Extensions;
using Shopping.Server.Models;
using Shopping.Shared.Data;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Model.Account;
using Shopping.Shared.Results.Account;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Server.Services.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ShoppingUser> _userManager;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(UserManager<ShoppingUser> userManager, ILogger<UserRepository> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<List<string>> GetRolesOfUserAsync(ShoppingUserModel userModel)
        {
            List<string> roles = null;

            var dbUser = await GetDbUserFromModelData(userModel);

            if (dbUser != null)
            {
                roles = (await _userManager.GetRolesAsync(dbUser)).ToList();
            }

            return roles;
        }

        public async Task<ShoppingUserModel> GetUserAsync(ShoppingUserModel userModel)
        {
            var dbUser = await GetDbUserFromModelData(userModel);

            ShoppingUserModel modelFromDbData = null;
            if (dbUser != null)
            {
                modelFromDbData = new ShoppingUserModel()
                {
                    Id = dbUser.Id,
                    UserName = dbUser.UserName,
                    Email = dbUser.Email,
                    Settings = new ShoppingUserSettingsModel()
                    {
                        FirstName = dbUser.FirstName,
                        LastName = dbUser.LastName,
                        StandardUserGroupId = dbUser.StandardUserGroupId
                    }
                };
            }

            return modelFromDbData;
        }

        public async Task<ShoppingUserModel> GetUserByEmailAsync(string email)
        {
            ShoppingUserModel userModel = null;
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                userModel = new ShoppingUserModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Settings = new ShoppingUserSettingsModel()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        StandardUserGroupId = user.StandardUserGroupId
                    }
                };
            }
            else
            {
                _logger.LogError($"Could not find any user with email: {email}");
            }
            return userModel;
        }

        public async Task<ShoppingUserModel> GetUserByIdAsync(string id)
        {
            ShoppingUserModel userModel = null;
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                userModel = new ShoppingUserModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Settings = new ShoppingUserSettingsModel()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        StandardUserGroupId = user.StandardUserGroupId
                    }
                };
            }
            else
            {
                _logger.LogError($"Could not find any user by id: {id}");
            }
            return userModel;
        }

        public async Task<ShoppingUserModel> GetUserByNameAsync(string userName)
        {
            ShoppingUserModel userModel = null;
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                userModel = new ShoppingUserModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Settings = new ShoppingUserSettingsModel()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        StandardUserGroupId = user.StandardUserGroupId
                    }
                };
            }
            else
            {
                _logger.LogError($"Could not find any user with name: {userName}");
            }
            return userModel;
        }

        public async Task<ShoppingUserModel> UpdateUserData(string id, ShoppingUserModel updateData)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.Email = updateData.Email;
            user.UserName = updateData.UserName;
            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                _logger.LogError("Could not update user", updateResult.GetErrorMessagesAsList());
                return null;
            }

            return updateData;
        }

        public async Task<ShoppingUserSettingsModel> UpdateUserSettingsAsync(string userId, ShoppingUserSettingsModel settingsData)
        {
            var dbUser = await _userManager.FindByIdAsync(userId);
            ShoppingUserSettingsModel result = null;
            if (dbUser != null)
            {
                dbUser.FirstName = settingsData.FirstName?.Trim() ?? null;
                dbUser.LastName = settingsData.LastName?.Trim() ?? null;
                dbUser.StandardUserGroupId = settingsData.StandardUserGroupId?.Trim() ?? null;
                var updateResult = await _userManager.UpdateAsync(dbUser);
                if (!updateResult.Succeeded)
                {
                    _logger.LogError($"Could not update user settings for user '{userId}'", updateResult.GetErrorMessagesAsList());
                }
                else
                {
                    result = settingsData;
                }
            }
            else
            {
                _logger.LogError($"Could not find any user with id '{userId}'");
            }
            return result;
        }
        public async Task<bool> RemoveStandardUserGroupId(string userGroupId)
        {
            var usersWithGroup = _userManager.Users
                .Where(u => u.StandardUserGroupId == userGroupId)
                .ToList();
            foreach (var user in usersWithGroup)
            {
                user.StandardUserGroupId = "";
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return false;
                }
            }
            return true;
        }
        private async Task<ShoppingUser> GetDbUserFromModelData(ShoppingUserModel userModel)
        {
            ShoppingUser dbUser = null;
            if (!string.IsNullOrEmpty(userModel.Id))
            {
                dbUser = await _userManager.FindByIdAsync(userModel.Id);
            }
            else if (!string.IsNullOrEmpty(userModel.UserName))
            {
                dbUser = await _userManager.FindByNameAsync(userModel.UserName);
            }
            else if (!string.IsNullOrEmpty(userModel.Email))
            {
                dbUser = await _userManager.FindByEmailAsync(userModel.Email);
            }
            else
            {
                _logger.LogError($"No valid data provided");
            }
            return dbUser;
        }


    }
}
