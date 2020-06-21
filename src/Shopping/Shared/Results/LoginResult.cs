using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Shared.Results
{
    public class LoginResult : BaseResult<object>
    {
        public string Token { get; set; }
    }
}
