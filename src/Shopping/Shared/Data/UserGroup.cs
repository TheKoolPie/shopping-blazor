using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Shopping.Shared.Data
{
    public class UserGroup : BaseItem
    {
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public List<string> MemberIds { get; set; }
        public UserGroup() : base()
        {

        }
        public UserGroup(UserGroup group) : base(group)
        {
            this.OwnerId = group.OwnerId;
            this.MemberIds = new List<string>(group.MemberIds ?? new List<string>());
        }

        public override bool Equals(object obj)
        {
            return obj is UserGroup group &&
                   Name == group.Name &&
                   OwnerId == group.OwnerId &&
                   MemberIds.All(group.MemberIds.Contains);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, OwnerId, MemberIds);
        }
    }
}
