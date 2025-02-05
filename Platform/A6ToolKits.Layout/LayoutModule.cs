using A6ToolKits.ApplicationController;
using A6ToolKits.Attributes;
using A6ToolKits.Container;
using A6ToolKits.Layout.Generator;
using A6ToolKits.Modules;
using Avalonia;
using Avalonia.Controls;

namespace A6ToolKits.Layout;

/// <summary>
///     布局模块，如果加载该模块将会基于配置文件自动加载窗口
/// </summary>
[AutoRegister(typeof(LayoutModule), RegisterType.Singleton)]
public sealed class LayoutModule : ModuleBase<LayoutConfigItem>
{
    private static WindowGenerator Generator => WindowGenerator.Instance;
    
    /// <summary>
    ///     初始化布局模块，加载布局配置文件
    /// </summary>
    public override void Initialize()
    {
        SetMainWindow();
        AddColorResources();
    }

    /// <summary>
    ///     设置主窗口
    /// </summary>
    public void SetMainWindow()
    {
        var controller = IoC.GetInstance<IApplicationController>();
        if (controller != null)
        {
            controller.MainWindow = Generator.GenerateWindow(Config);
            controller.Theme = WindowConfig.Instance.Theme;
        }
        else
        {
            throw new NullReferenceException("Cannot get window controller");
        }
    }

    private void AddColorResources()
    {
        var resource = new ResourceDictionary
        {
            { "PrimaryColor", WindowConfig.Instance.PrimaryColor },
            { "SecondaryColor", WindowConfig.Instance.SecondaryColor },
            { "TertiaryColor", WindowConfig.Instance.TertiaryColor }
        };
        Application.Current?.Resources.MergedDictionaries.Add(resource);
    }
}