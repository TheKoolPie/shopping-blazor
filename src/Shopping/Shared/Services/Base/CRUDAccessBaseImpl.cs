using Microsoft.Extensions.Logging;
using Shopping.Shared.Data;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.Services.Base
{
    public class CRUDAccessBaseImpl<T> : ICRUDAccess<T> where T : BaseItem
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;

        public CRUDAccessBaseImpl(HttpClient httpClient, ILogger logger)
        {
            _client = httpClient;
            _logger = logger;
        }

        public string BaseAddress { get; protected set; }

        public async Task<T> CreateAsync(T item)
        {
            T retVal = null;
            var response = await _client.PostAsJsonAsync<T>(BaseAddress, item);
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

        public async Task<bool> DeleteAsync(T item)
        {
            return await DeleteByIdAsync(item.Id);
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            var response = await _client.DeleteAsync($"{BaseAddress}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                _logger?.LogError(response.ReasonPhrase);
                return false;
            }
            return true;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _client.GetFromJsonAsync<List<T>>(BaseAddress);
        }

        public async Task<T> GetAsync(string id)
        {
            return await _client.GetFromJsonAsync<T>($"{BaseAddress}/{id}");
        }

        public async Task<T> UpdateAsync(string id, T item)
        {
            T retVal = null;
            var response = await _client.PutAsJsonAsync<T>($"{BaseAddress}/{id}", item);
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
