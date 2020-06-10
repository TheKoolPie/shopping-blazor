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
    public class UserFromHttpContextProvider : ICurrentUserProvider
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
        public async Task<ShoppingUserModel> GetUserAsync()
        {
            ShoppingUserModel user = null;
            var name = _httpContextAccessor.HttpContext.User.Identity.Name;
            if (!string.IsNullOrEmpty(name))
            {
                _logger.LogDebug($"Searching for user with user name or email: {name}");
                var dbUser = await _userManager.FindByNameAsync(name);
                if (dbUser == null)
                {
                    dbUser = await _userManager.FindByEmailAsync(name);
                }
                if (dbUser != null)
                {
                    user = new ShoppingUserModel()
                    {
                        Id = dbUser.Id,
                        Email = dbUser.Email,
                        UserName = dbUser.UserName
                    };
                }
            }
            return user;
        }

        public async Task<List<string>> GetUserRolesAsync()
        {
            var user = await GetUserAsync();
            List<string> roles = null;

            var dbUser = await _userManager.FindByIdAsync(user.Id);
            if (dbUser != null)
            {
                roles = (await _userManager.GetRolesAsync(dbUser)).ToList();
            }

            return roles;
        }

        public async Task<bool> IsUserAdminAsync()
        {
            var user = await GetUserAsync();
            bool result = false;

            var dbUser = await _userManager.FindByIdAsync(user.Id);
            if (dbUser != null)
            {
                result = await _userManager.IsInRoleAsync(dbUser, ShoppingUserRoles.Admin);
            }

            return result;
        }
    }
}
