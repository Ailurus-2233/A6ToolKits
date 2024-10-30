using A6ToolKits.Common.Attributes.MVVM;
using A6ToolKits.Layout.Generator;
using A6ToolKits.Layout.Helper;
using Avalonia.Controls;

namespace A6ToolKits.Layout.Controls;

/// <summary>
///     菜单按钮，点击后显示菜单列表
/// </summary>
[AutoRegister(typeof(MenuIcon), RegisterType.Singleton)]
public partial class MenuIcon : UserControl
{
    /// <summary>
    ///     构造函数
    /// </summary>
    public MenuIcon()
    {
        InitializeComponent();

        var menuItems = MenuBarGenerateHelper.GenerateMenuItems();
        menuItems.ForEach(menuItem =>
        {
            MenuButton.Items.Add(menuItem);
            menuItem.Height = WindowConfig.Instance.MenuHeight;
        });
    }
}