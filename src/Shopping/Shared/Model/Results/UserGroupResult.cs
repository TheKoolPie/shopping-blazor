using Shopping.Shared.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Shared.Model.Results
{
    public class UserGroupResult
    {
        public bool IsSuccessful { get; set; }
        public List<UserGroup> UserGroups { get; set; }
        public string Message { get; set; }
        public UserGroupResult()
        {
            UserGroups = new List<UserGroup>();
        }
    }
}
