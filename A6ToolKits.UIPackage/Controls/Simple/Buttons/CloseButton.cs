using Avalonia;
using Avalonia.Controls;

namespace A6ToolKits.UIPackage.Controls.Simple.Buttons;

public class CloseButton : Button
{
    public static readonly StyledProperty<double> IconSizeProperty =
        AvaloniaProperty.Register<CloseButton, double>(nameof(IconSize), 20);

    public static readonly StyledProperty<bool> IsIconVisibleProperty =
        AvaloniaProperty.Register<CloseButton, bool>(nameof(IsIconVisible), true);
    
    public double IconSize
    {
        get => GetValue(IconSizeProperty);
        set => SetValue(IconSizeProperty, value);
    }
    
    public bool IsIconVisible
    {
        get => GetValue(IsIconVisibleProperty);
        set => SetValue(IsIconVisibleProperty, value);
    }
}