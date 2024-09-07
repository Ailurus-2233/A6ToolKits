using Avalonia.Controls.Primitives;

namespace A6ToolKits.Layout.Generate;

public interface IGenerateControl<out T> where T : TemplatedControl
{
    public T GenerateControl();
}