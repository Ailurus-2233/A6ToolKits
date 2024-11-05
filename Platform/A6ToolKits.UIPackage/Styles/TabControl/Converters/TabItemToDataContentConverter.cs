using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using TabItem = A6ToolKits.UIPackage.Controls.TabControl.Models.TabItem;

namespace A6ToolKits.UIPackage.Styles.TabControl.Converters;

public class TabItemToDataContentConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value switch
        {
            TabItem tabItem => tabItem.Content,
            _ => new TextBlock { Text = "Empty" }
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}