
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shopping.Server.Data;
using Shopping.Shared.Data;
using System;
using System.Linq;

namespace Shopping.Server.UnitTests.Mocks
{
    public class ShoppingDbMockContext : ShoppingDbContext
    {
        public ShoppingDbMockContext(DbContextOptions<ShoppingDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<UserGroup>()
                .Property(e => e.MemberIds)
                .HasConversion(
                    v => string.Join(",", v),
                    v => v.Split(',', System.StringSplitOptions.None).Select(x => x).ToList());
            modelBuilder
                .Entity<ShoppingList>()
                .Property(e=>e.UserGroupIds)
                .HasConversion(
                    v => string.Join(",", v),
                    v => v.Split(',', System.StringSplitOptions.None).Select(x => x).ToList());
        }
    }

    public static class DBContextMocks
    {


        public static ShoppingDbMockContext GetMock()
        {
            var options = new DbContextOptionsBuilder<ShoppingDbContext>()
                .UseInMemoryDatabase($"InMemoryShopping_{Guid.NewGuid().ToString()}")
                .Options;

            var context = new ShoppingDbMockContext(options);

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
