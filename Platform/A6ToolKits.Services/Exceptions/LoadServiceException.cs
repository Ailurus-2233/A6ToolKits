using A6ToolKits.Exceptions;

namespace A6ToolKits.Services.Exceptions;

public class LoadServiceException(string information, string? details = null) : 
    ExceptionBase(ErrorCode.RuntimeError, information, details);