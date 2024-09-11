using System.Xml;
using A6ToolKits.Attributes;
using A6ToolKits.Helper.Assembly;
using A6ToolKits.Helper.Config;
using A6ToolKits.Layout.Container;
using A6ToolKits.Layout.Definitions;
using Avalonia.Controls;
using Avalonia.Media;

namespace A6ToolKits.Layout.Helper;

/// <summary>
///     生成窗口布局，基于配置文件，组装一个窗口布局并返回
/// </summary>
public static class GenerateHelper
{
    /// <summary>
    ///     生成窗口布局，基于配置文件，组装一个窗口布局并返回
    /// </summary>
    /// <returns>
    ///     生成的窗口布局
    /// </returns>
    /// <exception cref="Exception">
    ///     配置文件读取异常
    /// </exception>
    public static WindowLayout LoadLayout()
    {
        var layout = new WindowLayout();
        var layoutElement = ConfigHelper.GetElements("Layout")?[0];
        if (layoutElement == null)
            throw new Exception("Failed to load layout configuration");

        SetWindowProperty(layout, layoutElement);

        SetMenu(layout, layoutElement);

        SetToolBar(layout, layoutElement);

        SetStatusBar(layout, layoutElement);

        SetItems(layout);

        SetPages(layout);

        return layout;
    }

    private static void SetWindowProperty(WindowLayout layout, XmlNode node)
    {
        var layoutItem = new LayoutItemConfigItem();
        layoutItem.GenerateFromXmlNode(node);
        layout.Type = layoutItem.Type switch
        {
            "Window" => WindowType.Window,
            "FullScreen" => WindowType.FullScreen,
            _ => WindowType.Window
        };
        layout.Width = Convert.ToDouble(layoutItem.Width);
        layout.Height = Convert.ToDouble(layoutItem.Height);

        var color = layoutItem.MainColor;
        if (color.StartsWith('#'))
            layout.MainColor = Color.Parse(color);

        var iconPath = layoutItem.IconPath;
        if (!string.IsNullOrEmpty(iconPath))
            layout.WindowContainer.Icon = new WindowIcon(iconPath);

        layout.WindowContainer.Title = layoutItem.Name;
    }

    private static void SetMenu(WindowLayout layout, XmlNode layoutElement)
    {
        var menuNode = layoutElement["Menu"];
        if (menuNode == null)
            return;

        var menuConfigItem = new LayoutItemConfigItem();
        menuConfigItem.GenerateFromXmlNode(menuNode);

        if (string.IsNullOrEmpty(menuConfigItem.Target))
            throw new Exception("Invalid menu configuration");

        if (AssemblyHelper.CreateInstance(menuConfigItem.Assembly, menuConfigItem.Target) is not IDefinition
            menuDefinition)
            throw new Exception("Invalid menu configuration");

        layout.Menu.Height = menuConfigItem.Height == "Auto" ? double.NaN : double.Parse(menuConfigItem.Height);

        foreach (var targetItem in menuDefinition.GenerateMenuItem()) layout.Menu.Items.Add(targetItem);

        layout.Menu.IsVisible = layout.Menu.Items.Count != 0;
    }

    private static void SetToolBar(WindowLayout layout, XmlNode layoutElement)
    {
        var toolBarNode = layoutElement["ToolBar"];
        if (toolBarNode == null)
            return;

        var toolBarConfigItem = new LayoutItemConfigItem();
        toolBarConfigItem.GenerateFromXmlNode(toolBarNode);

        if (string.IsNullOrEmpty(toolBarConfigItem.Target))
            throw new Exception("Invalid toolbar configuration");

        if (AssemblyHelper.CreateInstance(toolBarConfigItem.Assembly, toolBarConfigItem.Target) is not IDefinition
            toolBarDefinition)
            throw new Exception("Invalid toolbar configuration");

        layout.ToolBar.Height =
            toolBarConfigItem.Height == "Auto" ? double.NaN : double.Parse(toolBarConfigItem.Height);


        toolBarDefinition.GenerateToolBar(ToolBarPosition.Left).ForEach(item => { layout.ToolBar.Children.Add(item); });

        toolBarDefinition.GenerateToolBar(ToolBarPosition.Right).ForEach(item => { layout.RightToolBar.Children.Add(item); });

        layout.ToolBar.IsVisible = layout.ToolBar.Children.Count != 0;
        layout.RightToolBar.IsVisible = layout.RightToolBar.Children.Count != 0;
    }

    private static void SetStatusBar(WindowLayout layout, XmlNode layoutElement)
    {
        var statusBarNode = layoutElement["StatusBar"];
        if (statusBarNode == null)
            return;

        var statusBarConfigItem = new LayoutItemConfigItem();
        statusBarConfigItem.GenerateFromXmlNode(statusBarNode);

        if (string.IsNullOrEmpty(statusBarConfigItem.Target))
            throw new Exception("Invalid status bar configuration");

        if (AssemblyHelper.CreateInstance(statusBarConfigItem.Assembly, statusBarConfigItem.Target) is not IDefinition
            statusBarDefinition)
            throw new Exception("Invalid status bar configuration");

        layout.StatusBar.Height = statusBarConfigItem.Height == "Auto"
            ? double.NaN
            : double.Parse(statusBarConfigItem.Height);

        AddControlToStatusBar(layout.LeftStatusBar, statusBarDefinition, StatusPosition.Left);
        AddControlToStatusBar(layout.CenterStatusBar, statusBarDefinition, StatusPosition.Center);
        AddControlToStatusBar(layout.RightStatusBar, statusBarDefinition, StatusPosition.Right);

        var visible = layout.LeftStatusBar.Children.Count != 0 ||
                      layout.RightStatusBar.Children.Count != 0 ||
                      layout.CenterStatusBar.Children.Count != 0;

        layout.StatusBar.IsVisible = visible;

        var color = layout.MainColor;
        layout.StatusBar.Background = new SolidColorBrush(color);
    }

    private static void AddControlToStatusBar(StackPanel statusBar, IDefinition definition, StatusPosition position)
    {
        foreach (var item in definition.GenerateStatusBar(position))
        {
            statusBar.Children.Add(item);
            statusBar.Children.Add(new Separator());
        }

        statusBar.Children.RemoveAt(statusBar.Children.Count - 1);
    }

    private static void SetItems(WindowLayout layout)
    {
        var items = ConfigHelper.GetElements("Item");
        if (items == null) return;
        foreach (XmlNode item in items)
        {
            var itemConfigItem = new LayoutItemConfigItem();
            itemConfigItem.GenerateFromXmlNode(item);

            if (string.IsNullOrEmpty(itemConfigItem.Target))
                throw new Exception("Invalid item configuration");

            if (AssemblyHelper.CreateInstance(itemConfigItem.Assembly, itemConfigItem.Target) is not UserControl
                itemControl)
                throw new Exception("Invalid item configuration");

            itemControl.SetValue(DockPanel.DockProperty, itemConfigItem.Position switch
            {
                "Top" => Dock.Top,
                "Bottom" => Dock.Bottom,
                "Left" => Dock.Left,
                "Right" => Dock.Right,
                _ => throw new Exception("Invalid item configuration")
            });
            layout.MainPanel.Children.Add(itemControl);
        }
    }

    private static void SetPages(WindowLayout layout)
    {
        var pages = ConfigHelper.GetElements("Page");
        if (pages == null) return;
        UserControl? defaultPage = null;
        foreach (XmlNode page in pages)
        {
            var pageConfigItem = new LayoutItemConfigItem();
            pageConfigItem.GenerateFromXmlNode(page);

            if (string.IsNullOrEmpty(pageConfigItem.Target))
                throw new Exception("Invalid page configuration");

            if (AssemblyHelper.CreateInstance(pageConfigItem.Assembly, pageConfigItem.Target) is not UserControl
                pageControl)
                throw new Exception("Invalid page configuration");

            layout.Container.AddPage(pageConfigItem.Name, pageControl);
            if (pageConfigItem.Default == "True")
                defaultPage = pageControl;
        }

        if (defaultPage != null)
            layout.Container.ActivatePage(defaultPage);
        else
            layout.Container.MoveToPage(0);
    }
}