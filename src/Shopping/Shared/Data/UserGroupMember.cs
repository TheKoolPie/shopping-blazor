using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Shared.Data
{
    public class UserGroupMember : BaseItem
    {
        public string UserId { get; set; }

        public UserGroupMember()
        {
        }

        public UserGroupMember(BaseItem item) : base(item)
        {
        }
    }
}
