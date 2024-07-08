using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var displayName = enumValue.GetType()
                                       .GetMember(enumValue.ToString())
                                       .First()
                                       .GetCustomAttribute<DisplayAttribute>()
                                       ?.Name;

            return displayName ?? enumValue.ToString();
        }
    }
}
