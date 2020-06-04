using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shopping.Shared.Data
{
    public class ShoppingList : BaseItem
    {
        public string OwnerId { get; set; }
        public List<string> UserGroupIds { get; set; }
        [NotMapped]
        public List<UserGroup> UserGroups { get; set; }

        public List<ShoppingListItem> Items { get; set; }

        public ShoppingList()
        {
            UserGroupIds = new List<string>();
            UserGroups = new List<UserGroup>();
            Items = new List<ShoppingListItem>();
        }
    }
}
