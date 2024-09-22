using A6ToolKits.UIPackage.LayoutControls.Buttons;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace A6ToolKits.UIPackage.LayoutControls.Common;

public class Icon : TemplatedControl
{
    public static readonly StyledProperty<double> IconSizeProperty =
        AvaloniaProperty.Register<Icon, double>(nameof(IconSize), 20);

    public static readonly StyledProperty<bool> IsIconVisibleProperty =
        AvaloniaProperty.Register<Icon, bool>(nameof(IsIconVisible), true);
    
    public static readonly StyledProperty<IImage> IconImageProperty =
        AvaloniaProperty.Register<Icon, IImage>(nameof(IconImage));
    
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
    
    public IImage IconImage
    {
        get => GetValue(IconImageProperty);
        set => SetValue(IconImageProperty, value);
    }
}