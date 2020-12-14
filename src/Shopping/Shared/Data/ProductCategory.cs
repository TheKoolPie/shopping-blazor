using System;

namespace Shopping.Shared.Data
{
    public class ProductCategory : BaseItem
    {
        public string Name { get; set; }
        public ProductCategory() : base()
        {
        }
        public ProductCategory(ProductCategory category) : base(category)
        {
            Name = category.Name;
        }

        public override bool Equals(object obj)
        {
            return obj is ProductCategory category &&
                   Name == category.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}
