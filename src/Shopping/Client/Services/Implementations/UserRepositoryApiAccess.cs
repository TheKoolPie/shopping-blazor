using Shopping.Client.Services.Interfaces;
using Shopping.Shared.Model.Account;
using Shopping.Shared.Results;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
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
    }
}
