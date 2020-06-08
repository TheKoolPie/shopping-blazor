using Shopping.Shared.Data;
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
    }
}
