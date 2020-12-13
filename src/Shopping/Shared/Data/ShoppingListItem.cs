using System;

namespace Shopping.Shared.Data
{
    public class ShoppingListItem : BaseItem
    {
        public string ProductItemId { get; set; }
        public ProductItem ProductItem { get; set; }

        public string ShoppingListId { get; set; }
        public ShoppingList ShoppingList { get; set; }

        public float Amount { get; set; }
        public bool Done { get; set; }

        public ShoppingListItem() : base()
        {
            Done = false;
        }
        public ShoppingListItem(ShoppingListItem item) : base(item)
        {
            this.ProductItemId = item.ProductItemId;
            this.ProductItem = new ProductItem(item.ProductItem ?? new ProductItem());
            this.Amount = item.Amount;
            this.Done = item.Done;
        }

        public override bool Equals(object obj)
        {
            return obj is ShoppingListItem item &&
                   ProductItemId == item.ProductItemId &&
                   Amount == item.Amount &&
                   Done == item.Done;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ProductItemId, Amount, Done);
        }
    }
}
