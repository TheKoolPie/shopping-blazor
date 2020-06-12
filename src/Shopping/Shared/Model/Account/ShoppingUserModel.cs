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

        public override bool Equals(object obj)
        {
            return obj is ShoppingUserModel model &&
                   Id == model.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
