using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Shopping.Client.Provider;
using Shopping.Client.Services.Interfaces;
using Shopping.Shared.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
        private readonly ITokenProvider _tokenProvider;
        private readonly ILogger<AuthService> _logger;

        public AuthService(HttpClient httpClient, ILogger<AuthService> logger,
                   AuthenticationStateProvider authenticationStateProvider,
                   ILocalStorageService localStorage, ITokenProvider tokenProvider)
        {
            _httpClient = httpClient;
            _logger = logger;
            _authStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
            _tokenProvider = tokenProvider;
        }

        public async Task<RegisterResult> Register(RegisterModel model)
        {
            var client = await GetHttpClient();
            var response = await client.PostAsJsonAsync("api/accounts/register", model);
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
            ((ApiAuthenticationStateProvider)_authStateProvider).MarkUserAsAuthenticated(result.Token);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", result.Token);

            return result;
        }
        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        protected async Task<HttpClient> GetHttpClient()
        {
            if (_httpClient.DefaultRequestHeaders.Authorization == null)
            {
                var token = await _tokenProvider.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                {
                    _logger.LogError($"Could not find access token");
                }
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
            return _httpClient;
        }
    }
}
