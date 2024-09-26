using A6ToolKits.Layout.Definer.Interfaces;
using Avalonia.Controls;

namespace A6ToolKits.Layout.Definer;

public abstract class LayoutDefiner : IDefiner<UserControl>
{
    public abstract MenuDefiner MenuDefiner { get; set; }
    
    public abstract ToolBarDefiner ToolBarDefiner { get; set; }
    
    public abstract StatusBarDefiner StatusBarDefiner { get; set; }
    
    public abstract PageDefiner PageDefiner { get; set; }
    
    public UserControl Build()
    {
        var result = new UserControl();
        var menu = MenuDefiner.Build();
        var toolBar = ToolBarDefiner.Build();
        var statusBar = StatusBarDefiner.Build();
        
        result.Content = new DockPanel
        {
            Children =
            {
                menu,
                toolBar,
                statusBar
            }
        };
        return result;
    }
}