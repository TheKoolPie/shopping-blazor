using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shopping.Shared.Model.Account
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "User name or Email address")]
        public string LoginName { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
