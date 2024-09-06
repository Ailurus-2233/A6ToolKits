using A6ToolKits.Action;
using A6ToolKits.Action.Controls;
using A6ToolKits.Helper;
using Avalonia.Controls;

namespace A6ToolKits.Layout.Helper;

public static class LayoutHelper
{
    public static Menu? GenerateMenu(string assemblyName, string typeName)
    {
        var result = AssemblyHelper.CreateInstance<Menu>(assemblyName, typeName);
        return result;
    }
}