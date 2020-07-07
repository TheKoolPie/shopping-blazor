using Microsoft.EntityFrameworkCore;
using Shopping.Client.Pages;
using Shopping.Shared;
using Shopping.Shared.Data;
using Shopping.Shared.Data.Abstractions;
using Shopping.Shared.Model.Account;
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
        public DbSet<ShoppingListItem> ShoppingListItems { get; set; }
        public DbSet<UserGroupShoppingList> UserGroupShoppingLists { get; set; }
        public DbSet<UserGroupMembers> UserGroupMembers { get; set; }

        public ShoppingDbContext() : base() { }
        public ShoppingDbContext(DbContextOptions<ShoppingDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
