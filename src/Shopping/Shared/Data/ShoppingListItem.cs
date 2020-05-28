using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shopping.Shared.Data
{
    public class ShoppingListItem : BaseItem
    {
        public string ProductItemId { get; set; }
        public ProductItem ProductItem { get; set; }

        [Required(ErrorMessage = "Amount is needed")]
        [Range(float.Epsilon, float.MaxValue, ErrorMessage = "Please select value greater than 0")]
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
        public ShoppingListItem(DateTime date) : this()
        {
            CreatedAt = date;
        }
    }
}
