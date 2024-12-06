using A6ToolKits.Common.Container;
using A6ToolKits.Configuration;
using A6ToolKits.Container.Attributes;
using A6ToolKits.Module;
using A6ToolKits.Module.Exceptions;
using A6ToolKits.Modules;
using Avalonia;
using Avalonia.Markup.Xaml.Styling;

namespace A6ToolKits;

/// <summary>
///     核心模块，首先加载的模块，并负责后续模块的加载
/// </summary>
[AutoRegister(typeof(CoreModule), RegisterType.Singleton)]
public class CoreModule : ModuleBase
{
    /// <summary>
    ///     初始化，加载模块时执行的操作
    /// </summary>
    public override void Initialize()
    {
        LoadResources();
        LoadModules();
    }

    private static void LoadModules()
    {
        ConfigHelper.GenerateDefaultConfigFile();
        var moduleList = IoC.GetInstance<IModuleManager>()?.GetToLoadModuleList();
        if (moduleList is not { Count: > 0 }) return;
        // 最后加载 LayoutModule
        var layout = moduleList.Find(t => t.Name == "ILayoutModule");

        moduleList.ForEach(module =>
        {
            if (module == layout)
                return;
            try
            {
                // 判断 module 是否是 ModuleBase 的基类
                if (!module.GetInterfaces().Contains(typeof(IModule))) return;
                var target = IoC.GetInstance(module) as IModule;
                target?.LoadModule();
            }
            catch (Exception e)
            {
                throw new LoadModuleException(module.Name, e.StackTrace);
            }
        });

        if (layout == null) return;

        var target = IoC.GetInstance(layout) as IModule;
        target?.LoadModule();
    }

    private static void LoadResources()
    {
        var resUri = new Uri("avares://A6ToolKits.Base/Resources.axaml");
        var resource = new ResourceInclude(resUri)
        {
            Source = resUri
        };

        Application.Current?.Resources.MergedDictionaries.Add(resource);
    }
}