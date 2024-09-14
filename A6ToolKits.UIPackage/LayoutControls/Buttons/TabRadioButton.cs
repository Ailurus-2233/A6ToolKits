using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Input;
using Avalonia.Media;

namespace A6ToolKits.UIPackage.LayoutControls.Buttons;

[PseudoClasses(":clicked")]
public class TabRadioButton : RadioButton
{
    public static readonly StyledProperty<IImage> IconProperty =
        AvaloniaProperty.Register<TabRadioButton, IImage>(nameof(Icon));

    public static readonly StyledProperty<double> IconSizeProperty =
        AvaloniaProperty.Register<TabRadioButton, double>(nameof(IconSize), 20);

    public static readonly StyledProperty<string> ToolTipProperty =
        AvaloniaProperty.Register<TabRadioButton, string>(nameof(ToolTip));

    public static readonly StyledProperty<string> HeaderProperty =
        AvaloniaProperty.Register<TabRadioButton, string>(nameof(Header));

    public static readonly StyledProperty<bool> IsCloseableProperty =
        AvaloniaProperty.Register<TabRadioButton, bool>(nameof(IsCloseable));
    
    public static readonly StyledProperty<bool> IsSelectedProperty =
        AvaloniaProperty.Register<TabRadioButton, bool>(nameof(IsSelected));

    public IImage Icon
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

    public bool IsSelected
    {
        get => GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    public void OnPointerPressed(object sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            PseudoClasses.Add(":clicked");
        }
    }
}