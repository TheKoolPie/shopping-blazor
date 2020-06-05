using Shopping.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shopping.Shared.Data
{
    public class ProductItem : BaseItem
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Product name is needed")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Unit is needed")]
        [EnumDataType(typeof(ProductUnit), ErrorMessage = "Unit is needed")]
        public ProductUnit Unit { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Category is needed")]
        public string CategoryId { get; set; }
        public ProductCategory Category { get; set; }

        public ProductItem() : base()
        {
            Category = new ProductCategory();
        }
        public ProductItem(ProductItem item) : base(item)
        {
            this.Name = item.Name;
            this.Unit = item.Unit;
            this.CategoryId = item.CategoryId;
            this.Category = new ProductCategory(item.Category ?? new ProductCategory());
        }

        public override bool Equals(object obj)
        {
            return obj is ProductItem item &&
                   Name == item.Name &&
                   Unit == item.Unit &&
                   CategoryId == item.CategoryId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Unit, CategoryId);
        }
    }
}
