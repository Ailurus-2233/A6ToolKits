using A6ToolKits.Helper.Config;
using A6ToolKits.Helper.Instance;
using A6ToolKits.Layout.Definer;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform;

namespace A6ToolKits.Layout;

public static class Generator
{
    /// <summary>
    ///     实例创建器，用于初始化布局过程中控件实例的创建
    /// </summary>
    public static IInstanceHelper? Creator { private get; set; }
    
    public static Window GenerateLayout()
    {
        var layoutConfigItem = ConfigHelper.GetElements("Layout")?.Item(0);
        if (layoutConfigItem == null)
            throw new Exception("Layout config not found.");
        

        var configItem = new LayoutConfigItem();
        configItem.GenerateFromXmlNode(layoutConfigItem);
        
        if (configItem.Target == null)
            throw new Exception("Target property not found.");

        if (Creator?.CreateInstance(configItem.Target, configItem.Assembly) is not LayoutDefiner layout)
            throw new Exception("Layout not found.");
        
        if (!IsLayoutDefiner(layout.GetType()))
            throw new Exception("Layout must be a subclass of LayoutDefiner.");

        var window = layout.Build();
        
        return window;
    }
    
    private static bool IsLayoutDefiner(Type type)
    {
        return type.IsSubclassOf(typeof(LayoutDefiner));
    }
}