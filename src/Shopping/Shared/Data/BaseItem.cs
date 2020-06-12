using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shopping.Shared.Data
{
    public class BaseItem
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Id is needed")]
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
