using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Shopping.Shared.Enums
{
    public enum ProductUnit
    {
        [Description("Gramm")]
        Gramm = 1,
        [Description("Kilogramm")]
        KiloGramm,
        [Description("Stück")]
        Piece,
        [Description("Packung")]
        Package,
        [Description("Milliliter")]
        MilliLiter,
        [Description("Liter")]
        Liter,
    }
}
