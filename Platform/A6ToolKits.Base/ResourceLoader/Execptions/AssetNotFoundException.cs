using A6ToolKits.Common;

namespace A6ToolKits.ResourceLoader.Execptions;

public class AssetNotFoundException(Uri? uri, string? details = null) :
    FrameworkExceptionBase(ErrorCode.InvalidArgument, $"Resource [{uri}] not found", details);