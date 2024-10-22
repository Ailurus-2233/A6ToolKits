﻿using A6ToolKits.Bootstrapper;
using A6ToolKits.Layout;
using A6ToolKits.Module;
using Avalonia.Controls;
using Avalonia.Styling;

namespace UIDemo;

public class Bootstrapper : BaseBootstrapper<App, Window>
{
    protected override ThemeVariant Theme { get; set; } = ThemeVariant.Light;
    
    /// <summary>
    ///     重写完成方法，在完成时设置主窗体
    /// </summary>
    public override void OnCompleted()
    {
        MainWindow = ModuleLoader.TryGetModule<LayoutModule>(out var layoutModule)
            ? layoutModule?.Window
            : new Window();
        
        base.OnCompleted();
    }
}