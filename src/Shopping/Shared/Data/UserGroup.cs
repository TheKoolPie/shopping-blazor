using Shopping.Shared.Model.Account;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.Shared.Data
{
    public class UserGroup : BaseItem
    {
        public string Name { get; set; }
        public string OwnerId { get; set; }

        public ShoppingUserModel Owner { get; set; }

        public List<ShoppingList> ShoppingLists { get; set; }
        public List<ShoppingUserModel> Members { get; set; }

        public UserGroup() : base()
        {
            ShoppingLists = new List<ShoppingList>();
            Members = new List<ShoppingUserModel>();
        }
        public UserGroup(UserGroup group) : base(group)
        {
            this.OwnerId = group.OwnerId;
            this.Members = new List<ShoppingUserModel>(group.Members ?? new List<ShoppingUserModel>());
        }
    }
}
