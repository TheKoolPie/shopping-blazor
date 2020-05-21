using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace Shopping.Shared
{
    public class ShoppingList
    {
        public string Id { get; set; }

        public List<ShoppingItem> Items { get; set; }
        [DisplayName("Datum")]
        public DateTime Date { get; set; }

        private string DoneCssClassName => "done";


        public ShoppingList()
        {
            Date = DateTime.Now;
            Items = new List<ShoppingItem>();
        }
        public void RemoveEntry(string id)
        {
            var item = Items.FirstOrDefault(i => i.Id.Equals(id));
            if (item != null)
            {
                Items.Remove(item);
            }
        }
        public void ToggleDoneState(string id)
        {
            var item = Items.FirstOrDefault(i => i.Id.Equals(id));
            if (item != null)
            {
                item.Done = !item.Done;
                item.DoneCssClassName = item.Done ? DoneCssClassName : null;
            }
        }
        public bool Contains(ShoppingItem item)
        {
            return Items.Any(i => i.Name.Equals(item.Name) && i.Amount.Equals(item.Amount));
        }
        public void AddEntry(ShoppingItem item)
        {
            item.Id = Guid.NewGuid().ToString();
            Items.Add(item);
        }
    }
}
