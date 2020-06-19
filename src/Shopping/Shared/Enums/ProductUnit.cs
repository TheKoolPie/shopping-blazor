using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shopping.Shared.Enums
{
    public enum ProductUnit
    {
        [Description("Gramm")]
        [Display(Name ="g")]
        Gramm = 1,
        [Description("Kilogramm")]
        [Display(Name = "kg")]
        KiloGramm,
        [Description("Stück")]
        [Display(Name = "Stk")]
        Piece,
        [Description("Packung")]
        [Display(Name = "Pckg")]
        Package,
        [Description("Milliliter")]
        [Display(Name = "ml")]
        MilliLiter,
        [Description("Liter")]
        [Display(Name = "l")]
        Liter,
        [Description("Dose")]
        [Display(Name = "Dose")]
        Can,
        [Description("Glas")]
        [Display(Name = "Gl")]
        Glas,
        [Description("Flasche")]
        [Display(Name = "Fl")]
        Bottle
    }
}
