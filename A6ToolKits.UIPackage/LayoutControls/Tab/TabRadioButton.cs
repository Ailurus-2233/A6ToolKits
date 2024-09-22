using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace A6ToolKits.UIPackage.LayoutControls.Tab;

public class TabRadioButton : RadioButton
{
    public static readonly StyledProperty<IImage?> IconProperty =
        AvaloniaProperty.Register<TabRadioButton, IImage?>(nameof(Icon));

    public static readonly StyledProperty<double> IconSizeProperty =
        AvaloniaProperty.Register<TabRadioButton, double>(nameof(IconSize), 20);

    public static readonly StyledProperty<string> ToolTipProperty =
        AvaloniaProperty.Register<TabRadioButton, string>(nameof(ToolTip), "");

    public static readonly StyledProperty<string> HeaderProperty =
        AvaloniaProperty.Register<TabRadioButton, string>(nameof(Header));

    public static readonly StyledProperty<bool> IsCloseableProperty =
        AvaloniaProperty.Register<TabRadioButton, bool>(nameof(IsCloseable), false);

    public static readonly StyledProperty<IBrush> PromptLineColorProperty =
        AvaloniaProperty.Register<TabRadioButton, IBrush>(nameof(PromptLineColor), SolidColorBrush.Parse("#3377D2"));
    
    public IImage? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public double IconSize
    {
        get => GetValue(IconSizeProperty);
        set => SetValue(IconSizeProperty, value);
    }

    public string ToolTip
    {
        get => GetValue(ToolTipProperty);
        set => SetValue(ToolTipProperty, value);
    }

    public string Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    public bool IsCloseable
    {
        get => GetValue(IsCloseableProperty);
        set => SetValue(IsCloseableProperty, value);
    }

    public IBrush PromptLineColor
    {
        get => GetValue(PromptLineColorProperty);
        set => SetValue(PromptLineColorProperty, value);
    }

    public bool DisplayIcon => Icon != null;
    
}