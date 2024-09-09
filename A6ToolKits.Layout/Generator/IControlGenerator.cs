using A6ToolKits.Action;
using Avalonia.Controls.Primitives;

namespace A6ToolKits.Layout.Generator;

public interface IControlGenerator<out T> where T : TemplatedControl
{
    public T GenerateControl(ActionBase action);
}