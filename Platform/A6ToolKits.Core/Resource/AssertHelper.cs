using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace A6ToolKits.Resource;

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
    
    
    /// <summary>
    ///     从 网络地址 加载图片
    /// </summary>
    /// <param name="url">
    ///     网络地址
    /// </param>
    /// <returns>
    ///     返回图片的异步任务
    /// </returns>
    public static async Task<Bitmap?> LoadFromWeb(Uri url)
    {
        using var httpClient = new HttpClient();
        try
        {
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsByteArrayAsync();
            return new Bitmap(new MemoryStream(data));
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"An error occurred while downloading image '{url}' : {ex.Message}");
            return null;
        }
    }
}