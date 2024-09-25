using System.Xml;
using A6ToolKits.Attributes;
using A6ToolKits.Helper.Config;
using A6ToolKits.InstanceCreator;
using A6ToolKits.Layout.Controls;
using A6ToolKits.Layout.Definitions;
using A6ToolKits.Layout.Definitions.Extensions;
using A6ToolKits.Layout.Enums;
using A6ToolKits.Layout.Models.ConfigItems;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace A6ToolKits.Layout.Helper;

/// <summary>
///     生成窗口布局，基于配置文件，组装一个窗口布局并返回
/// </summary>
public static class LayoutGenerateHelper
{
    /// <summary>
    ///     实例创建器，用于初始化布局过程中控件实例的创建
    /// </summary>
    public static IInstanceCreator? Creator { private get; set; }

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

        SetHeaderBar(layout.HeaderBar, layoutElement, layout);

        SetStatusBar(layout.StatusBar, layoutElement, layout);

        SetPages(layout.Container);
        
        return layout;
    }

    private static void SetWindowProperty(WindowLayout layout, XmlNode layoutElement)
    {
        var layoutItem = new LayoutItemConfigItem();
        layoutItem.GenerateFromXmlNode(layoutElement);
        layout.Type = layoutItem.Type switch
        {
            "Window" => WindowType.Window,
            "FullScreen" => WindowType.FullScreen,
            _ => WindowType.Window
        };
        layout.Width = Convert.ToDouble(layoutItem.Width);
        layout.Height = Convert.ToDouble(layoutItem.Height);

        var color = layoutItem.BackgroundColor;
        if (color.StartsWith('#'))
            layout.BackgroundColor = Color.Parse(color);

        var current = Application.Current;
        if (current != null) current.RequestedThemeVariant = layout.Theme;

        var iconPath = layoutItem.IconPath;
        if (!string.IsNullOrEmpty(iconPath))
            layout.DefaultWindow.Icon = new WindowIcon(iconPath);

        layout.DefaultWindow.Title = layoutItem.Name;

        layout.MainPanel.SetLayout(Enum.Parse<LayoutAlignment>(layoutItem.Alignment));
    }

    private static void SetHeaderBar(HeaderBar header, XmlNode layoutElement, WindowLayout layout)
    {
        var menuNode = layoutElement["Menu"];
        if (menuNode != null)
            SetMenu(header.Menu, menuNode);

        var buttonBarNode = layoutElement["ButtonBar"];
        if (buttonBarNode != null)
            SetButtonBar(header.ButtonBar, header.RightButtonBar, buttonBarNode);

        header.SetMenuVisible(header.Menu.Items.Count != 0);
        header.SetButtonBarVisible(header.ButtonBar.Children.Count != 0);
        header.SetRightButtonBarVisible(header.RightButtonBar.Children.Count != 0);

        header.Background = new SolidColorBrush(layout.BackgroundColor, 0.8);
    }

    private static void SetMenu(Menu menu, XmlNode menuNode)
    {
        var menuConfigItem = new LayoutItemConfigItem();
        menuConfigItem.GenerateFromXmlNode(menuNode);

        if (string.IsNullOrEmpty(menuConfigItem.Target))
            throw new Exception("Invalid menu configuration");

        if (Creator?.GetOrCreateInstance(menuConfigItem.Target, menuConfigItem.Assembly) is not IDefiner
            menuDefinition)
            throw new Exception("Invalid menu configuration");

        menu.Height = menuConfigItem.Height == "Auto" ? double.NaN : double.Parse(menuConfigItem.Height);

        foreach (var targetItem in menuDefinition.GenerateMenuItem()) menu.Items.Add(targetItem);
    }

    private static void SetButtonBar(StackPanel buttonBar, StackPanel rightButtonBar, XmlNode buttonBarNode)
    {
        var buttonBarNodeConfigItem = new LayoutItemConfigItem();
        buttonBarNodeConfigItem.GenerateFromXmlNode(buttonBarNode);

        if (string.IsNullOrEmpty(buttonBarNodeConfigItem.Target))
            throw new Exception("Invalid button bar configuration");

        if (Creator?.GetOrCreateInstance(buttonBarNodeConfigItem.Target, buttonBarNodeConfigItem.Assembly) is not
            IDefiner
            buttonBarDefinition)
            throw new Exception("Invalid button bar configuration");

        buttonBar.Height =
            buttonBarNodeConfigItem.Height == "Auto" ? double.NaN : double.Parse(buttonBarNodeConfigItem.Height);


        buttonBarDefinition.GenerateButtonBar(ButtonBarPosition.Left)
            .ForEach(item => { buttonBar.Children.Add(item); });

        buttonBarDefinition.GenerateButtonBar(ButtonBarPosition.Right)
            .ForEach(item => { rightButtonBar.Children.Add(item); });
    }

    private static void SetStatusBar(StatusBar statusBar, XmlNode layoutElement, WindowLayout layout)
    {
        var statusBarNode = layoutElement["StatusBar"];
        if (statusBarNode == null)
            return;

        var statusBarConfigItem = new LayoutItemConfigItem();
        statusBarConfigItem.GenerateFromXmlNode(statusBarNode);

        if (string.IsNullOrEmpty(statusBarConfigItem.Target))
            throw new Exception("Invalid status bar configuration");

        if (Creator?.GetOrCreateInstance(statusBarConfigItem.Target, statusBarConfigItem.Assembly) is not IDefiner
            statusBarDefinition)
            throw new Exception("Invalid status bar configuration");

        statusBar.DockPanel.Height = statusBarConfigItem.Height == "Auto"
            ? double.NaN
            : double.Parse(statusBarConfigItem.Height);

        AddControlToStatusBar(statusBar.LeftStatus, statusBarDefinition, StatusPosition.Left);
        AddControlToStatusBar(statusBar.CenterStatus, statusBarDefinition, StatusPosition.Center);
        AddControlToStatusBar(statusBar.RightStatus, statusBarDefinition, StatusPosition.Right);

        var visible = statusBar.LeftStatus.Children.Count != 0 ||
                      statusBar.RightStatus.Children.Count != 0 ||
                      statusBar.CenterStatus.Children.Count != 0;

        statusBar.IsVisible = visible;
        statusBar.Background = new SolidColorBrush(layout.BackgroundColor, 0.8);
    }

    private static void AddControlToStatusBar(StackPanel statusBar, IDefiner definer, StatusPosition position)
    {
        foreach (var item in definer.GenerateStatusBar(position))
        {
            statusBar.Children.Add(item);
            statusBar.Children.Add(new Separator());
        }

        statusBar.Children.RemoveAt(statusBar.Children.Count - 1);
    }

    // private static void SetItems(WindowLayout layout)
    // {
    //     var items = ConfigHelper.GetElements("Item");
    //     if (items == null) return;
    //     foreach (XmlNode item in items)
    //     {
    //         var itemConfigItem = new LayoutItemConfigItem();
    //         itemConfigItem.GenerateFromXmlNode(item);
    //
    //         if (string.IsNullOrEmpty(itemConfigItem.Target))
    //             throw new Exception("Invalid item configuration");
    //
    //         if (Creator?.GetOrCreateInstance(itemConfigItem.Target, itemConfigItem.Assembly) is not UserControl
    //             itemControl)
    //             throw new Exception("Invalid item configuration");
    //
    //         if (ItemsLoaded[itemConfigItem.Position])
    //             throw new Exception($"Invalid item configuration: {itemConfigItem.Position} already loaded");
    //
    //         ItemsLoaded[itemConfigItem.Position] = true;
    //
    //         var dockPosition = itemConfigItem.Position switch
    //         {
    //             "Top" => Dock.Top,
    //             "Bottom" => Dock.Bottom,
    //             "Left" => Dock.Left,
    //             "Right" => Dock.Right,
    //             _ => throw new Exception("Invalid item configuration")
    //         };
    //
    //         itemControl.SetValue(DockPanel.DockProperty, dockPosition);
    //         layout.MainPanel.Children.Add(itemControl);
    //     }
    // }

    private static void SetPages(PageContainer container)
    {
        var pages = ConfigHelper.GetElements("Page");
        if (pages == null) return;
        string? defaultPageName = null;
        foreach (XmlNode page in pages)
        {
            var pageConfigItem = new LayoutItemConfigItem();
            pageConfigItem.GenerateFromXmlNode(page);
    
            if (string.IsNullOrEmpty(pageConfigItem.Target))
                throw new Exception("Invalid page configuration");
    
            if (Creator?.GetOrCreateInstance(pageConfigItem.Target, pageConfigItem.Assembly) is not UserControl
                pageControl)
                throw new Exception("Invalid page configuration");
    
            container.AddPage(pageConfigItem.Name, pageControl);
            if (pageConfigItem.Default == "True")
                defaultPageName = pageConfigItem.Name;
        }
    
        if (defaultPageName != null)
            container.ActivatePage(defaultPageName);
    }
}