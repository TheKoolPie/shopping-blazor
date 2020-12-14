using System;

namespace Shopping.Shared.Data
{
    public class BaseItem
    {
        public string Id { get; set; }

        public DateTime? CreatedAt { get; set; }

        public BaseItem()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
        }
        public BaseItem(BaseItem item)
        {
            this.Id = item.Id;
            this.CreatedAt = item.CreatedAt;
        }
    }
}
