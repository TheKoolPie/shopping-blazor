using Microsoft.Extensions.Logging;
using Shopping.Client.Services.Interfaces;
using Shopping.Shared.Data;
using Shopping.Shared.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shopping.Client.Services.Implementations
{
    public class CRUDApiAccessBaseImpl<T> : ICRUDAccess<T> where T : BaseItem
    {
        protected readonly ILogger _logger;
        protected readonly IAuthService _authService;

        public CRUDApiAccessBaseImpl(IAuthService authService, ILogger logger)
        {
            _authService = authService;
            _logger = logger;
        }

        public string BaseAddress { get; protected set; }

        public async Task<T> CreateAsync(T item)
        {
            T retVal = null;

            var client = await _authService.GetHttpClientAsync();

            var response = await client.PostAsJsonAsync<T>(BaseAddress, item);
            if (response.IsSuccessStatusCode)
            {
                retVal = await response.Content.ReadFromJsonAsync<T>();
            }
            else
            {
                _logger.LogError(response.ReasonPhrase);
            }
            return retVal;
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            var client = await _authService.GetHttpClientAsync();

            var response = await client.DeleteAsync($"{BaseAddress}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                _logger?.LogError(response.ReasonPhrase);
                return false;
            }
            return true;
        }

        public async Task<List<T>> GetAllAsync()
        {
            var client = await _authService.GetHttpClientAsync();
            List<T> items = null;

            var response = await client.GetAsync(BaseAddress);
            if (response.IsSuccessStatusCode)
            {
                items = await response.Content.ReadFromJsonAsync<List<T>>();
            }
            return items;
        }

        public async Task<T> GetAsync(string id)
        {
            var client = await _authService.GetHttpClientAsync();
            T item = null;
            var response = await client.GetAsync($"{BaseAddress}/{id}");
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    item = await response.Content.ReadFromJsonAsync<T>();
                }
                catch(Exception e)
                {
                    _logger.LogError("Could not deserialize result", e);
                }
            }
            return item;
        }

        public bool ItemAlreadyExists(T item)
        {
            throw new NotImplementedException();
        }

        public bool ItemCanBeUpdated(T item)
        {
            throw new NotImplementedException();
        }

        public async Task<T> UpdateAsync(string id, T item)
        {
            T retVal = null;
            var client = await _authService.GetHttpClientAsync();

            var response = await client.PutAsJsonAsync<T>($"{BaseAddress}/{id}", item);
            if (response.IsSuccessStatusCode)
            {
                retVal = await response.Content.ReadFromJsonAsync<T>();
            }
            else
            {
                _logger.LogError(response.ReasonPhrase);
            }
            return retVal;
        }
    }
}
