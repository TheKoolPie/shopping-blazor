using Microsoft.Extensions.Logging;
using Shopping.Shared.Model.Serialization;
using Shopping.Shared.Results;
using Shopping.Shared.Services;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Shopping.Client.Services.Implementations
{
    public class ShoppingListRepoBackupApiAccess : IShoppingRepoBackup
    {
        protected readonly ILogger<ShoppingListRepoBackupApiAccess> _logger;
        protected readonly IAuthService _authService;

        private string uri => "api/Backup";

        public ShoppingListRepoBackupApiAccess(IAuthService authService, ILogger<ShoppingListRepoBackupApiAccess> logger)
        {
            _logger = logger;
            _authService = authService;
        }

        public Task ExportDataJsonAsync(string filePath)
        {
            throw new NotImplementedException();
        }

        public async Task ImportDataAsync(ShoppingDataSerializationModel data)
        {
            var client = await _authService.GetHttpClientAsync();

            foreach (var product in data.Products)
            {
                product.Category = data.Categories.FirstOrDefault(c => c.Id == product.CategoryId);
            }

            var response = await client.PostAsJsonAsync(uri, data);

            var result = await response.Content.ReadFromJsonAsync<DatabaseBackupResult>();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(result.CompleteErrorMessage);
            }
        }

        public Task ImportDataJsonAsync(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
