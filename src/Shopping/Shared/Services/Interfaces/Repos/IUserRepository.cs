using Shopping.Shared.Data;
using Shopping.Shared.Model.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.Services.Interfaces
{
    public interface IUserRepository
    {
        Task<ShoppingUserModel> GetUserByIdAsync(string id);
        Task<ShoppingUserModel> GetUserByNameAsync(string userName);
        Task<ShoppingUserModel> GetUserByEmailAsync(string email);
        Task<List<string>> GetRolesOfUserAsync(ShoppingUserModel userModel);
        Task<ShoppingUserModel> GetUserAsync(ShoppingUserModel userModel);
        Task<ShoppingUserModel> UpdateUserData(string id, ShoppingUserModel updateData);
    }
}
