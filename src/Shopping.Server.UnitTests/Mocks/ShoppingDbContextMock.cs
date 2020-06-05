
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shopping.Server.Data;
using Shopping.Shared.Data;
using System;
using System.Linq;

namespace Shopping.Server.UnitTests.Mocks
{
    public static class DBContextMocks
    {


        public static ShoppingDbContext GetMock()
        {
            var options = new DbContextOptionsBuilder<ShoppingDbContext>()
                .UseInMemoryDatabase($"InMemoryShopping_{Guid.NewGuid().ToString()}")
                .Options;

            var context = new ShoppingDbContext(options);

            foreach (var group in DataMocks.GetUserGroups())
            {
                context.UserGroups.Add(group);
            }
            foreach (var category in DataMocks.GetProductCategories())
            {
                context.Categories.Add(category);
            }
            foreach (var product in DataMocks.GetProducts())
            {
                context.Products.Add(product);
            }
            foreach(var list in DataMocks.GetShoppingLists())
            {
                context.ShoppingLists.Add(list);
            }

            context.SaveChanges();

            return context;
        }
    }
}
