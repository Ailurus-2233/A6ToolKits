using A6ToolKits.Bootstrapper;
using A6ToolKits.Database;
using A6ToolKits.Layout;
using Avalonia.Controls;

namespace ToDoList;

public class Bootstrapper: BaseBootstrapper<MainApp, Window>
{
    protected override List<Type> ToLoadModules =>
    [
        typeof(ILayoutModule),
        typeof(IDatabaseModule)
    ];
}