using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;

namespace A6ToolKits.Layout.Converters;

public class WidthRadioToWidthConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not double ratio || parameter is not double parentWidth) return AvaloniaProperty.UnsetValue;
        // Calculate the actual width based on the ratio and parent width
        var actualWidth = ratio * parentWidth;
        return new GridLength(actualWidth, GridUnitType.Pixel);

        // Return a default value or handle the case where conversion fails
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}