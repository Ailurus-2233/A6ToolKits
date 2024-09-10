using System.Xml;
using A6ToolKits.Helper;
using A6ToolKits.Helper.Assembly;
using A6ToolKits.Helper.Config;
using A6ToolKits.Layout.Attributes;
using A6ToolKits.Layout.Container;
using A6ToolKits.Layout.Definitions;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace A6ToolKits.Layout.Helper;

public static class GenerateHelper
{
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

        var organizations = layoutItem.Organization.Split(",");

        if (organizations is not { Length: 4 })
            throw new Exception("Invalid layout configuration");

        layout.LeftPanel.Orientation = SwitchOrientation(organizations[0]);
        layout.TopPanel.Orientation = SwitchOrientation(organizations[1]);
        layout.RightPanel.Orientation = SwitchOrientation(organizations[2]);
        layout.BottomPanel.Orientation = SwitchOrientation(organizations[3]);

        var color = layoutItem.MainColor;
        if (color.StartsWith('#'))
            layout.MainColor = Color.Parse(color);

        var iconPath = layoutItem.IconPath;
        if (!string.IsNullOrEmpty(iconPath))
            layout.WindowContainer.Icon = new WindowIcon(iconPath);

        layout.WindowContainer.Title = layoutItem.Name;
    }

    private static Orientation SwitchOrientation(string orientation)
    {
        return orientation switch
        {
            "V" => Orientation.Vertical,
            "H" => Orientation.Horizontal,
            _ => Orientation.Vertical
        };
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

        foreach (var targetItem in menuDefinition.GenerateMenuItem())
        {
            layout.Menu.Items.Add(targetItem);
        }

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

        layout.WindowContainer.ToolBar.Height =
            toolBarConfigItem.Height == "Auto" ? double.NaN : double.Parse(toolBarConfigItem.Height);


        toolBarDefinition.GenerateToolBar(ToolBarPosition.Left).ForEach(item => { layout.WindowContainer.ToolBar.Children.Add(item); });

        toolBarDefinition.GenerateToolBar(ToolBarPosition.Right).ForEach(item => { layout.WindowContainer.RightToolBar.Children.Add(item); });

        layout.WindowContainer.ToolBar.IsVisible = layout.WindowContainer.ToolBar.Children.Count != 0;
        layout.WindowContainer.RightToolBar.IsVisible = layout.WindowContainer.RightToolBar.Children.Count != 0;
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

        layout.WindowContainer.StatusBar.Height = statusBarConfigItem.Height == "Auto"
            ? double.NaN
            : double.Parse(statusBarConfigItem.Height);

        statusBarDefinition.GenerateStatusBar(StatusPosition.Left).ForEach(item => { layout.WindowContainer.LeftStatus.Children.Add(item); });
        statusBarDefinition.GenerateStatusBar(StatusPosition.Right).ForEach(item => { layout.WindowContainer.RightStatus.Children.Add(item); });
        statusBarDefinition.GenerateStatusBar(StatusPosition.Center).ForEach(item => { layout.WindowContainer.CenterStatus.Children.Add(item); });

        var visible = layout.WindowContainer.LeftStatus.Children.Count != 0 ||
                      layout.WindowContainer.RightStatus.Children.Count != 0 ||
                      layout.WindowContainer.CenterStatus.Children.Count != 0;

        layout.WindowContainer.StatusBar.IsVisible = visible;

        var color = layout.MainColor;
        layout.WindowContainer.StatusBar.Background = new SolidColorBrush(color, 0.5);
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