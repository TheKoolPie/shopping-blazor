using System;

namespace Shopping.Shared.Data
{
    public class StoreProductCategory
    {
        public string StoreProductCategoryId { get; set; }
        public string StoreId { get; set; }
        public Store Store { get; set; }
        public string ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public int RankingValue { get; set; }

        public DateTime CreatedAt { get; set; }
        public StoreProductCategory()
        {
            StoreProductCategoryId = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
        }

    }
}
