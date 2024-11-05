using A6ToolKits.Common.Attributes.MVVM;
using A6ToolKits.Layout.Generator;
using A6ToolKits.Layout.Helper;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace A6ToolKits.Layout.Controls;

/// <summary>
///     自动生成的工具栏
/// </summary>
[AutoRegister(typeof(ToolBar), RegisterType.Singleton)]
public partial class ToolBar : UserControl
{
    /// <summary>
    ///     构造函数
    /// </summary>
    public ToolBar()
    {
        InitializeComponent();

        var items = ToolBarGenerateHelper.GenerateToolBarItems();
        if (items.Count == 0)
        {
            ToolBarPanel.Height = 0;
            ToolBarBorder.BorderThickness = new Thickness(0);
        }
        else
        {
            items.ForEach(item =>
            {
                ToolBarPanel.Children.Add(item);
                if (item is Button button)
                    SetButtonStyle(button);
                if (item is Separator separator)
                    SetSeparatorStyle(separator);
            });
            ToolBarPanel.Height = WindowConfig.Instance.ToolBarHeight;
            ToolBarBorder.Background = new SolidColorBrush(WindowConfig.Instance.TertiaryColor);
            ToolBarBorder.BorderBrush = new SolidColorBrush(WindowConfig.Instance.PrimaryColor);
        }
    }

    private void SetButtonStyle(Button button)
    {
        button.BorderThickness = new Thickness(0);
        button.Background = new SolidColorBrush(WindowConfig.Instance.TertiaryColor);
        button.Margin = new Thickness(5, 0, 0, 0);
        button.Height = WindowConfig.Instance.ToolBarHeight - 4;
    }

    private void SetSeparatorStyle(Separator separator)
    {
        separator.Background = new SolidColorBrush(WindowConfig.Instance.SecondaryColor);
        var height = WindowConfig.Instance.ToolBarHeight - 15 > 5 ? WindowConfig.Instance.ToolBarHeight - 15 : 5;
        separator.Margin = new Thickness(5, 0, 0, 0);
        separator.Height = height;
    }
}