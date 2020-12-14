using Shopping.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shopping.Shared.Data
{
    public class ProductItem : BaseItem
    {
        public string Name { get; set; }
        public ProductUnit Unit { get; set; }

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

    }
}
