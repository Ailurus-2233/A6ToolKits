using A6ToolKits.Common.Attributes.Layout;
using A6ToolKits.Common.Attributes.MVVM;
using A6ToolKits.Layout.Generator;
using A6ToolKits.Layout.Helper;
using Avalonia.Controls;
using Avalonia.Media;

namespace A6ToolKits.Layout.Controls;

/// <summary>
///     自动生成状态栏
/// </summary>
[AutoRegister(typeof(StatusBar), RegisterType.Singleton)]
public partial class StatusBar : UserControl
{
    /// <summary>
    ///     构造函数
    /// </summary>
    public StatusBar()
    {
        InitializeComponent();

        var left = StatusBarGenerateHelper.GenerateStatusBarItems(StatusPosition.Left);
        left.ForEach(item => LeftStatusBar.Children.Add(item));

        var right = StatusBarGenerateHelper.GenerateStatusBarItems(StatusPosition.Right);
        right.ForEach(item => RightStatusBar.Children.Add(item));

        var center = StatusBarGenerateHelper.GenerateStatusBarItems(StatusPosition.Center);
        center.ForEach(item => CenterStatusBar.Children.Add(item));
        
        var count = left.Count + right.Count + center.Count;
        StatusBarPanel.Height = count == 0 ? 0 : WindowConfig.Instance.StatusBarHeight;

        StatusBarBorder.Background = new SolidColorBrush(WindowConfig.Instance.TertiaryColor);
        StatusBarBorder.BorderBrush = new SolidColorBrush(WindowConfig.Instance.PrimaryColor);
        Height = WindowConfig.Instance.StatusBarHeight;
    }
}