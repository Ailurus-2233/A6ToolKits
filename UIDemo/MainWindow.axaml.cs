using System.Collections.ObjectModel;
using A6ToolKits.UIPackage.LayoutControls.Tab;
using A6ToolKits.UIPackage.LayoutControls.Tab.Models;
using Avalonia.Controls;
using TabItem = A6ToolKits.UIPackage.LayoutControls.Tab.Models.TabItem;

namespace UIDemo;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var tabHeader = this.FindControl<TabHeader>("TabHeader");
        if (tabHeader != null) tabHeader.TabCollection = TabItems;
    }

    private TabItemCollection TabItems { get; set; } = new("Group1", [
        new TabItem { Header = "Tab1", ToolTip = "Content1" },
        new TabItem { Header = "Tab2", ToolTip = "Content2" },
        new TabItem { Header = "Tab3", ToolTip = "Content3" }
    ]);
}