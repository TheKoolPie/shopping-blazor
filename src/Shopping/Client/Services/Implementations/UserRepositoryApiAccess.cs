using Newtonsoft.Json;
using Shopping.Client.Services.Interfaces;
using Shopping.Shared.Model.Account;
using Shopping.Shared.Results;
using Shopping.Shared.Results.Account;
using Shopping.Shared.Services;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Client.Services.Implementations
{
    public class UserRepositoryApiAccess : IUserRepository
    {
        private string BaseAddress;
        private readonly IAuthService _authService;
        public UserRepositoryApiAccess(IAuthService authService)
        {
            BaseAddress = "api/Users";
            _authService = authService;
        }

        public async Task<ShoppingUserModel> GetUserByIdAsync(string id)
        {
            var client = await _authService.GetHttpClientAsync();
            var response = await client.GetAsync($"{BaseAddress}/{id}");

            var resultObject = await response.Content.ReadFromJsonAsync<ShoppingUserResult>();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(resultObject.CompleteErrorMessage);
            }

            return resultObject.ResultData.FirstOrDefault();
        }

        public Task<ShoppingUserModel> GetUserByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }
        public Task<List<string>> GetRolesOfUserAsync(ShoppingUserModel userModel)
        {
            throw new NotImplementedException();
        }

        public Task<ShoppingUserModel> GetUserAsync(ShoppingUserModel userModel)
        {
            throw new NotImplementedException();
        }

        public Task<ShoppingUserModel> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<ShoppingUserModel> UpdateUserData(string id, ShoppingUserModel updateData)
        {
            var client = await _authService.GetHttpClientAsync();
            var response = await client.PutAsJsonAsync($"{BaseAddress}/{id}", updateData);

            var resultObject = await response.Content.ReadFromJsonAsync<ShoppingUserResult>();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(resultObject.CompleteErrorMessage);
            }

            return resultObject.ResultData.FirstOrDefault();
        }

        public async Task<ShoppingUserSettingsModel> UpdateUserSettingsAsync(string userId, ShoppingUserSettingsModel settingsData)
        {
            var client = await _authService.GetHttpClientAsync();
            var response = await client.PutAsJsonAsync($"{BaseAddress}/UpdateUserSettings/{userId}", settingsData);
            var resultObject = await response.Content.ReadFromJsonAsync<UpdateUserSettingsResult>();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(resultObject.CompleteErrorMessage);
            }
            return resultObject.ResultData.FirstOrDefault();

        }
    }
}
