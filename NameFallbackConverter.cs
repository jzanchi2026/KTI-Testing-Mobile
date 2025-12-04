using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace MauiApp2
{
    public class NameFallbackConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
                return "Unknown";

            var type = value.GetType();

            // Try Tool.Name first
            var prop = type.GetProperty("Name");

            // Fall back to Material.name
            if (prop == null)
                prop = type.GetProperty("name");

            return prop?.GetValue(value)?.ToString() ?? "Unknown";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
