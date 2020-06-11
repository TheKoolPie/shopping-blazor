using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shopping.Shared.Model.Account
{
    public class ShoppingUserModel
    {
        public string Id { get; set; }
        [NotMapped]
        public string UserName { get; set; }
        [NotMapped]
        public string Email { get; set; }
    }
}
