using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shopping.Shared.Model.Account
{
    public class DeleteUserModel
    {
        [Required]
        public string UserId { get; set; }
    }
}
