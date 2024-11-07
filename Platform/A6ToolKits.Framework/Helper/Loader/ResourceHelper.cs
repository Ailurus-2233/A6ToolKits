using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace A6ToolKits.Helper.Loader;

/// <summary>
///     资源帮助类
/// </summary>
public static class ResourceHelper
{
    /// <summary>
    ///     从 Avalonia应用资源 中加载图片
    /// </summary>
    /// <param name="key">
    ///     资源 Key
    /// </param>
    /// <returns>
    ///     返回图片
    /// </returns>
    public static IImage? LoadImage(string key)
    {
        return Application.Current?.FindResource(key) as IImage;
    }


    /// <summary>
    ///     从 Avalonia应用资源 中加载资源
    /// </summary>
    /// <param name="key">
    ///     资源的 key
    /// </param>
    /// <typeparam name="T">
    ///     资源类型
    /// </typeparam>
    /// <returns>
    ///     资源对象
    /// </returns>
    /// <exception cref="Exception">
    ///     无法加载资源时抛出异常
    /// </exception>
    public static T? LoadResource<T>(string key) where T : class
    {
        var app = Application.Current;
        var resource = app?.FindResource(key) as T;
        return resource;
    }

    /// <summary>
    ///     尝试从 Avalonia应用资源 中加载资源，与 LoadResource 不同的是，当资源不存在时不会抛出异常
    /// </summary>
    /// <param name="key">
    ///     资源的 key
    /// </param>
    /// <param name="result">
    ///     加载的资源
    /// </param>
    /// <typeparam name="T">
    ///     资源类型
    /// </typeparam>
    /// <returns>
    ///     加载成功返回资源对象，否则返回 null
    /// </returns>
    public static object? TryLoadResource<T>(string key, out T? result) where T : class
    {
        var app = Application.Current;
        result = default;
        if (app == null) return null;

        var resource = app.FindResource(key);
        if (resource is UnsetValueType)
            return null;
        result = resource as T;
        return resource;
    }
}