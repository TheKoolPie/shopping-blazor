using Shopping.Shared.Data;

namespace Shopping.Shared.Services.Interfaces
{
    public interface IShoppingListSortStrategy
    {
        ShoppingList Sort(ShoppingList list);
    }
}