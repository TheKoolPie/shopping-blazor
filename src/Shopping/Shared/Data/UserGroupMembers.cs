﻿
namespace Shopping.Shared.Data
{
    public class UserGroupMembers : BaseItem
    {
        public string UserGroupId { get; set; }
        public UserGroup UserGroup { get; set; }
        public string MemberId { get; set; }
    }
}
