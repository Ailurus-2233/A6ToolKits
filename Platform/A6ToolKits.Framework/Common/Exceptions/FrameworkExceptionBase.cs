namespace A6ToolKits.Common.Exceptions;

public abstract class FrameworkExceptionBase(ErrorCode code, string information, string? details = null) : Exception(information)
{
    public ErrorCode Code { get; set; } = code;
    public string Information { get; set; } = information;
    public string? Details { get; set; } = details;

    public string FullMessage
    {
        get
        {
            var result = $"{Code.ToString()}:\n\tInformation:{Information}";
            if (!string.IsNullOrEmpty(Details))
                result += $"\n\tDetails:{Details}";
            return result;
        }
    }
}

public enum ErrorCode
{
    RuntimeError,
    InvalidOperation,
    InvalidArgument,
    InvalidState,
}