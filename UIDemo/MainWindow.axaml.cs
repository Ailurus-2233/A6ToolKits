using System.Collections.ObjectModel;
using A6ToolKits.UIPackage.Controls.Layout.Tab;
using A6ToolKits.UIPackage.Controls.Layout.Tab.Models;
using Avalonia.Controls;
using UIDemo.TabDemo;
using TabItem = A6ToolKits.UIPackage.Controls.Layout.Tab.Models.TabItem;

namespace UIDemo;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var tabHeader = this.FindControl<TabHeader>("TabHeader");
        TabItemCollection tabItems = new("Group1", [
            new TabItem { Header = "Tab1", ToolTip = "Content1", GroupName = "Group1", Content = new Tab1() },
            new TabItem { Header = "Tab2", ToolTip = "Content2", GroupName = "Group1", Content = new Tab2() },
            new TabItem { Header = "Tab3", ToolTip = "Content3", GroupName = "Group1", Content = new Tab3() }
        ])
        {
            IsCloseable = true
        };
        if (tabHeader == null) return;
        tabHeader.TabCollection = tabItems;
        
        var tabContainer = this.FindControl<TabContainer>("TabContainer");
        if (tabContainer == null) return;
        tabContainer.TabCollection = tabItems;
    }
}