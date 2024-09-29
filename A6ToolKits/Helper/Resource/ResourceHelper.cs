using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace A6ToolKits.Helper.Resource;

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


    public static T LoadResource<T>(string key)
    {
        try
        {
            var app = Application.Current;
            var theme = app?.ActualThemeVariant;
            var resource = (T)Application.Current?.FindResource(theme, key)!;
            return resource;
        }
        catch (Exception e)
        {
            throw new Exception($"Picture load error. Key:{key}", e);
        }
    }
}