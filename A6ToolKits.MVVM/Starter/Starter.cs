using System.Reflection;
using A6ToolKits.MVVM.Container;
using A6ToolKits.MVVM.Starter.MainPage;
using Avalonia;
using Avalonia.Controls;

namespace A6ToolKits.MVVM.Starter;

internal static class Starter
{
    public static string Title { get; set; } = "Base Application";

    /// <summary>
    /// 定制平台中的注册类型，为一些平台服务注册做准备
    /// </summary>
    /// <param name="containerRegistry"> Prism 容器注册器 </param>
    public static void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // 由 IoC 类接管 DryIoc 容器
        IoC.SetContainer(containerRegistry);
    }

    /// <summary>
    /// 获取启动实例
    /// </summary>
    /// <returns> 平台MainWindow实例 </returns>
    public static AvaloniaObject GetStartInstance()
    {
        var window = IoC.GetInstance<MainWindow>();
        if (window.DataContext is MainWindowModel model) model.Title = Title;
        return window;
    }

    /// <summary>
    /// 配置ViewModel定位器
    /// </summary>
    public static void ConfigureViewModelLocator()
    {
        
    }
    
    /// <summary>
    /// 加载平台模块
    /// </summary>
    public static void LoadModule(IModuleCatalog moduleCatalog)
    {
        ModuleHelper.SetModuleCatalog(moduleCatalog);
    }
    
    public static void SetDisplayView(UserControl view)
    {
        var regionManager = IoC.Get<IRegionManager>();
        var region = regionManager.Regions["DisplayRegion"];
        region.Add(view);
    }
}