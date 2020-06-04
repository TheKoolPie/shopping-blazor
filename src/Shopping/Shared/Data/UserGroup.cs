using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shopping.Shared.Data
{
    public class UserGroup : BaseItem
    {
        public string OwnerId { get; set; }
        public List<string> MemberIds { get; set; }
        public UserGroup() : base()
        {

        }
        public UserGroup(UserGroup group) : base(group)
        {
            this.OwnerId = group.OwnerId;
            this.MemberIds = new List<string>(group.MemberIds);
        }
    }
}
