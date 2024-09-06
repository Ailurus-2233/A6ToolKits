using System.Xml;
using A6ToolKits.Helper;
using A6ToolKits.Layout.Container;
using Avalonia.Layout;

namespace A6ToolKits.Layout;

public static class LayoutLoader
{
    public static WindowLayout LoadLayout()
    {
        var layout = new WindowLayout();
        var layoutElement = ConfigHelper.GetElements("Layout")?[0];
        if (layoutElement == null)
            throw new Exception("Failed to load layout configuration");

        SetWindowProperty(layout, layoutElement);

        SetMenuPanel(layout, layoutElement);

        return layout;
    }

    private static void SetWindowProperty(WindowLayout layout, XmlNode node)
    {
        var type = node.Attributes?["Type"]?.Value;
        if (type != null)
        {
            layout.Type = type switch
            {
                "Window" => WindowType.Window,
                "FullScreen" => WindowType.FullScreen,
                _ => WindowType.Window
            };
        }

        var width = node.Attributes?["Width"]?.Value;
        if (width != null) layout.Width = double.Parse(width);

        var height = node.Attributes?["Height"]?.Value;
        if (height != null) layout.Height = double.Parse(height);

        var organizations = node.Attributes?["Organization"]?.Value.Split(",");

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

    private static void SetMenuPanel(WindowLayout layout, XmlNode layoutElement)
    {
        var menuNode = layoutElement["Menu"];
        if (menuNode == null)
            throw new Exception("Failed to load menu configuration");

        var assembly = menuNode.Attributes["Assembly"]?.Value;
        var target = menuNode.Attributes["Target"]?.Value;

        if (target == null)
            throw new Exception("Invalid menu configuration");

        if (AssemblyHelper.CreateInstance(assembly, target) is not LayoutMenu targetMenu)
            throw new Exception("Invalid menu configuration");

        targetMenu.LoadFromXml(menuNode);
        targetMenu.GenerateMenu().ForEach(menuItem =>
        {
            menuItem.Classes.Add("Root");
            layout.Menu.Items.Add(menuItem);
        });
        
        if (layout.Menu.Items.Count > 0)
            layout.Menu.IsVisible = true;
    }
}