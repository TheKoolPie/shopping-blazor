using Shopping.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Shared.Data
{
    public class Store
    {
        public string StoreId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public PriceCategory PriceCategory { get; set; }
        public string StoreChainId { get; set; }
        public StoreChain StoreChain { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string CreatorId { get; set; }

        public Store()
        {
            StoreId = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
            CreatorId = "";
            StoreChainId = "";
        }

        public string getAddress()
        {
            return $"{Street} {HouseNumber}, {PostalCode} {City}";
        }
    }
}
