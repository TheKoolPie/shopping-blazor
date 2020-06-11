using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Client.Models
{
    public class GroupIdInputModel
    {
        [Required(ErrorMessage = "Group id is required")]
        public string GroupId { get; set; }
    }
}
