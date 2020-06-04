using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public ShoppingListItem() : base()
        {
            Done = false;
        }
        public ShoppingListItem(DateTime date) : this()
        {
            CreatedAt = date;
        }
    }
}
