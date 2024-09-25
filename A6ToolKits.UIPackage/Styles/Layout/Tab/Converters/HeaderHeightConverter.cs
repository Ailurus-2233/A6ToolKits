using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data.Converters;

namespace A6ToolKits.UIPackage.Styles.Layout.Tab.Converters;

public class HeaderHeightConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (parameter is Dock dock)
        {
            return dock is Dock.Top or Dock.Bottom ? value : double.NaN;
        }

        return double.NaN;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}