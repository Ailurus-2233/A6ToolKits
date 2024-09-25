using System.Globalization;
using A6ToolKits.UIPackage.Controls.Layout.Tab;
using Avalonia.Controls;
using Avalonia.Data.Converters;

namespace A6ToolKits.UIPackage.Styles.Layout.Tab.Converters;

public class HeaderWidthConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not TabContainer tabContainer) return double.NaN;
        return tabContainer.TabStripPlacement is Dock.Left or Dock.Right ? tabContainer.HeaderWidth : double.NaN;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}