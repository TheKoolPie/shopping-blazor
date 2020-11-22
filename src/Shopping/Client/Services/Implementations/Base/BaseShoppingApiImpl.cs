using Microsoft.Extensions.Logging;
using Shopping.Client.Services.Interfaces;
using Shopping.Shared.Data;
using Shopping.Shared.Results;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        protected readonly ILogger _logger;

        public BaseShoppingApiImpl(string baseUri, IAuthService authService, ILogger logger)
        {
            _baseUri = "api/" + baseUri;
            _authService = authService;
            _logger = logger;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var client = await GetApiClient();

            var response = await client.GetAsync(_baseUri);

            CheckForUnauthorized(response);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TResult>();
                if (result.IsSuccessful)
                {
                    return result.ResultData;
                }
                else
                {
                    _logger.LogError("Result is not set to successful", result.ErrorMessages);
                }
            }
            else
            {
                _logger.LogError($"Response has no success status code: {response.StatusCode}");
            }

            return null;
        }

        public async Task<TEntity> GetAsync(string id)
        {
            var client = await GetApiClient();

            var response = await client.GetAsync($"{_baseUri}/{id}");

            CheckForUnauthorized(response);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TResult>();
                if (result.IsSuccessful)
                {
                    return result.ResultData.FirstOrDefault();
                }
                else
                {
                    _logger.LogError("Result is not set to successful", result.ErrorMessages);
                }
            }
            else
            {
                _logger.LogError($"Response has no success status code: {response.StatusCode}");
            }

            return null;
        }

        public async Task<TEntity> CreateAsync(TEntity item)
        {
            var client = await GetApiClient();

            var response = await client.PostAsJsonAsync<TEntity>(_baseUri, item);

            CheckForUnauthorized(response);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TResult>();
                if (result.IsSuccessful)
                {
                    return result.ResultData.FirstOrDefault();
                }
                else
                {
                    _logger.LogError("Result is not set to successful", result.ErrorMessages);
                }
            }
            else
            {
                _logger.LogError($"Response has no success status code: {response.StatusCode}");
            }

            return null;
        }

        public async Task<TEntity> UpdateAsync(string id, TEntity item)
        {
            var client = await GetApiClient();

            var response = await client.PutAsJsonAsync<TEntity>($"{_baseUri}/{id}", item);

            CheckForUnauthorized(response);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TResult>();
                if (result.IsSuccessful)
                {
                    return result.ResultData.FirstOrDefault();
                }
                else
                {
                    _logger.LogError("Result is not set to successful", result.ErrorMessages);
                }
            }
            else
            {
                _logger.LogError($"Response has no success status code: {response.StatusCode}");
            }

            return null;
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            return await SendDelete($"{_baseUri}/{id}");
        }

        protected async Task<bool> SendDelete(string uri)
        {
            var client = await GetApiClient();

            var response = await client.DeleteAsync(uri);

            CheckForUnauthorized(response);

            bool deleteResult = false;

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TResult>();
                deleteResult = result.IsSuccessful;
                if (!deleteResult)
                {
                    _logger.LogError("Result is not set to successful", result.ErrorMessages);
                }
            }
            else
            {
                _logger.LogError($"Response has no success status code: {response.StatusCode}");
            }
            return deleteResult;
        }

        protected void CheckForUnauthorized(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }
        }

        protected async Task<HttpClient> GetApiClient()
        {
            return await _authService.GetHttpClientAsync();
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
