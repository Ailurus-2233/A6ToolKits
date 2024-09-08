using Avalonia.Controls;

namespace A6ToolKits.Layout.Definitions;

public interface IDefinition<T> where T: Control
{
    public List<T> Generate();
}