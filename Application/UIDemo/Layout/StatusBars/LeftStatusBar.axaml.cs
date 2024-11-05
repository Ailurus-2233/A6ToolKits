using A6ToolKits.Attributes.Layout;
using A6ToolKits.Common.Attributes.Layout;
using Avalonia.Controls;

namespace UIDemo.Layout.StatusBars;

[StatusBarItem(StatusPosition.Left, 0)]
public partial class LeftStatusBar : UserControl
{
    public LeftStatusBar()
    {
        InitializeComponent();
    }
}