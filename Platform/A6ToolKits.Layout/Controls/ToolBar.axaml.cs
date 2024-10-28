using A6ToolKits.Common.Attributes.MVVM;
using A6ToolKits.Layout.Generator;
using A6ToolKits.Layout.Helper;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DynamicData;

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
        items.ForEach(item => ToolBarPanel.Children.Add(item));
        ToolBarPanel.Height = items.Count == 0 ? 0 : WindowConfig.ToolBarHeight;
    }
}