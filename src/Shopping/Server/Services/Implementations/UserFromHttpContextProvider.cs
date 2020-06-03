using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Shopping.Server.Models;
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
    }
}
