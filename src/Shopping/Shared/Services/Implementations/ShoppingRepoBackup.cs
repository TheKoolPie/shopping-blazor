using Shopping.Shared.Data.Abstractions;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Model.Serialization;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Shopping.Shared.Services.Implementations
{
    public class ShoppingRepoBackup : IShoppingRepoBackup
    {
        private readonly IShoppingDataRepository _repository;
        public ShoppingRepoBackup(IShoppingDataRepository repository)
        {
            _repository = repository;
        }

        public async Task ExportDataJsonAsync(string filePath)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };

            var serializationModel = new ShoppingDataSerializationModel(_repository);
            var jsonString = JsonSerializer.Serialize(serializationModel, options);

            await File.WriteAllTextAsync(filePath, jsonString);
        }

        public async Task ImportDataJsonAsync(string filePath)
        {
            var jsonString = await File.ReadAllTextAsync(filePath);

            var model = JsonSerializer.Deserialize<ShoppingDataSerializationModel>(jsonString);

            foreach (var category in model.Categories)
            {
                _repository.Categories.Add(category);
            }
            foreach (var product in model.Products)
            {
                _repository.Products.Add(product);
            }
            foreach (var group in model.UserGroups)
            {
                _repository.UserGroups.Add(group);
            }
            foreach (var lists in model.ShoppingLists)
            {
                _repository.ShoppingLists.Add(lists);
            }
            foreach (var assignments in model.UserGroupShoppingLists)
            {
                _repository.UserGroupShoppingLists.Add(assignments);
            }

            try
            {
                await _repository.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new PersistencyException("Could not perform import", e);
            }
        }
    }
}
