using Shopping.Shared.Model.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Client.Models
{

    public class MemberInputModel
    {
        [Required(ErrorMessage = "Username or Email required")]
        public string UserNameOrEmail { get; set; }

        public ShoppingUserModel CreateUserModel()
        {
            var userModel = new ShoppingUserModel();
            if (new EmailAddressAttribute().IsValid(this.UserNameOrEmail))
            {
                userModel.Email = this.UserNameOrEmail;
            }
            else
            {
                userModel.UserName = this.UserNameOrEmail;
            }
            return userModel;
        }
    }
}
