using Shopping.Shared.Model.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Shopping.Shared.Data
{
    public class ShoppingList : BaseItem
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime ListDate { get; set; }
        public ShoppingUserModel Owner { get; set; }

        [NotMapped]
        public List<UserGroup> UserGroups { get; set; }

        public List<ShoppingListItem> Items { get; set; }


        public ShoppingList()
        {
            UserGroups = new List<UserGroup>();
            Items = new List<ShoppingListItem>();
            Owner = new ShoppingUserModel();
        }

        public override bool Equals(object obj)
        {
            return obj is ShoppingList list &&
                   Name == list.Name &&
                   ListDate == list.ListDate &&
                   Owner.Equals(list.Owner) &&
                   ItemsEqual(list.Items);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, ListDate, Owner, Items);
        }

        private bool ItemsEqual(List<ShoppingListItem> items)
        {
            if (Items.Count != items.Count)
            {
                return false;
            }
            var local = Items.OrderBy(x => x.Id).ToList();
            var compare = items.OrderBy(x => x.Id).ToList();

            for (int i = 0; i < local.Count; i++)
            {
                if (!local[i].Equals(compare[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
