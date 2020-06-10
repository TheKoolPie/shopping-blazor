using Shopping.Shared.Model.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopping.Server.Services
{
    public interface ICurrentUserProvider
    {
        Task<ShoppingUserModel> GetUserAsync();
        Task<bool> IsUserAdminAsync();
        Task<List<string>> GetUserRolesAsync();
    }
}
