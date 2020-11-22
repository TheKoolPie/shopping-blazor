using Shopping.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Client.Models
{
    public class StoreChainCreateViewModel
    {
        public string StoreChainId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Price category is required")]
        public PriceCategory Category { get; set; }
        [DataType(DataType.Url)]
        public string Url { get; set; }

    }
}
