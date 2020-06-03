﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shopping.Shared.Data
{
    public class ProductCategory : BaseItem
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Category name is needed")]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Farbcode is needed")]
        public string ColorCode { get; set; }
        public ProductCategory() : base()
        {
        }
        public ProductCategory(string id) : this()
        {
            Id = id;
        }

        public ProductCategory(ProductCategory category)
        {
            Id = category.Id;
            Name = category.Name;
            ColorCode = category.ColorCode;
        }
    }
}
