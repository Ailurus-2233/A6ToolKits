using Avalonia.Controls;

namespace A6ToolKits.Layout.Controls.LayoutWindow;

/// <summary>
///     无边框窗口
/// </summary>
public partial class EmptyWindow : WindowBase
{
    /// <summary>
    ///     无边框窗口
    /// </summary>
    public EmptyWindow()
    {
        InitializeComponent();
    }
    
    /// <inheritdoc />
    public override UserControl? MainRegion
    {
        get => MainControl.Children.FirstOrDefault() as UserControl;
        set
        {
            if (value == null) return;
            MainControl.Children.Clear();
            MainControl.Children.Add(value);
        }
    }
}