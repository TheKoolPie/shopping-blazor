﻿using Shopping.Shared.Data.Abstractions;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Model.Serialization;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public async Task ImportDataAsync(ShoppingDataSerializationModel data)
        {
            try
            {
                foreach (var category in data.Categories)
                {
                    _repository.Categories.Add(category);
                }
                foreach (var product in data.Products)
                {
                    product.Category = await _repository.Categories.FirstOrDefaultAsync(c => c.Id == product.CategoryId);
                    _repository.Products.Add(product);
                }
                foreach (var group in data.UserGroups)
                {
                    _repository.UserGroups.Add(group);
                }
                foreach (var lists in data.ShoppingLists)
                {
                    _repository.ShoppingLists.Add(lists);
                }
                foreach (var assignments in data.UserGroupShoppingLists)
                {
                    _repository.UserGroupShoppingLists.Add(assignments);
                }


                await _repository.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new PersistencyException("Could not perform import", e);
            }
        }

        public async Task ImportDataJsonAsync(string filePath)
        {
            var jsonString = await File.ReadAllTextAsync(filePath);

            var data = JsonSerializer.Deserialize<ShoppingDataSerializationModel>(jsonString);

            await ImportDataAsync(data);
        }
    }
}