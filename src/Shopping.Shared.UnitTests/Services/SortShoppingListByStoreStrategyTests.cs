using Shopping.Shared.Data;
using Shopping.Shared.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Shopping.Shared.UnitTests.Services
{
    public class SortShoppingListByStoreStrategyTests
    {
        private ShoppingListItem GetShoppingListItemOfCategory(string productName, string categoryName)
        {
            return new ShoppingListItem
            {
                ProductItem = new ProductItem
                {
                    Name = productName,
                    CategoryId = categoryName,
                }
            };
        }

        private StoreProductCategory GetAssignmentOfCategory(string catName, int ranking)
        {
            return new StoreProductCategory
            {
                ProductCategoryId = catName,
                RankingValue = ranking
            };
        }

        [Fact]
        void Sort_ItemsInListWithDifferentCategories_GetSortedByRankingValue()
        {
            ShoppingList list = new ShoppingList
            {
                Items = new List<ShoppingListItem>
                {
                    GetShoppingListItemOfCategory("Apple","Fruit"),
                    GetShoppingListItemOfCategory("Gouda", "Cheese"),
                    GetShoppingListItemOfCategory("Chicken","Meat")
                }
            };

            List<StoreProductCategory> assignments = new List<StoreProductCategory>
            {
                GetAssignmentOfCategory("Cheese",0),
                GetAssignmentOfCategory("Meat",1),
                GetAssignmentOfCategory("Fruit",2)
            };
            var sorter = new SortShoppingListByStoreStrategy(assignments);

            var sortedList = sorter.Sort(list);

            Assert.Equal("Cheese", sortedList.Items[0].ProductItem.CategoryId);
            Assert.Equal("Meat", sortedList.Items[1].ProductItem.CategoryId);
            Assert.Equal("Fruit", sortedList.Items[2].ProductItem.CategoryId);
        }
        [Fact]
        void Sort_MultipleItemsWithSameCategory_GetsSortedByRankingValueAndName()
        {
            ShoppingList list = new ShoppingList
            {
                Items = new List<ShoppingListItem>
                {
                    GetShoppingListItemOfCategory("Apple","Fruit"),
                    GetShoppingListItemOfCategory("Gouda", "Cheese"),
                    GetShoppingListItemOfCategory("Chicken","Meat"),
                    GetShoppingListItemOfCategory("Banana","Fruit"),
                    GetShoppingListItemOfCategory("Melon","Fruit"),
                    GetShoppingListItemOfCategory("Gorgonzola","Cheese"),
                    GetShoppingListItemOfCategory("Turkey","Meat")
                }
            };

            var assignments = new List<StoreProductCategory>
            {
                GetAssignmentOfCategory("Cheese",0),
                GetAssignmentOfCategory("Fruit",1),
                GetAssignmentOfCategory("Meat",2)
            };

            List<string> targetNameList = new List<string>
            {
                "Gorgonzola",
                "Gouda",
                "Apple",
                "Banana",
                "Melon",
                "Chicken",
                "Turkey"
            };

            var sorter = new SortShoppingListByStoreStrategy(assignments);

            var sortedList = sorter.Sort(list);

            var actualNameList = sortedList.Items.Select(i => i.ProductItem.Name);

            Assert.Equal(targetNameList, actualNameList);
        }
    }
}
