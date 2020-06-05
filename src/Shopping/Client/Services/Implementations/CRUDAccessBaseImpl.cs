﻿using Microsoft.Extensions.Logging;
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
using System.Threading.Tasks;

namespace Shopping.Client.Services.Implementations
{
    public class CRUDAccessBaseImpl<T> : ICRUDAccess<T> where T : BaseItem
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;
        private readonly ITokenProvider _tokenProvider;

        public CRUDAccessBaseImpl(HttpClient httpClient,
            ITokenProvider tokenProvider,
            ILogger logger)
        {
            _client = httpClient;
            _logger = logger;
            _tokenProvider = tokenProvider;
        }

        public string BaseAddress { get; protected set; }

        public async Task<T> CreateAsync(T item)
        {
            T retVal = null;

            var client = await GetHttpClient();

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

        public async Task<bool> DeleteAsync(T item)
        {
            return await DeleteByIdAsync(item.Id);
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            var client = await GetHttpClient();

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
            var client = await GetHttpClient();

            return await client.GetFromJsonAsync<List<T>>(BaseAddress);
        }

        public async Task<T> GetAsync(string id)
        {
            var client = await GetHttpClient();

            return await client.GetFromJsonAsync<T>($"{BaseAddress}/{id}");
        }

        public async Task<T> UpdateAsync(string id, T item)
        {
            T retVal = null;
            var client = await GetHttpClient();

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

        private async Task<HttpClient> GetHttpClient()
        {
            if (_client.DefaultRequestHeaders.Authorization == null)
            {
                var token = await _tokenProvider.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                {
                    _logger.LogError($"Could not find access token");
                }
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
            return _client;
        }
    }
}
