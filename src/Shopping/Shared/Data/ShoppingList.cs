﻿using Shopping.Shared.Model.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Shopping.Shared.Data
{
    public class ShoppingList : BaseItem
    {
        public string Name { get; set; }
        public DateTime ListDate { get; set; }

        public string OwnerId { get; set; }

        public ShoppingUserModel Owner { get; set; }
        public List<UserGroup> UserGroups { get; set; }

        public List<ShoppingListItem> Items { get; set; }


        public int ItemCount { get; set; }
        public bool ListDone { get; set; }

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
                   OwnerId.Equals(list.OwnerId) &&
                   ItemsEqual(list.Items);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, ListDate, OwnerId, Items);
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
