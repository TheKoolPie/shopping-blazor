using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Shopping.Server.Models;
using Shopping.Shared.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Server.Services
{
    public class UserFromHttpContextProvider : IUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ShoppingUser> _userManager;
        private readonly ILogger<UserFromHttpContextProvider> _logger;

        public UserFromHttpContextProvider(IHttpContextAccessor httpContextAccessor,
            UserManager<ShoppingUser> userManager, ILogger<UserFromHttpContextProvider> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _logger = logger;
        }
        public async Task<ShoppingUser> GetUserAsync()
        {
            ShoppingUser user = null;
            var name = _httpContextAccessor.HttpContext.User.Identity.Name;
            if (!string.IsNullOrEmpty(name))
            {
                _logger.LogDebug($"Searching for user with user name or email: {name}");
                user = await _userManager.FindByNameAsync(name);
                if (user == null)
                {
                    user = await _userManager.FindByEmailAsync(name);
                }
            }
            return user;
        }

        public async Task<List<string>> GetUserRolesAsync()
        {
            var user = await GetUserAsync();
            return (await _userManager.GetRolesAsync(user)).ToList();
        }

        public async Task<List<ShoppingUser>> GetUsersInRoleAsync(string role)
        {
            return (await _userManager.GetUsersInRoleAsync(role)).ToList();
        }

        public async Task<bool> IsUserAdminAsync()
        {
            var user = await GetUserAsync();
            return await _userManager.IsInRoleAsync(user, ShoppingUserRoles.Admin);
        }
        public async Task<bool> IsUserAdminAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.IsInRoleAsync(user, ShoppingUserRoles.Admin);
        }
    }
}
