using Avalonia.Controls;

namespace A6ToolKits.Layout.Definer.Interfaces;

public interface IDefiner<out T> where T : Control
{
    public T Build();
}