namespace A6ToolKits.Exceptions
{
    /// <summary>
    ///     框架异常基类，用于记录框架内部异常
    /// </summary>
    /// <param name="code"></param>
    /// <param name="information"></param>
    /// <param name="details"></param>
    public abstract class ExceptionBase(ErrorCode code, string information, string? details = null) : Exception(information)
    {
        /// <summary>
        ///     错误码，用于标识错误类型
        /// </summary>
        public ErrorCode Code { get; set; } = code;

        /// <summary>
        ///     错误信息
        /// </summary>
        public string Information { get; set; } = information;

        /// <summary>
        ///     错误详细信息
        /// </summary>
        public string? Details { get; set; } = details;

        /// <summary>
        ///     完整错误信息
        /// </summary>
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

    /// <summary>
    ///     错误码
    /// </summary>
    public enum ErrorCode
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        RuntimeError,
        InvalidOperation,
        InvalidArgument,
        InvalidState
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }
}
