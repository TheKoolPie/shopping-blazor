using Blazored.LocalStorage;
using Shopping.Client.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Client.Services.Implementations
{
    public class TokenProviderLocalStorage : ITokenProvider
    {
        private readonly ILocalStorageService _localStorage;
        public TokenProviderLocalStorage(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }
        public async Task<string> GetTokenAsync()
        {
            return await _localStorage.GetItemAsync<string>("authToken");
        }
    }
}
