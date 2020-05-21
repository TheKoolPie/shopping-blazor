using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shopping.Shared
{
    public class ShoppingItem
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage = "Produktname wird benötigt")]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Anzahl wird benötigt")]
        [DisplayName("Anzahl")]
        public string Amount { get; set; }

        [Required]
        [DisplayName("Einheit")]
        public AmountType AmountType { get; set; }

        [DisplayName("Gekauft")]
        public bool Done { get; set; }
        public string DoneCssClassName { get; set; }

        public ShoppingItem() { }
        public ShoppingItem(ShoppingItem item)
        {
            this.Id = item.Id;
            this.Name = item.Name;
            this.Amount = item.Amount;
            this.AmountType = item.AmountType;
            this.Done = item.Done;
        }
        
    }
}
