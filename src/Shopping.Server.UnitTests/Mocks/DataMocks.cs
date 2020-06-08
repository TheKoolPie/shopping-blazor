using Microsoft.Azure.Cosmos;
using Shopping.Server.Models;
using Shopping.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shopping.Server.UnitTests.Mocks
{
    public static class DataMocks
    {
        public const string AdminId = "abc-def";
        public const string AdminMail = "admin@test.de";
        public const string AdminName = "admin";
        public const string ManagerId = "ghi-jkl";
        public const string ManagerMail = "manager@test.de";
        public const string ManagerName = "manager";
        public const string CreatorId = "mno-pqr";
        public const string CreatorMail = "creator@test.de";
        public const string CreatorName = "creator";
        public const string UserId = "stu-vwx";
        public const string UserMail = "user@test.de";
        public const string UserName = "user";

        public const string Group_All_Id = "123-456";
        public const string Group_ManagerCreatorUser_Id = "789-123";
        public const string Group_ManagerUser_Id = "a1b2-c3d4";
        public const string Group_Admin_Id = "456-789";
        public static List<ShoppingUser> GetUsers()
        {
            return new List<ShoppingUser>
            {
                new ShoppingUser()
                {
                    Id = AdminId,
                    Email = AdminMail,
                    UserName = AdminName
                },
                new ShoppingUser()
                {
                    Id = ManagerId,
                    Email = ManagerMail,
                    UserName = ManagerName
                },
                new ShoppingUser()
                {
                    Id = CreatorId,
                    Email = CreatorMail,
                    UserName = CreatorName,
                },
                new ShoppingUser()
                {
                    Id = UserId,
                    Email = UserMail,
                    UserName = UserName
                }
            };
        }
        public static List<UserGroup> GetUserGroups()
        {
            return new List<UserGroup>
            {
                new UserGroup()
                {
                    Id = Group_All_Id,
                    Name = "All",
                    OwnerId = AdminId,
                    Members = new List<UserGroupMember>(GetUsers().Select(x=>new UserGroupMember{UserId = x.Id })),
                },
                new UserGroup()
                {
                    Id = Group_ManagerCreatorUser_Id,
                    Name = "Group 1",
                    OwnerId = ManagerId,
                    Members = new List<UserGroupMember>
                    {
                        new UserGroupMember
                        {
                            UserId = ManagerId,
                        },
                        new UserGroupMember
                        {
                            UserId = CreatorId,
                        },
                        new UserGroupMember
                        {
                            UserId = UserId,
                        },
                    }
                },
                new UserGroup()
                {
                    Id = Group_Admin_Id,
                    Name = "Admin",
                    OwnerId = AdminId,
                    Members = new List<UserGroupMember>
                    {
                        new UserGroupMember
                        {
                            UserId = AdminId,
                        },
                    }
                },
                new UserGroup()
                {
                    Id = Group_ManagerUser_Id,
                    Name = "Group 2",
                    OwnerId = ManagerId,
                    Members = new List<UserGroupMember>
                    {
                        new UserGroupMember
                        {
                            UserId = ManagerId,
                        },
                        new UserGroupMember
                        {
                            UserId = UserId,
                        },
                    }
                },
            };
        }
        public static List<ProductCategory> GetProductCategories()
        {
            return new List<ProductCategory>
            {
                new ProductCategory()
                {
                    Id="aaa-bbb",
                    Name = "Wurst",
                    ColorCode = "#fff111",
                },
                new ProductCategory()
                {
                    Id="ccc-ddd",
                    Name = "Käse",
                    ColorCode = "#111fff"
                },
                new ProductCategory()
                {
                    Id="eee-fff",
                    Name = "Gemüse",
                    ColorCode = "#ff11ff"
                }
            };
        }
        public static List<ProductItem> GetProducts()
        {
            return new List<ProductItem>
            {
                new ProductItem()
                {
                    Id="0001",
                    Name = "Pute",
                    Unit = Shared.Enums.ProductUnit.Gramm,
                    CategoryId = "aaa-bbb",
                },
                new ProductItem()
                {
                    Id="0010",
                    Name = "Cheddar",
                    Unit = Shared.Enums.ProductUnit.Gramm,
                    CategoryId = "ccc-ddd",
                },
                new ProductItem()
                {
                    Id="0011",
                    Name = "Brokkoli",
                    Unit = Shared.Enums.ProductUnit.Piece,
                    CategoryId = "eee-fff",
                }
            };
        }

        public static List<ShoppingListItem> GetShoppingListItems()
        {
            return new List<ShoppingListItem>
            {
                new ShoppingListItem
                {
                    Amount = 500,
                    Done = false,
                    ProductItemId = "0001"
                },
                new ShoppingListItem
                {
                    Amount = 3,
                    Done = true,
                    ProductItemId = "0011"
                }
            };

        }

        public static List<ShoppingList> GetShoppingLists()
        {
            return new List<ShoppingList>
            {
                new ShoppingList
                {
                    Id = "123456789",
                    ListDate = new DateTime(1993,5,4),
                    OwnerId = AdminId,
                    UserGroups = new List<UserGroup>
                    {
                        new UserGroup
                        {
                            Id = Group_ManagerCreatorUser_Id,
                        },
                        new UserGroup
                        {
                            Id = Group_Admin_Id
                        }
                    },
                    Items = new List<ShoppingListItem>{ GetShoppingListItems()[0], GetShoppingListItems()[1] }
                },
                new ShoppingList
                {
                    Id = "1011121314",
                    ListDate = new DateTime(1993,5,4),
                    OwnerId = CreatorId,
                    UserGroups = new List<UserGroup>
                    {
                        new UserGroup
                        {
                            Id = Group_ManagerUser_Id
                        },
                    },
                    Items = new List<ShoppingListItem>{ GetShoppingListItems()[0], GetShoppingListItems()[1] }
                }
            };
        }
    }
}
