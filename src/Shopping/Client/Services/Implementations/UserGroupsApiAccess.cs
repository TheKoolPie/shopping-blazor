using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shopping.Client.Services.Interfaces;
using Shopping.Shared.Data;
using Shopping.Shared.Model.Account;
using Shopping.Shared.Results;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shopping.Client.Services.Implementations
{
    public class UserGroupsApiAccess : CRUDApiAccessBaseImpl<UserGroup>, IUserGroupRepository
    {
        public UserGroupsApiAccess(IAuthService authService, ILogger<UserGroupsApiAccess> logger) : base(authService, logger)
        {
            BaseAddress = "api/UserGroups";
        }

        public async Task<UserGroup> AddUserToGroup(string userGroupId, ShoppingUserModel user)
        {
            var client = await _authService.GetHttpClientAsync();

            var content = new StringContent(JsonConvert.SerializeObject(user), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{BaseAddress}/AddUser/{userGroupId}", content);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<UserGroupResult>();
                if (!result.IsSuccessful)
                {
                    throw new Exception(result.CompleteErrorMessage);
                }
                else
                {
                    return result.ResultData.FirstOrDefault();
                }
            }
            return null;
        }

        public Task<List<UserGroup>> GetAllOfUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserGroup>> GetCommonGroupsAsync(string userOneId, string userTwoId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ShoppingUserModel>> GetUsersInGroup(string userGroupId)
        {
            var client = await _authService.GetHttpClientAsync();
            var response = await client.GetAsync($"{BaseAddress}/GetUsersInGroup/{userGroupId}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ShoppingUserResult>();
                if (!result.IsSuccessful)
                {
                    throw new Exception(result.CompleteErrorMessage);
                }
                else
                {
                    return result.ResultData;
                }
            }
            return null;
        }

        public async Task<UserGroup> RemoveUserFromGroup(string userGroupId, ShoppingUserModel user)
        {
            var client = await _authService.GetHttpClientAsync();

            var content = new StringContent(JsonConvert.SerializeObject(user), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{BaseAddress}/RemoveUser/{userGroupId}", content);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<UserGroupResult>();
                if (!result.IsSuccessful)
                {
                    throw new Exception(result.CompleteErrorMessage);
                }
                else
                {
                    return result.ResultData.FirstOrDefault();
                }
            }
            return null;
        }

        public Task<bool> UserIsInGroupAsync(string userGroupId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
