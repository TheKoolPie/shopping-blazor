
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
            context.SaveChanges();

            return context;
        }
    }
}
