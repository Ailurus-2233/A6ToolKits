using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace A6ToolKits.Command.Controls;

public class CommandButton : Button
{
    public static readonly StyledProperty<string> TextProperty =
        AvaloniaProperty.Register<CommandButton, string>(nameof(Text));

    public static readonly StyledProperty<string> ToolTipProperty =
        AvaloniaProperty.Register<CommandButton, string>(nameof(ToolTip));

    public static readonly StyledProperty<IImage> ImageSourceProperty =
        AvaloniaProperty.Register<CommandButton, IImage>(nameof(ImageSource));

    public static readonly StyledProperty<CommandDefinition> DefinitionProperty =
        AvaloniaProperty.Register<CommandButton, CommandDefinition>(nameof(Definition));

    public static readonly StyledProperty<double> ButtonSizeProperty =
        AvaloniaProperty.Register<CommandButton, double>(nameof(ButtonSize), 32);
    
    public static readonly StyledProperty<double> IconSizeProperty =
        AvaloniaProperty.Register<CommandButton, double>(nameof(IconSize), 30);
    
    public string Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public string ToolTip
    {
        get => GetValue(ToolTipProperty);
        set => SetValue(ToolTipProperty, value);
    }

    public IImage ImageSource
    {
        get => GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    public CommandDefinition Definition
    {
        get => GetValue(DefinitionProperty);
        set => SetValue(DefinitionProperty, value);
    }
    
    public double ButtonSize
    {
        get => GetValue(ButtonSizeProperty);
        set => SetValue(ButtonSizeProperty, value);
    }

    public double IconSize
    {
        get => GetValue(IconSizeProperty);
        set => SetValue(IconSizeProperty, value);
    }
    
    public CommandButton()
    {
        DefinitionProperty.Changed.AddClassHandler<CommandButton>((_, e) =>
        {
            if (e.NewValue is not CommandDefinition commandDefinition) return;
            Text = commandDefinition.Text;
            ToolTip = commandDefinition.ToolTip;
            ImageSource = commandDefinition.ImageSource;
            Click += (_, _) => commandDefinition.Run();
            IsEnabled = commandDefinition.CanRun;
            commandDefinition.CanRunChanged += (_, _) => IsEnabled = commandDefinition.CanRun;
        });
    }
}