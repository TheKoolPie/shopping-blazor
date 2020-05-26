using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Shared.Data
{
    public class ShoppingListItem
    {
        public string Id { get; set; }
        public ProductItem Product { get; set; }
        public float Amount { get; set; }
    }
}
