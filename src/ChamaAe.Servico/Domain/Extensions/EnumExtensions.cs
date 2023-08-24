using System;
using System.Linq;
using System.Reflection;

namespace ChamaAe.Servico.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum genericEnum)
        {
            var genericEnumType = genericEnum.GetType();
            var memberInfo = genericEnumType.GetMember(genericEnum.ToString());
            if (memberInfo is not {Length: > 0}) return genericEnum.ToString();
            var attribs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            if ((attribs != null && attribs.Any()))
            {
                return ((System.ComponentModel.DescriptionAttribute)attribs.ElementAt(0)).Description;
            }
            return genericEnum.ToString();
        }

    }
}