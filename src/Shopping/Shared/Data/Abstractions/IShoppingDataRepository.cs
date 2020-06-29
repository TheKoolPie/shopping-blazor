﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.Data.Abstractions
{
    public interface IShoppingDataRepository
    {
        IDataSet<ProductCategory> Categories { get;}
        IDataSet<ProductItem> Products { get; }
        IDataSet<UserGroup> UserGroups { get; }
        IDataSet<ShoppingList> ShoppingLists { get; }
        IDataSet<UserGroupShoppingList> UserGroupShoppingLists { get;}

        Task SaveChangesAsync();
    }
}