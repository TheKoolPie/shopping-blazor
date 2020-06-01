using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Shopping.Client.Provider;
using Shopping.Client.Services.Interfaces;
using Shopping.Shared.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shopping.Client.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient,
                   AuthenticationStateProvider authenticationStateProvider,
                   ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<RegisterResult> Register(RegisterModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/accounts/register", model);
            var result = JsonSerializer.Deserialize<RegisterResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result;
        }

        public async Task<LoginResult> Login(LoginModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/accounts/login", model);

            var result = await response.Content.ReadFromJsonAsync<LoginResult>();

            if (!response.IsSuccessStatusCode)
            {
                return result;
            }

            await _localStorage.SetItemAsync("authToken", result.Token);
            ((ApiAuthenticationStateProvider)_authStateProvider).MarkUserAsAuthenticated(model.LoginName);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", result.Token);

            return result;
        }
        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
