using Shopping.Shared.Converter;
using Shopping.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Shared.Model
{
    public class SortingModel
    {
        public SortingType Type { get; set; }
        public string Icon { get; set; }

        public SortingModel()
        {
            Type = SortingType.Descending;
            Icon = Type.ToOpenIconicCssClassName();
        }

        public void Toggle()
        {
            Type = (Type == SortingType.Ascending) ? SortingType.Descending : SortingType.Ascending;
            Icon = Type.ToOpenIconicCssClassName();
        }
    }
}
