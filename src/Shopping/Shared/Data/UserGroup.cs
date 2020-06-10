using Shopping.Shared.Model.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Shopping.Shared.Data
{
    public class UserGroup : BaseItem
    {
        [Required(ErrorMessage = "User group name is needed")]
        public string Name { get; set; }
        public ShoppingUserModel Owner { get; set; }

        [NotMapped]
        public List<ShoppingList> ShoppingLists { get; set; }

        public List<ShoppingUserModel> Members { get; set; }

        public UserGroup() : base()
        {
            ShoppingLists = new List<ShoppingList>();
            Members = new List<ShoppingUserModel>();
        }
        public UserGroup(UserGroup group) : base(group)
        {
            this.Owner = group.Owner;
            this.Members = new List<ShoppingUserModel>(group.Members ?? new List<ShoppingUserModel>());
        }
    }
}
