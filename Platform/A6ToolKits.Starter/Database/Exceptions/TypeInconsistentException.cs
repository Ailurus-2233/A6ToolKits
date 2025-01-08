using A6ToolKits.Common;
using A6ToolKits.Database.Enums;

namespace A6ToolKits.Database.Exceptions;

/// <summary>
///     类型不一致异常，用于表示类型与列类型不一致
/// </summary>
/// <param name="columnType">
///     列类型
/// </param>
/// <param name="type">
///     实际类型
/// </param>
/// <param name="details">
///     异常详细信息
/// </param>
public class TypeInconsistentException(ColumnType columnType, Type type, string? details = null)
    : FrameworkExceptionBase(ErrorCode.InvalidArgument, $"Type: {type} inconsistent with ColumnType: {columnType}",
        details);