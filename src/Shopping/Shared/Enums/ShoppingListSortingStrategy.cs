using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shopping.Shared.Enums
{
    public enum ShoppingListSortingStrategy
    {
        [Display(Name = "No Sort")]
        None,
        [Display(Name = "Sort by Store")]
        Store
    }
}
