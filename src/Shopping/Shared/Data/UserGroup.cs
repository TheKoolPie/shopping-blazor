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

    }
}
