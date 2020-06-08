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
        public string Name { get; set; }
        public string OwnerId { get; set; }

        [NotMapped]
        public List<ShoppingList> ShoppingLists { get; set; }

        public List<UserGroupMember> Members { get; set; }

        public UserGroup() : base()
        {
            ShoppingLists = new List<ShoppingList>();
            Members = new List<UserGroupMember>();
        }
        public UserGroup(UserGroup group) : base(group)
        {
            this.OwnerId = group.OwnerId;
            this.Members = new List<UserGroupMember>(group.Members ?? new List<UserGroupMember>());
        }
    }
}
