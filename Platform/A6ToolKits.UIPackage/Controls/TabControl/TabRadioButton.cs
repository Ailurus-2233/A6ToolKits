using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace A6ToolKits.UIPackage.Controls.TabControl;

public class TabRadioButton : RadioButton
{
    private static readonly string[] PromptLinePositions = ["Top", "Bottom", "Left", "Right", "Default"];
    private static readonly string[] DisplayTypes = ["Icon", "Text", "IconAndText"];

    public static readonly StyledProperty<IImage?> IconProperty =
        AvaloniaProperty.Register<TabRadioButton, IImage?>(nameof(Icon));

    public static readonly StyledProperty<double> IconSizeProperty =
        AvaloniaProperty.Register<TabRadioButton, double>(nameof(IconSize), 20);

    public static readonly StyledProperty<string> ToolTipProperty =
        AvaloniaProperty.Register<TabRadioButton, string>(nameof(ToolTip), "");

    public static readonly StyledProperty<string> HeaderProperty =
        AvaloniaProperty.Register<TabRadioButton, string>(nameof(Header));

    public static readonly StyledProperty<bool> IsCloseableProperty =
        AvaloniaProperty.Register<TabRadioButton, bool>(nameof(IsCloseable));

    public static readonly StyledProperty<IBrush> PromptLineColorProperty =
        AvaloniaProperty.Register<TabRadioButton, IBrush>(nameof(PromptLineColor), SolidColorBrush.Parse("#3377D2"));

    public static readonly StyledProperty<string> PromptLinePositionProperty =
        AvaloniaProperty.Register<TabRadioButton, string>(nameof(PromptLinePosition), "Default");
    
    public static readonly StyledProperty<string> DisplayTypeProperty =
        AvaloniaProperty.Register<TabRadioButton, string>(nameof(DisplayType), "IconAndText");
    
    public static readonly StyledProperty<ICommand> CloseCommandProperty = 
        AvaloniaProperty.Register<TabRadioButton, ICommand>(nameof (CloseCommand), enableDataValidation: true);
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

    public string PromptLinePosition
    {
        get => GetValue(PromptLinePositionProperty);
        set => SetValue(PromptLinePositionProperty, PromptLinePositions.Contains(value) ? value : "Default");
    }
    
    public string DisplayType
    {
        get => GetValue(DisplayTypeProperty);
        set => SetValue(DisplayTypeProperty, DisplayTypes.Contains(value) ? value : "IconAndText");
    }

    public bool DisplayIcon => Icon != null;

    public Thickness PromptThickness
    {
        get
        {
            return PromptLinePosition switch
            {
                "Bottom" => new Thickness(0, 0, 0, 2),
                "Top" => new Thickness(0, 2, 0, 0),
                "Left" => new Thickness(2, 0, 0, 0),
                "Right" => new Thickness(0, 0, 2, 0),
                "Default" => new Thickness(0, 0, 0, 0),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
    
    public bool IsIconVisible => DisplayType is "Icon" or "IconAndText";
    
    public bool IsHeaderVisible => DisplayType is "Text" or "IconAndText";
    
    public ICommand CloseCommand
    {
        get => GetValue(CloseCommandProperty);
        set => SetValue(CloseCommandProperty, value);
    }
}