using Shopping.Shared.Data;
using Shopping.Shared.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Categories = dataRepo.Categories?.ToList() ?? new List<ProductCategory>();
            Products = dataRepo.Products?.ToList() ?? new List<ProductItem>();
            UserGroups = dataRepo.UserGroups?.ToList() ?? new List<UserGroup>();
            foreach (var group in UserGroups)
            {
                var members = dataRepo.UserGroupMembers.ToList();
                var membersOfGroup = members.Where(m => m.UserGroupId == group.Id);
                group.Members = new List<Account.ShoppingUserModel>();
                foreach (var member in membersOfGroup)
                {
                    group.Members.Add(new Account.ShoppingUserModel
                    {
                        Id = member.MemberId
                    });
                }
            }
            ShoppingLists = dataRepo.ShoppingLists?.ToList() ?? new List<ShoppingList>();
            foreach (var list in ShoppingLists)
            {
                var listItems = dataRepo.ShoppingListItems.ToList();
                var itemsOfList = listItems.Where(i => i.ShoppingListId == list.Id);
                list.Items = new List<ShoppingListItem>();
                foreach (var item in itemsOfList)
                {
                    item.ProductItem = null;
                    list.Items.Add(item);
                }
            }
            UserGroupShoppingLists = dataRepo.UserGroupShoppingLists?.ToList() ?? new List<UserGroupShoppingList>();
            foreach (var assignment in UserGroupShoppingLists)
            {
                assignment.ShoppingList = null;
                assignment.UserGroup = null;
            }
        }
    }
}
