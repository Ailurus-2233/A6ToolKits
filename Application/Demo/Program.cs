using System;
using Avalonia;

namespace UIDemo;

internal static class Program
{
    /// <summary>
    ///     程序入口，用于启动Bootstrapper的配置流程
    /// </summary>
    /// <param name="args"></param>
    [STAThread]
    public static void Main(string[] args)
    {
        new Bootstrapper().Run(args);
    }
    
    // 用于开发工具加载虚拟界面
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}