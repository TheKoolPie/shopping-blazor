﻿using Microsoft.EntityFrameworkCore;
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
        public ShoppingDbContext() : base() { }
        public ShoppingDbContext(DbContextOptions<ShoppingDbContext> options) : base(options)
        {

        }
    }
}
