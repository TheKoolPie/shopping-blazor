using System;

namespace Shopping.Shared.Data
{
    public class StoreProductCategory
    {
        public string StoreId { get; set; }
        public Store Store { get; set; }
        public string ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public int RankingValue { get; set; }

        public DateTime CreatedAt { get; set; }
        public StoreProductCategory()
        {
            CreatedAt = DateTime.Now;
        }

    }
}
