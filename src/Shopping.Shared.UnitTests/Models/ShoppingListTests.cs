using Shopping.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Shopping.Shared.UnitTests
{
    public class ShoppingListTests
    {
        [Fact]
        public void AddOrUpdateItem_ItemDoesNotExist_NewItemGetsAddedToList()
        {
            var list = new ShoppingList();
            var item = new ShoppingListItem
            {
                ProductItemId = "1234",
                Id = "abcd-efgh",
                Amount = 500.0f,
                CreatedAt = DateTime.Now,
                Done = false,
            };

            list.AddOrUpdateItem(item);

            Assert.True(list.Items.Count == 1);
        }
        [Fact]
        public void AddOrUpdateItem_ItemExists_ValuesGetUpdated()
        {
            var list = new ShoppingList();
            var item = new ShoppingListItem
            {
                ProductItemId = "1234",
                Id = "abcd-efgh",
                Amount = 500.0f,
                CreatedAt = DateTime.Now,
                Done = false,
            };

            list.AddOrUpdateItem(item);

            var updatedItem = new ShoppingListItem(item);
            updatedItem.Amount = 25;
            updatedItem.Done = true;

            list.AddOrUpdateItem(updatedItem);

            Assert.True(list.Items.Count == 1);
            Assert.Equal(25, list.Items.FirstOrDefault().Amount);
            Assert.True(list.Items.FirstOrDefault().Done);
        }

        [Fact]
        public void AddUserGroup_GroupIdDoesNotExist_GroupIdGetsAdded()
        {
            var list = new ShoppingList();
            var group = new UserGroup()
            {
                Id = "abc-def"
            };

            list.AddUserGroup(group);

            Assert.Contains("abc-def", list.UserGroupIds);
        }
        [Fact]
        public void AddUserGroup_GroupIdAlreadyExists_GroupDoesNotGetAdded()
        {
            var list = new ShoppingList();
            var group = new UserGroup()
            {
                Id = "abc-def"
            };

            list.AddUserGroup(group);
            var group2 = new UserGroup(group);
            list.AddUserGroup(group2);

            Assert.True(list.UserGroupIds.Count == 1,"Item was added even though it shouldn't or not added at all");
        }
        [Fact]
        public void RemoveItem_IdExists_ItemGetsDeleted()
        {
            var list = new ShoppingList();
            var item = new ShoppingListItem
            {
                ProductItemId = "1234",
                Id = "abcd-efgh",
                Amount = 500.0f,
                CreatedAt = DateTime.Now,
                Done = false,
            };

            list.AddOrUpdateItem(item);
            var itemWasAdded = list.Items.Count == 1;

            list.RemoveItem(item.Id);
            Assert.True(itemWasAdded && list.Items.Count == 0, "Item not deleted or not added at all");
        }
        [Fact]
        public void RemoveItem_IdDoesNotExist_NoItemGetsDeleted()
        {
            var list = new ShoppingList();
            var item = new ShoppingListItem
            {
                ProductItemId = "1234",
                Id = "abcd-efgh",
                Amount = 500.0f,
                CreatedAt = DateTime.Now,
                Done = false,
            };

            list.AddOrUpdateItem(item);
            var itemWasAdded = list.Items.Count == 1;

            list.RemoveItem("xyz");
            Assert.True(itemWasAdded && list.Items.Count == 1, "No item available, maybe nothing was added");
        }
        [Fact]
        public void RemoveUserGroup_IdDoesExist_GroupGetsDeleted()
        {
            var list = new ShoppingList();
            var group = new UserGroup()
            {
                Id = "abc-def"
            };

            list.AddUserGroup(group);
            var groupWasAdded = list.UserGroupIds.Count == 1;
            list.RemoveUserGroup(group.Id);

            Assert.True(groupWasAdded && list.UserGroupIds.Count == 0, "Group was not deleted or not added at all");
        }

        [Fact]
        public void RemoveUserGroup_IdDoesNotExist_NoGroupGetsDeleted()
        {
            var list = new ShoppingList();
            var group = new UserGroup()
            {
                Id = "abc-def"
            };

            list.AddUserGroup(group);
            var groupWasAdded = list.UserGroupIds.Count == 1;
            list.RemoveUserGroup("xyz");

            Assert.True(groupWasAdded && list.UserGroupIds.Count == 1, "Group was deleted or not added at all");
        }

        [Fact]
        public void Update_UpdateUserGroupIds_NewIdsInObject()
        {
            var groupIdList1 = new List<string>
                {
                    "abc",
                    "xyz",
                };
            var groupIdList2 = new List<string>
                {
                    "123",
                    "456",
                    "789"
                };

            var list = new ShoppingList()
            {
                UserGroupIds = groupIdList1
            };

            list.Update(new ShoppingList()
            {
                UserGroupIds = groupIdList2
            });

            var compareList = new List<string>
            {
                "789",
                "123",
                "456"
            };

            Assert.Equal(3, list.UserGroupIds.Count);
            Assert.True(list.UserGroupIds.All(compareList.Contains));
        }

        [Fact]
        public void HasDifferentItems_ItemListContainsSameElements_ReturnsFalse()
        {
            var list1 = new List<ShoppingListItem>
            {
                new ShoppingListItem
                {
                    ProductItemId = "12334234",
                    Id = "abcd-lmon",
                    Amount = 500.0f,
                    CreatedAt = DateTime.Now,
                    Done = false,
                },
                new ShoppingListItem
                {
                    ProductItemId = "5601278",
                    Id = "abcd-7994",
                    Amount = 5.0f,
                    CreatedAt = DateTime.Now,
                    Done = true,
                },
                 new ShoppingListItem
                {
                    ProductItemId = "94831",
                    Id = "abcd-efgh",
                    Amount = 100.0f,
                    CreatedAt = DateTime.Now,
                    Done = false,
                },
            };
            var list2 = new List<ShoppingListItem>
            {
                new ShoppingListItem
                {
                    ProductItemId = "5601278",
                    Id = "abcd-7994",
                    Amount = 5.0f,
                    CreatedAt = DateTime.Now,
                    Done = true,
                },
                 new ShoppingListItem
                {
                    ProductItemId = "94831",
                    Id = "abcd-efgh",
                    Amount = 100.0f,
                    CreatedAt = DateTime.Now,
                    Done = false,
                },
                new ShoppingListItem
                {
                    ProductItemId = "12334234",
                    Id = "abcd-lmon",
                    Amount = 500.0f,
                    CreatedAt = DateTime.Now,
                    Done = false,
                },
            };

            var shoppingList = new ShoppingList()
            {
                Items = list1,
            };

            Assert.False(shoppingList.HasDifferentItems(list2));
        }
        [Fact]
        public void HasDifferentItems_ItemListContainsDifferentAmountOfItems_ReturnsTrue()
        {
            var list1 = new List<ShoppingListItem>
            {
                new ShoppingListItem
                {
                    ProductItemId = "12334234",
                    Id = "abcd-lmon",
                    Amount = 500.0f,
                    CreatedAt = DateTime.Now,
                    Done = false,
                },
                new ShoppingListItem
                {
                    ProductItemId = "5601278",
                    Id = "abcd-7994",
                    Amount = 5.0f,
                    CreatedAt = DateTime.Now,
                    Done = true,
                },
                 new ShoppingListItem
                {
                    ProductItemId = "94831",
                    Id = "abcd-efgh",
                    Amount = 100.0f,
                    CreatedAt = DateTime.Now,
                    Done = false,
                },
            };
            var list2 = new List<ShoppingListItem>
            {
                new ShoppingListItem
                {
                    ProductItemId = "5601278",
                    Id = "abcd-7994",
                    Amount = 5.0f,
                    CreatedAt = DateTime.Now,
                    Done = true,
                },
                 new ShoppingListItem
                {
                    ProductItemId = "94831",
                    Id = "abcd-efgh",
                    Amount = 100.0f,
                    CreatedAt = DateTime.Now,
                    Done = false,
                },
            };

            var shoppingList = new ShoppingList()
            {
                Items = list1,
            };

            Assert.True(shoppingList.HasDifferentItems(list2));
        }
        [Fact]
        public void HasDifferentItems_ItemListContainsSameAmountButDifferentItems_ReturnsTrue()
        {
            var list1 = new List<ShoppingListItem>
            {
                new ShoppingListItem
                {
                    ProductItemId = "75075",
                    Id = "abcd-lmon",
                    Amount = 500.0f,
                    CreatedAt = DateTime.Now,
                    Done = false,
                },
                new ShoppingListItem
                {
                    ProductItemId = "5601278",
                    Id = "abcd-7994",
                    Amount = 5.0f,
                    CreatedAt = DateTime.Now,
                    Done = false,
                },
                 new ShoppingListItem
                {
                    ProductItemId = "94831",
                    Id = "abcd-efgh",
                    Amount = 50.0f,
                    CreatedAt = DateTime.Now,
                    Done = false,
                },
            };
            var list2 = new List<ShoppingListItem>
            {
                new ShoppingListItem
                {
                    ProductItemId = "5601278",
                    Id = "abcd-7994",
                    Amount = 5.0f,
                    CreatedAt = DateTime.Now,
                    Done = true,
                },
                 new ShoppingListItem
                {
                    ProductItemId = "94831",
                    Id = "abcd-efgh",
                    Amount = 100.0f,
                    CreatedAt = DateTime.Now,
                    Done = false,
                },
                new ShoppingListItem
                {
                    ProductItemId = "12334234",
                    Id = "abcd-lmon",
                    Amount = 500.0f,
                    CreatedAt = DateTime.Now,
                    Done = false,
                },
            };

            var shoppingList = new ShoppingList()
            {
                Items = list1,
            };

            Assert.True(shoppingList.HasDifferentItems(list2));
        }
    }
}
