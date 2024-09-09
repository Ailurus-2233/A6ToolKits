using System.Xml;
using A6ToolKits.Helper;
using A6ToolKits.Layout.Attributes;
using A6ToolKits.Layout.Container;
using A6ToolKits.Layout.Definitions;
using Avalonia.Controls;
using Avalonia.Layout;

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

        return layout;
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

        if (AssemblyHelper.CreateInstance(toolBarConfigItem.Assembly, toolBarConfigItem.Target) is not IDefinition toolBarDefinition)
            throw new Exception("Invalid toolbar configuration");

        layout.WindowContainer.ToolBar.Height = toolBarConfigItem.Height == "Auto" ? double.NaN : double.Parse(toolBarConfigItem.Height);


        toolBarDefinition.GenerateToolBar(Position.Left).ForEach(item => { layout.WindowContainer.ToolBar.Children.Add(item); });

        toolBarDefinition.GenerateToolBar(Position.Right).ForEach(item => { layout.WindowContainer.RightToolBar.Children.Add(item); });

        layout.WindowContainer.ToolBar.IsVisible = layout.WindowContainer.ToolBar.Children.Count != 0;
        layout.WindowContainer.RightToolBar.IsVisible = layout.WindowContainer.RightToolBar.Children.Count != 0;
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
}

public class LayoutItemConfigItem : ConfigItemBase
{
    public string Type { get; set; } = string.Empty;
    public string Height { get; set; } = string.Empty;
    public string Width { get; set; } = string.Empty;
    public string Organization { get; set; } = string.Empty;

    public string Position { get; set; } = string.Empty;
    public string Default { get; set; } = string.Empty;
    public string Assembly { get; set; } = string.Empty;
    public string Target { get; set; } = string.Empty;
}