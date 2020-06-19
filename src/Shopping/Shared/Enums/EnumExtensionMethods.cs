using System;
using System.Reflection;
using System.Linq;

namespace Shopping.Shared.Enums
{
    public static class EnumExtensionMethods
    {
        public static string GetDescription(this Enum GenericEnum)
        {
            Type genericEnumType = GenericEnum.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(GenericEnum.ToString());
            if ((memberInfo != null && memberInfo.Length > 0))
            {
                var _Attribs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if ((_Attribs != null && _Attribs.Count() > 0))
                {
                    return ((System.ComponentModel.DescriptionAttribute)_Attribs.ElementAt(0)).Description;
                }
            }
            return GenericEnum.ToString();
        }
        public static string GetDisplayName(this Enum GenericEnum)
        {
            Type genericEnumType = GenericEnum.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(GenericEnum.ToString());
            if ((memberInfo != null && memberInfo.Length > 0))
            {
                var _Attribs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false);
                if ((_Attribs != null && _Attribs.Count() > 0))
                {
                    return ((System.ComponentModel.DataAnnotations.DisplayAttribute)_Attribs.ElementAt(0)).Name;
                }
            }
            return GenericEnum.ToString();
        }

        public static float GetStepperIncrement(this ProductUnit unit)
        {
            float stepperValue = 0.1f;
            switch (unit)
            {
                case ProductUnit.KiloGramm:
                case ProductUnit.Liter:
                    stepperValue = .5f;
                    break;
                case ProductUnit.Piece:
                case ProductUnit.Package:
                case ProductUnit.Can:
                case ProductUnit.Glas:
                case ProductUnit.Bottle:
                    stepperValue = 1;
                    break;
                case ProductUnit.Gramm:
                case ProductUnit.MilliLiter:
                    stepperValue = 50f;
                    break;
            }
            return stepperValue;
        }

    }
}