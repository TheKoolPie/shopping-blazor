using Shopping.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Shared.Data
{
    public class StoreChain
    {
        public string StoreChainId { get; set; }
        public string Name { get; set; }
        public PriceCategory PriceCategory { get; set; }
        public string Url { get; set; }
        public string Logo { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string CreatorId { get; set; }

        public List<Store> Stores { get; set; }

        public StoreChain()
        {
            StoreChainId = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
            Stores = new List<Store>();
        }
    }
}
