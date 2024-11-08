namespace A6ToolKits.Common.Exceptions;

public class AssetNotFoundException(Uri? uri, string? details = null) : 
    FrameworkExceptionBase(ErrorCode.InvalidArgument, $"Resource [{uri}] not found", details);