using Microsoft.Extensions.Logging;
using Shopping.Client.Services.Interfaces;
using Shopping.Shared.Data;
using Shopping.Shared.Results;
using Shopping.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Shopping.Client.Services.Implementations.Base
{
    public class BaseShoppingApiImpl<TEntity, TResult> : ICRUDAccess<TEntity>
        where TEntity : class
        where TResult : BaseResult<TEntity>
    {
        protected readonly string _baseUri;
        private readonly IAuthService _authService;
        private readonly ILogger _logger;

        public BaseShoppingApiImpl(string baseUri, IAuthService authService, ILogger logger)
        {
            _baseUri = "api/" + baseUri;
            _authService = authService;
            _logger = logger;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var client = await _authService.GetHttpClientAsync();

            var response = await client.GetAsync(_baseUri);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }
            var result = await response.Content.ReadFromJsonAsync<TResult>();

            if (response.IsSuccessStatusCode && result.IsSuccessful)
            {
                return result.ResultData;
            }
            else
            {
                _logger.LogError(result.CompleteErrorMessage);
            }
            return null;
        }

        public async Task<TEntity> GetAsync(string id)
        {
            var client = await _authService.GetHttpClientAsync();

            var response = await client.GetAsync($"{_baseUri}/{id}");
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }
            var result = await response.Content.ReadFromJsonAsync<TResult>();

            if (response.IsSuccessStatusCode && result.IsSuccessful)
            {
                return result.ResultData.FirstOrDefault();
            }
            else
            {
                _logger.LogError(result.CompleteErrorMessage);
            }
            return null;
        }

        public async Task<TEntity> CreateAsync(TEntity item)
        {
            var client = await _authService.GetHttpClientAsync();

            var response = await client.PostAsJsonAsync<TEntity>(_baseUri, item);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }

            var result = await response.Content.ReadFromJsonAsync<TResult>();

            if (response.IsSuccessStatusCode && result.IsSuccessful)
            {
                return result.ResultData.FirstOrDefault();
            }
            else
            {
                _logger.LogError(result.CompleteErrorMessage);
            }
            return null;
        }

        public async Task<TEntity> UpdateAsync(string id, TEntity item)
        {
            var client = await _authService.GetHttpClientAsync();

            var response = await client.PutAsJsonAsync<TEntity>($"{_baseUri}/{id}", item);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }
            var result = await response.Content.ReadFromJsonAsync<TResult>();

            if (response.IsSuccessStatusCode && result.IsSuccessful)
            {
                return result.ResultData.FirstOrDefault();
            }
            else
            {
                _logger.LogError(result.CompleteErrorMessage);
            }
            return null;
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            return await SendDelete($"{_baseUri}/{id}");
        }

        protected async Task<bool> SendDelete(string uri)
        {
            var client = await _authService.GetHttpClientAsync();

            var response = await client.DeleteAsync(uri);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }
            var result = await response.Content.ReadFromJsonAsync<TResult>();

            if (!response.IsSuccessStatusCode || !result.IsSuccessful)
            {
                _logger.LogError(result.CompleteErrorMessage);
            }

            return response.IsSuccessStatusCode && result.IsSuccessful;
        }

        public bool ItemAlreadyExists(TEntity item)
        {
            throw new NotImplementedException();
        }

        public bool ItemCanBeUpdated(TEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
