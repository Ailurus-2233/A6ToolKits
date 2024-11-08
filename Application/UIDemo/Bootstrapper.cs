using System;
using System.Collections.Generic;
using A6ToolKits;
using A6ToolKits.Bootstrapper;
using A6ToolKits.Layout;
using A6ToolKits.Module;
using A6ToolKits.UIPackage;
using Avalonia.Controls;

namespace UIDemo;

public class Bootstrapper : BaseBootstrapper<App, Window>
{
    protected override List<Type> ToLoadModules =>
    [
        typeof(ILayoutModule),
        typeof(IUIPackageModule)
    ];
}