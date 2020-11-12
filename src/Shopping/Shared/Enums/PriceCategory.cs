using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Shopping.Shared.Enums
{
    public enum PriceCategory
    {
        [Description("Low")]
        Low,
        [Description("Medium")]
        Medium,
        [Description("High")]
        High,
        [Description("Very High")]
        VeryHigh
    }
}
