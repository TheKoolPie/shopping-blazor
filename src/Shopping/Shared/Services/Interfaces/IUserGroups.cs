using Shopping.Shared.Data;
using Shopping.Shared.Model.Account;
using Shopping.Shared.Model.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.Services.Interfaces
{
    public interface IUserGroups : ICRUDAccess<UserGroup>
    {
        Task<List<UserGroup>> GetAllOfUserAsync(string userId);
        Task<bool> UserIsInGroupAsync(string userGroupId, string userId);
        Task<UserGroup> AddUserToGroup(string userGroupId, ShoppingUserModel user);
        Task<List<ShoppingUserModel>> GetUsersInGroup(string userGroupId);
    }
}
