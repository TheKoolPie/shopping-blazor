using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shopping.Shared.Data
{
    public class BaseItem
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Id is needed")]
        public string Id { get; set; }
    }
}
