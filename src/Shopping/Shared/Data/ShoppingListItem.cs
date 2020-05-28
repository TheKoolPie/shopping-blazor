using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shopping.Shared.Data
{
    public class ShoppingListItem
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Id is needed")]
        public string Id { get; set; }

        public string ProductItemId { get; set; }
        public ProductItem ProductItem { get; set; }

        [Required(ErrorMessage = "Amount is needed")]
        public float Amount { get; set; }
        public bool Done { get; set; }

        [Required(ErrorMessage = "Creation date is needed")]
        public DateTime CreatedAt { get; set; }

        public ShoppingListItem()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
            Done = false;
        }
    }
}
