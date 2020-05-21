using Microsoft.EntityFrameworkCore;
using Shopping.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Server
{
    public class ShoppingDbContext : DbContext
    {
        public DbSet<ShoppingItem> Items { get; set; }

        public ShoppingDbContext(DbContextOptions<ShoppingDbContext> options) : base(options)
        {

        }
    }
}
