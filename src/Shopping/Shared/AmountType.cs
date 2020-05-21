using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace Shopping.Shared
{
    public enum AmountType
    {
        [Description("g")]
        Gramm,
        [Description("kg")]
        KiloGramm,
        [Description("Stk.")]
        Piece,
        [Description("Pckg.")]
        Package,
        [Description("ml")]
        MilliLiter,
        [Description("l")]
        Liter,
    }
}
