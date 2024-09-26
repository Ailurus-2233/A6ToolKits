using Avalonia.Controls;

namespace A6ToolKits.Layout.Controls;

internal static class ControlExtension
{
    internal static void SetGridPosition(this Control control, int row, int column, int rowSpan = 1, int columnSpan = 1)
    {
        control.SetValue(Grid.RowProperty, row);
        control.SetValue(Grid.ColumnProperty, column);
        control.SetValue(Grid.RowSpanProperty, rowSpan);
        control.SetValue(Grid.ColumnSpanProperty, columnSpan);
    }
}