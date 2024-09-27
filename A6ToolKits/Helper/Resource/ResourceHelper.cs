using System;
using Avalonia;
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
        var target = Application.Current?.Resources[key];
        if (target is DrawingImage image)
        {
            return image;
        }
        else
        {
            throw new Exception("Error: Resource not found.");
        }
    }
}