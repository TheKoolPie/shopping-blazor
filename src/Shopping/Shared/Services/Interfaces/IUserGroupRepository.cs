using Shopping.Shared.Data;
using Shopping.Shared.Model.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopping.Shared.Services.Interfaces
{
    public interface IUserGroupRepository : ICRUDAccess<UserGroup>
    {
        Task<List<UserGroup>> GetAllOfUserAsync(string userId);
        Task<bool> UserIsInGroupAsync(string userGroupId, string userId);
        Task<UserGroup> AddUserToGroup(string userGroupId, ShoppingUserModel user);
        Task<UserGroup> RemoveUserFromGroup(string userGroupId, ShoppingUserModel user);
        Task<List<ShoppingUserModel>> GetUsersInGroup(string userGroupId);
        Task<List<UserGroup>> GetCommonGroupsAsync(string userOneId, string userTwoId);
    }
}
