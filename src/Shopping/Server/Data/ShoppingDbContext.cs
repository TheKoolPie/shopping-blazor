using Microsoft.EntityFrameworkCore;
using Shopping.Client.Pages;
using Shopping.Shared;
using Shopping.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Server.Data
{
    public class ShoppingDbContext : DbContext
    {
        public DbSet<ProductCategory> Categories { get; set; }
        public DbSet<ProductItem> Products { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }

        public ShoppingDbContext() : base() { }
        public ShoppingDbContext(DbContextOptions<ShoppingDbContext> options) : base(options)
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
                .Property(e => e.UserGroupIds)
                .HasConversion(
                    v => string.Join(",", v),
                    v => v.Split(',', System.StringSplitOptions.None).Select(x => x).ToList());
            modelBuilder
                .Entity<ShoppingList>()
                .OwnsMany(l => l.Items);

            base.OnModelCreating(modelBuilder);
        }
    }
}
