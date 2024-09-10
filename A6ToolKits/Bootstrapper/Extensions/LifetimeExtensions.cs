using System;
using System.Reflection;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

namespace A6ToolKits.Bootstrapper.Extensions;

/// <summary>
///     Avalonia 生命周期扩展，为了拆分始化过程中的一部分操作
/// </summary>
public static class LifetimeExtensions
{
    /// <summary>
    ///     订阅全局事件
    /// </summary>
    /// <param name="lifetime">
    ///     桌面应用程序生命周期
    /// </param>
    public static void SubscribeGlobalEvents(this ClassicDesktopStyleApplicationLifetime lifetime)
    {
        var subscribeGlobalEventsMethod = typeof(ClassicDesktopStyleApplicationLifetime).GetMethod(
            "SubscribeGlobalEvents",
            BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
        subscribeGlobalEventsMethod?.Invoke(lifetime, null);
    }

    /// <summary>
    ///     准备生命周期
    /// </summary>
    /// <param name="builder">
    ///     Avalonia 应用程序构建器
    /// </param>
    /// <param name="args">
    ///     命令行参数
    /// </param>
    /// <param name="lifetimeBuilder">
    ///     Avalonia 生命周期构建器
    /// </param>
    /// <returns>
    ///     返回 Avalonia 生命周期
    /// </returns>
    /// <exception cref="MissingMethodException">
    ///     PrepareLifetime 方法未找到
    /// </exception>
    /// <exception cref="InvalidOperationException">
    ///     返回值无效
    /// </exception>
    public static ClassicDesktopStyleApplicationLifetime PrepareLifetime(
        AppBuilder builder,
        string[] args,
        Action<IClassicDesktopStyleApplicationLifetime>? lifetimeBuilder)
    {
        var extensionsType = typeof(ClassicDesktopStyleApplicationLifetimeExtensions);
        var prepareLifetimeMethod =
            extensionsType.GetMethod("PrepareLifetime", BindingFlags.NonPublic | BindingFlags.Static);

        if (prepareLifetimeMethod == null) throw new MissingMethodException("PrepareLifetime method not found.");

        return prepareLifetimeMethod.Invoke(null, [builder, args, lifetimeBuilder]) as
            ClassicDesktopStyleApplicationLifetime ?? throw new InvalidOperationException();
    }
}