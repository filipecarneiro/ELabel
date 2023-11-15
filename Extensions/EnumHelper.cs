using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ELabel.Extensions
{
    public static class EnumHelper
    {
        public static string? GetDisplayName(Enum? enumValue)
        {
            if (enumValue == null)
                return null;

            return enumValue?.GetType()?
           .GetMember(enumValue.ToString())?[0]?
           .GetCustomAttribute<DisplayAttribute>()?
           .Name;
        }

        public static string? GetDisplayDescription(Enum? enumValue)
        {
            if (enumValue == null)
                return null;

            return enumValue?.GetType()?
           .GetMember(enumValue.ToString())?[0]?
           .GetCustomAttribute<DisplayAttribute>()?
           .Description;
        }
    }
}
