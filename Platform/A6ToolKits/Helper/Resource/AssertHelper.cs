using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace A6ToolKits.Helper.Resource;

/// <summary>
///     资源帮助类
/// </summary>
public static class AssertHelper
{
    /// <summary>
    ///     从 Avalonia应用资源 中加载图片
    /// </summary>
    /// <param name="resourceUri">
    ///     资源 URI
    /// </param>
    /// <returns>
    ///     返回图片
    /// </returns>
    public static Bitmap LoadImage(Uri resourceUri)
    {
        return new Bitmap(AssetLoader.Open(resourceUri));
    }
}