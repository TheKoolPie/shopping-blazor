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
    public class CurrentUserApiAccess : ICurrentUserProvider
    {
        private string BaseAddress;
        private readonly IAuthService _authService;
        public CurrentUserApiAccess(IAuthService authService)
        {
            BaseAddress = "api/Users";
            _authService = authService;
        }
        public async Task<ShoppingUserModel> GetUserAsync()
        {
            var client = await _authService.GetHttpClientAsync();
            var response = await client.GetAsync($"{BaseAddress}");
            var result = await response.Content.ReadFromJsonAsync<ShoppingUserResult>();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(result.CompleteErrorMessage);
            }

            return result.ResultData.FirstOrDefault();
        }

        public Task<List<string>> GetUserRolesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsUserAdminAsync()
        {
            throw new NotImplementedException();
        }
    }
}
