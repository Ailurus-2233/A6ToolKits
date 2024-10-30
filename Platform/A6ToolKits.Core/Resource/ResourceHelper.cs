using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Serilog;

namespace A6ToolKits.Resource;

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
    /// <exception cref="Exception"></exception>
    public static DrawingImage LoadImage(string key)
    {
        try
        {
            var image = (DrawingImage)Application.Current?.FindResource(key)!;
            return image;
        }
        catch (Exception e)
        {
            throw new Exception($"Picture load error. Key:{key}", e);
        }
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
    public static T? LoadResource<T>(string key)
    {
        try
        {
            var app = Application.Current;
            var resource = (T)app?.FindResource(key)!;
            return resource;
        }
        catch (Exception e)
        {
            throw new Exception($"Resource load error. Key:{key}", e);
        }
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
    public static object? TryLoadResource<T>(string key, out T? result)
    {
        var app = Application.Current;
        result = default;
        if (app != null)
        {
            var resource = app.FindResource(key);
            if (resource is UnsetValueType)
            {
                Log.Error("Resource load error. Key:{key}");
                return null;
            }

            try
            {
                result = (T)resource!;
            }
            catch (Exception)
            {
                Log.Error("The resource type is not matched. Key:{key}, Type:{0}", resource?.GetType());
            }

            return resource;
        }

        Log.Error("Application is not running.");
        return null;
    }
}