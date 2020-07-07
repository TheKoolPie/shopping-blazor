using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shopping.Shared.Data
{
    public class UserGroupShoppingList : BaseItem
    {
        public string UserGroupId { get; set; }
        public UserGroup UserGroup { get; set; }

        public string ShoppingListId { get; set; }
        public ShoppingList ShoppingList { get; set; }
    }
}
