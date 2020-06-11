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
    }
}
