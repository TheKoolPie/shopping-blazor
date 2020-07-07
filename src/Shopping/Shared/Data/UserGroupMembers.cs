using Shopping.Shared.Model.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shopping.Shared.Data
{
    public class UserGroupMembers : BaseItem
    {
        [Required]
        public string UserGroupId { get; set; }

        [Required]
        public string MemberId { get; set; }
    }
}
