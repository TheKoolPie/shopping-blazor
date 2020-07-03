using Shopping.Shared.Data;
using Shopping.Shared.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Shared.Model.Serialization
{
    public class ShoppingDataSerializationModel
    {
        public List<ProductCategory> Categories { get; set; }
        public List<ProductItem> Products { get; set; }
        public List<UserGroup> UserGroups { get; set; }
        public List<ShoppingList> ShoppingLists { get; set; }
        public List<UserGroupShoppingList> UserGroupShoppingLists { get; set; }

        public ShoppingDataSerializationModel()
        {
            Categories = new List<ProductCategory>();
            Products = new List<ProductItem>();
            UserGroups = new List<UserGroup>();
            ShoppingLists = new List<ShoppingList>();
            UserGroupShoppingLists = new List<UserGroupShoppingList>();
        }
        public ShoppingDataSerializationModel(IShoppingDataRepository dataRepo)
        {
            Categories = dataRepo.Categories.ToList();
            Products = dataRepo.Products.ToList();
            UserGroups = dataRepo.UserGroups.ToList();
            ShoppingLists = dataRepo.ShoppingLists.ToList();
            UserGroupShoppingLists = dataRepo.UserGroupShoppingLists.ToList();
        }
    }
}
