using A6ToolKits.Common.Attributes.Layout;
using Avalonia.Controls;

namespace UIDemo.Layout.StatusBars;

[StatusBar("Left")]
public partial class LeftStatusBar : UserControl
{
    public LeftStatusBar()
    {
        InitializeComponent();
    }
}