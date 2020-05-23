using Shopping.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Shared.Enums.Converter
{
    public static class AlertTypeConverter
    {
        public static string ToBootstrapClassName(this AlertType type)
        {
            string cssName = "alert alert-";
            switch (type)
            {
                case AlertType.Primary:
                    cssName += "primary";
                    break;
                case AlertType.Secondary:
                    cssName += "secondary";
                    break;
                case AlertType.Success:
                    cssName += "success";
                    break;
                case AlertType.Danger:
                    cssName += "danger";
                    break;
                case AlertType.Warning:
                    cssName += "warning";
                    break;
                case AlertType.Info:
                    cssName += "info";
                    break;
                case AlertType.Light:
                    cssName += "light";
                    break;
                case AlertType.Dark:
                    cssName += "dark";
                    break;
            }
            cssName += " alert-dismissible fade show";

            return cssName;
        }
    }
}
