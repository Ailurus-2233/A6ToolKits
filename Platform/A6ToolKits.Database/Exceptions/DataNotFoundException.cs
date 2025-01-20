using A6ToolKits.Exceptions;

namespace A6ToolKits.Database.Exceptions;

/// <summary>
/// 表示在数据库中未找到数据时引发的异常。
/// </summary>
public class DataNotFoundException : ExceptionBase
{
    /// <summary>
    /// 初始化 <see cref="DataNotFoundException"/> 类的新实例。
    /// </summary>
    /// <param name="information">错误信息。</param>
    /// <param name="details">错误详情。</param>
    public DataNotFoundException(string information, string? details = null)
        : base(ErrorCode.RuntimeError, information, details)
    {
    }
}

