﻿using Microsoft.Extensions.Logging;
using Shopping.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Shopping.Client.Services.Interfaces;
using System.Threading.Tasks;

namespace Shopping.Client.Services.Implementations
{
    public class ShoppingListsApiAccess : IShoppingLists
    {
        private readonly IShoppingListItems _shoppingListItems;
        private readonly IProducts _products;
        private readonly IProductCategories _categories;
        private readonly ILogger<ShoppingListsApiAccess> _logger;

        public ShoppingListsApiAccess(IShoppingListItems shoppingListItems,
            IProducts products, IProductCategories categories,
            ILogger<ShoppingListsApiAccess> logger)
        {
            _shoppingListItems = shoppingListItems;
            _products = products;
            _categories = categories;
            _logger = logger;
        }

        public async Task<List<ShoppingListModel>> GetAllAsync()
        {
            _logger.LogInformation("Getting all shopping list items");
            var allItems = await _shoppingListItems.GetAllAsync();
            _logger.LogInformation("Group items by date");
            var grouped = allItems.GroupBy(c => c.CreatedAt);
            _logger.LogInformation("Transform to shopping list models");
            var modelList = grouped.Select(c => new ShoppingListModel(c.Key, c.ToList()));
            _logger.LogInformation("Return list");
            return modelList.ToList();
        }

        public async Task<ShoppingListModel> GetByDate(DateTime Date)
        {
            var itemsOfDate = (await _shoppingListItems.GetAllAsync())
                .Where(l => l.CreatedAt.Date == Date.Date)
                .ToList();

            return new ShoppingListModel(Date.Date, itemsOfDate);
        }

        public async Task<ShoppingListModel> SaveShoppingList(ShoppingListModel model)
        {
            var dbData = (await GetByDate(model.ListDate.Date)).Items;
            var modelData = model.Items;

            var dbIds = dbData.Select(i => i.Id);
            var modelIds = modelData.Select(i => i.Id);

            var idsToDelete = dbIds.Except(modelIds).ToList();
            if (idsToDelete.Count > 0)
            {
                foreach (var id in idsToDelete)
                {
                    if (!await _shoppingListItems.DeleteByIdAsync(id))
                    {
                        throw new Exception($"Could not delete shopping list item with id {id}");
                    }
                }
            }


            foreach (var item in modelData)
            {
                var existing = dbData.FirstOrDefault(i => i.Id == item.Id);
                if (existing == null)
                {
                    _logger.LogInformation($"No item with id {item.Id} exists in DB. Create new one");
                    var created = await _shoppingListItems.CreateAsync(item);
                    if (created == null)
                    {
                        throw new Exception($"Could not create new shopping list item");
                    }
                }
                else
                {
                    _logger.LogInformation($"Found item with id {item.Id} in DB. Update");
                    //Provide Product Item and Category data

                    var product = await _products.GetAsync(existing.ProductItemId);
                    product.Category = await _categories.GetAsync(product.CategoryId);

                    item.ProductItem = product;
                    var updated = await _shoppingListItems.UpdateAsync(item.Id, item);
                    if (updated == null)
                    {
                        throw new Exception($"Could not update shopping list item {item.Id}");
                    }
                }
            }

            return await GetByDate(model.ListDate);
        }
    }
}