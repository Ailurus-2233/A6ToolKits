using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace A6ToolKits.UIPackage.Controls.Simple.Buttons;

public class IconButton : Button
{
    public static readonly StyledProperty<double> IconSizeProperty =
        AvaloniaProperty.Register<IconButton, double>(nameof(IconSize), 20);

    public static readonly StyledProperty<IImage> IconProperty =
        AvaloniaProperty.Register<IconButton, IImage>(nameof(Icon));

    public static readonly StyledProperty<bool> IsIconVisibleProperty =
        AvaloniaProperty.Register<IconButton, bool>(nameof(IsIconVisible), true);

    public double IconSize
    {
        get => GetValue(IconSizeProperty);
        set => SetValue(IconSizeProperty, value);
    }

    public IImage Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public bool IsIconVisible
    {
        get => GetValue(IsIconVisibleProperty);
        set => SetValue(IsIconVisibleProperty, value);
    }
}