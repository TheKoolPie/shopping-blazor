using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shopping.Shared.Data
{
    public class ProductCategory
    {
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name für Kategorie wird benötigt")]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Farbcode wird benötigt")]
        public string ColorCode { get; set; }
        public ProductCategory()
        {
            Id = Guid.NewGuid();
        }

        public ProductCategory(ProductCategory category)
        {
            Id = category.Id;
            Name = category.Name;
            ColorCode = category.ColorCode;
        }
    }
}
