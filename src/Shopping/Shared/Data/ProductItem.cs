using Shopping.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Shared.Data
{
    public class ProductItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ProductUnit Unit { get; set; }
        public ProductCategory Category { get; set; }
    }
}
