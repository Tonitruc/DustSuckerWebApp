using DustSuckerWebApp.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace DustSuckerWebApp.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            return value.GetType()
                .GetField(value.ToString())?
                .GetCustomAttribute<DisplayAttribute>()?
                .Name ?? value.ToString();
        }

        public static List<string> GetEnumDisplayNames<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                       .Cast<T>()
                       .Select(e => e.GetDisplayName()
                       ).ToList();
        }

        public static T? FromDisplayName<T>(this string displayName) where T : Enum
        {
            return Enum.GetValues(typeof(T))
                       .Cast<T>()
                       .FirstOrDefault(e => e.GetDisplayName().Equals(displayName, StringComparison.OrdinalIgnoreCase));
        }

        public static object? FromDisplayName(this string displayName, Type enumType)
        {
            if (!enumType.IsEnum) throw new ArgumentException("Only enum", nameof(enumType));

            return Enum.GetValues(enumType)
                       .Cast<object>()
                       .FirstOrDefault(e => ((Enum)e).GetDisplayName().Equals(displayName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
