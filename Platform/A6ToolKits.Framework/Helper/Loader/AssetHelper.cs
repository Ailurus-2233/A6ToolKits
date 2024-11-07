using A6ToolKits.Common.Exceptions;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Svg;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace A6ToolKits.Helper.Loader;

/// <summary>
///     资源帮助类
/// </summary>
public static class AssetHelper
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
    public static IImage LoadImage(Uri resourceUri)
    {
        using var stream = AssetLoader.Open(resourceUri);
        if (stream == null)
            throw new AssetNotFoundException(resourceUri);
        return new Bitmap(stream);
    }

    /// <summary>
    ///     从 Avalonia应用资源 中加载 SVG 图片
    /// </summary>
    /// <param name="resourceUri">
    ///     资源 URI
    /// </param>
    /// <returns>
    ///     返回图片
    /// </returns>
    public static IImage LoadSvgImage(Uri resourceUri)
    {
        using var stream = AssetLoader.Open(resourceUri);
        if (stream == null)
            throw new AssetNotFoundException(resourceUri);
        return new SvgImage { Source = SvgSource.Load(stream) };
    }


    /// <summary>
    ///     从 网络地址 加载图片
    /// </summary>
    /// <param name="uri">
    ///     网络地址
    /// </param>
    /// <returns>
    ///     返回图片的异步任务
    /// </returns>
    public static async Task<Bitmap?> LoadFromWeb(Uri uri)
    {
        using var httpClient = new HttpClient();
        try
        {
            var response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsByteArrayAsync();
            return new Bitmap(new MemoryStream(data));
        }
        catch (HttpRequestException)
        {
            throw new AssetNotFoundException(uri);
        }
    }
}