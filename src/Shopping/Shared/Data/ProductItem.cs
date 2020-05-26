using Shopping.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shopping.Shared.Data
{
    public class ProductItem
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Product Id is needed")]
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Product name is needed")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Unit is needed")]
        [EnumDataType(typeof(ProductUnit), ErrorMessage = "Unit is needed")]
        public ProductUnit Unit { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Category is needed")]
        public string CategoryId { get; set; }
        public ProductCategory Category { get; set; }

        public ProductItem()
        {
            Id = Guid.NewGuid().ToString();
            Category = new ProductCategory("");
        }
    }
}
