using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;

namespace A6ToolKits.Helper.Resource;

/// <summary>
///     Web资源帮助类
/// </summary>
public class WebAssertHelper
{
    
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