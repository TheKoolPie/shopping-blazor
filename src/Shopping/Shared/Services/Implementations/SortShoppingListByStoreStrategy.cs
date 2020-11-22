using Shopping.Shared.Data;
using Shopping.Shared.Extensions;
using Shopping.Shared.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Shopping.Shared.Services.Implementations
{
    public class SortShoppingListByStoreStrategy : IShoppingListSortStrategy
    {
        private readonly List<StoreProductCategory> _storeProductCatAssignmentsOfStore;
        public SortShoppingListByStoreStrategy(List<StoreProductCategory> storeProductCatAssignmentsOfStore)
        {
            _storeProductCatAssignmentsOfStore = storeProductCatAssignmentsOfStore;
        }

        public ShoppingList Sort(ShoppingList list)
        {
            var assignments = _storeProductCatAssignmentsOfStore
                .OrderBy(a => a.RankingValue)
                .ToList();
            List<ShoppingListItem> sortedItemList = new List<ShoppingListItem>();
            foreach(var assignment in assignments)
            {
                string currentCatId = assignment.ProductCategoryId;
                var itemsWithCurrentCat = list.Items
                    .Where(i => i.ProductItem.CategoryId == currentCatId)
                    .OrderBy(i => i.ProductItem.Name)
                    .ToList();
                if (!itemsWithCurrentCat.IsNullOrEmpty())
                {
                    sortedItemList.AddRange(itemsWithCurrentCat);
                }
            }
            list.Items = sortedItemList;
            return list;
        }
    }
}
