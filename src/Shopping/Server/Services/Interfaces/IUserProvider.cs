using Shopping.Server.Models;
using Shopping.Shared.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Server.Services
{
    public interface IUserProvider
    {
        Task<ShoppingUser> GetUserAsync();
        Task<bool> IsUserAdminAsync();
        Task<bool> IsUserAdminAsync(string userId);
        Task<List<string>> GetUserRolesAsync();
        Task<List<ShoppingUser>> GetUsersInRoleAsync(string role);
    }
}
