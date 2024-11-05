using A6ToolKits.Common.Attributes.MVVM;
using A6ToolKits.Layout.Generator;
using A6ToolKits.Layout.Helper;
using Avalonia.Controls;
using Avalonia.Media;

namespace A6ToolKits.Layout.Controls;

/// <summary>
///     自动生成的菜单栏控件
/// </summary>
[AutoRegister(typeof(MenuBar), RegisterType.Singleton)]
public partial class MenuBar : UserControl
{
    /// <summary>
    ///     菜单栏构造函数
    /// </summary>
    public MenuBar()
    {
        InitializeComponent();

        var menuItems = MenuBarGenerateHelper.GenerateMenuItems();
        menuItems.ForEach(menuItem =>
        {
            Menu.Items.Add(menuItem);
            menuItem.Height = WindowConfig.Instance.MenuHeight;
        });

        Menu.Height = menuItems.Count == 0 ? 0 : WindowConfig.Instance.MenuHeight;
        MenuBarBorder.Background = new SolidColorBrush(WindowConfig.Instance.TertiaryColor);
    }
}