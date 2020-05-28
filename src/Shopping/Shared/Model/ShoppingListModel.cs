using Shopping.Shared.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Shared.Model
{
    public class ShoppingListModel
    {
        public List<ShoppingListItem> Items { get; set; }
        public DateTime ListDate { get; set; }

        public ShoppingListModel()
        {
            Items = new List<ShoppingListItem>();
            ListDate = DateTime.Now;
        }

        public ShoppingListModel(DateTime listDate, List<ShoppingListItem> items)
        {
            ListDate = listDate;
            Items = new List<ShoppingListItem>(items);
        }

        public int ItemCount { get => Items.Count; }

    }
}
