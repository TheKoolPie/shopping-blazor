using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Client.Models
{
    public class ListIdInputModel
    {
        [Required(ErrorMessage = "List id is required")]
        public string ListId { get; set; }
    }
}
