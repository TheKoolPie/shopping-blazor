using Shopping.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Shared.Converter
{
    public static class SortingTypeConverter
    {
        public static string ToOpenIconicCssClassName(this SortingType type)
        {
            string cssName = "oi oi-";
            switch (type)
            {
                case SortingType.Ascending:
                    cssName += "caret-top";
                    break;
                case SortingType.Descending:
                    cssName += "caret-bottom";
                    break;
            }

            return cssName;
        }
    }
}
