namespace A6ToolKits.Exceptions;

/// <summary>
///     资源未找到异常，从资源加载器中未找到资源时抛出
/// </summary>
/// <param name="uri">
///     资源 URI
/// </param>
/// <param name="details">
///     详细信息
/// </param>
public class AssetNotFoundException(Uri? uri, string? details = null) :
    ExceptionBase(ErrorCode.InvalidArgument, $"Resource [{uri}] not found", details);