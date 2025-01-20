using A6ToolKits.Exceptions;

namespace A6ToolKits.Database.Exceptions;

/// <summary>
///     没有找到根元素异常，当在XML文件中没有找到根元素时抛出
/// </summary>
public class DataNotExistException(string targetXmlFile, string? details = null) :
    ExceptionBase(ErrorCode.InvalidOperation, targetXmlFile, details);