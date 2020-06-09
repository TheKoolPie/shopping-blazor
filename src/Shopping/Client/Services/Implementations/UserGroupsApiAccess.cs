using Microsoft.Extensions.Logging;
using Shopping.Client.Services.Interfaces;
using Shopping.Shared.Data;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Shopping.Client.Services.Implementations
{
    public class UserGroupsApiAccess : CRUDApiAccessBaseImpl<UserGroup>, IUserGroups
    {
        public UserGroupsApiAccess(IAuthService authService, ILogger<UserGroupsApiAccess> logger) : base(authService, logger)
        {
            BaseAddress = "api/UserGroups";
        }

        public Task<List<UserGroup>> GetAllOfUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserIsInGroupAsync(string userGroupId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
