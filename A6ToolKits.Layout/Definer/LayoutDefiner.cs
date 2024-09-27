using A6ToolKits.Common.Action;
using A6ToolKits.Common.Action.CommonActions;
using A6ToolKits.Common.Attributes.Layout;
using A6ToolKits.Layout.Definer.Interfaces;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace A6ToolKits.Layout.Definer;

public abstract class LayoutDefiner : IDefiner<UserControl>
{
    private readonly UserControl _control = new();
    
    public abstract MenuDefiner MenuDefiner { get; set; }
    
    public abstract ToolBarDefiner ToolBarDefiner { get; set; }
    
    public abstract StatusBarDefiner StatusBarDefiner { get; set; }
    
    public abstract PageDefiner PageDefiner { get; set; }
    
    public abstract double HeaderHeight { get; set; }
    
    public abstract double ToolBarHeight { get; set; }
    
    public abstract double StatusBarHeight { get; set; }
    
    public abstract string Title { get; set; }
    
    public UserControl Build()
    {
        var grid = new Grid
        {
            Name = "MainGrid",
            RowDefinitions = new RowDefinitions($"Auto,Auto,*,Auto,{StatusBarHeight}")
        };
        
        var topContainer = BuildTopContainer();
        Grid.SetRow(topContainer, 0);
        
        var splitLine1 = new Separator
        {
            Width = 1,
            Height = double.NaN,
        };
        Grid.SetRow(splitLine1, 1);
        
        var page = PageDefiner.Build();
        Grid.SetRow(page, 2);
        
        var splitLine2 = new Separator
        {
            Width = 1,
            Height = double.NaN,
        };
        Grid.SetRow(splitLine2, 3);
        
        var statusBar = StatusBarDefiner.Build();
        Grid.SetRow(statusBar, 4);
        
        grid.Children.Add(topContainer);
        grid.Children.Add(splitLine1);
        grid.Children.Add(page);
        grid.Children.Add(splitLine2);
        grid.Children.Add(statusBar);
        
        _control.Content = grid;
        
        return _control;
    }

    private DockPanel BuildHeaderBar()
    {
        var result = new DockPanel();
        
        var menu = MenuDefiner.Build();
        DockPanel.SetDock(menu, Dock.Left);
        result.Children.Add(menu);
        
        var stackPanel = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center
        };
        DockPanel.SetDock(stackPanel, Dock.Right);
        var minusButton = new MinusAction().GenerateButton(ButtonType.Icon);
        var closeButton = new CloseAction().GenerateButton(ButtonType.Icon);
        var maximizeButton = new MaximizeAction().GenerateButton(ButtonType.Icon);
        maximizeButton.PropertyChanged += (_, _) =>
        {
            maximizeButton.IsVisible = maximizeButton.IsEnabled;
        };
        var windowButton = new WindowAction().GenerateButton(ButtonType.Icon);
        windowButton.PropertyChanged += (_, _) =>
        {
            windowButton.IsVisible = windowButton.IsEnabled;
        };
        
        stackPanel.Children.Add(minusButton);
        stackPanel.Children.Add(maximizeButton);
        stackPanel.Children.Add(windowButton);
        stackPanel.Children.Add(closeButton);
        result.Children.Add(stackPanel);
        
        
        var title = new TextBlock
        {
            Text = Title,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center
        };
        result.Children.Add(title);
        
        return result;
    }

    private Grid BuildTopContainer()
    {
        var result = new Grid
        {
            RowDefinitions = new RowDefinitions($"{HeaderHeight},Auto,{ToolBarHeight}")
        };
        
        var headerBar = BuildHeaderBar();
        Grid.SetRow(headerBar, 0);
        result.Children.Add(headerBar);
        
        var splitLine = new Separator
        {
            Width = 1,
            Height = double.NaN,
        };
        Grid.SetRow(splitLine, 1);
        result.Children.Add(splitLine);
        
        var toolBar = ToolBarDefiner.Build();
        Grid.SetRow(toolBar, 2);
        result.Children.Add(toolBar);
        
        return result;
    }

    public void ReplacePage(PageDefiner pageDefiner)
    {
        PageDefiner = pageDefiner;
        _control.GetControl<Grid>("MainGrid").Children[2] = PageDefiner.Build();
    }
    
    public void UpdatePageLayout(LayoutAlignment alignment)
    {
        PageDefiner.Alignment = alignment;
        PageDefiner.UpdatePageLayout();
        
    }
}