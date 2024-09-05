using System.Xml;
using A6ToolKits.Helper;
using A6ToolKits.Layout.Models;

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
        
        return layout;
    }

    private static void SetWindowProperty(WindowLayout layout, XmlNode node)
    {
        var type = node.Attributes?["WindowType"]?.Value;
        if (type != null)
        {
            layout.WindowType = type switch
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
    }
    
    private static void SetMenuProperty(WindowLayout layout, XmlNode root)
    {
        var menuElement = root.SelectSingleNode("Menu");
        if (menuElement == null)
        {
            layout.IsContainMenu = false;
            return;
        }
        
        layout.IsContainMenu = true;
        
    }
}