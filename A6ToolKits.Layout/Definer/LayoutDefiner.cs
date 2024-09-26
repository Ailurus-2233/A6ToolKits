using A6ToolKits.Layout.Definer.Interfaces;
using Avalonia.Controls;

namespace A6ToolKits.Layout.Definer;

public abstract class LayoutDefiner : IDefiner<UserControl>
{
    public UserControl Build()
    {
        throw new NotImplementedException();
    }
}