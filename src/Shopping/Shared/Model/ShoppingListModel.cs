using Shopping.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shopping.Shared.Model
{
    public class ShoppingListModel
    {
        public List<ShoppingListItem> Items { get; set; }
        public DateTime ListDate { get; set; }

        public bool IsModified { get; set; }

        public ShoppingListModel()
        {
            Items = new List<ShoppingListItem>();
            ListDate = DateTime.Now;
            IsModified = false;
        }

        public ShoppingListModel(DateTime listDate, List<ShoppingListItem> items) : this()
        {
            ListDate = listDate;
            Items = new List<ShoppingListItem>(items);
        }

        public int ItemCount => Items.Count;
        public int DoneCount => Items.Where(i => i.Done).Count();

        public void AddOrUpdateItem(ShoppingListItem item)
        {
            var backlogItem = Items.FirstOrDefault(i => i.ProductItemId == item.ProductItem.Id);
            if (backlogItem == null)
            {
                Items.Add(item);
            }
            else
            {
                backlogItem.Amount = item.Amount;
                backlogItem.Done = item.Done;
            }
            IsModified = true;
        }
        public void RemoveItem(string id)
        {
            var item = Items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                Items.Remove(item);
                IsModified = true;
            }
        }
        public void ToggleItemDoneState(string id)
        {
            var toggleItem = Items.FirstOrDefault(i => i.Id == id);
            toggleItem.Done = !toggleItem.Done;
            IsModified = true;
        }
    }
}
