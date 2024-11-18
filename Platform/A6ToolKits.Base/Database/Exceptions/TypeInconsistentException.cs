using A6ToolKits.Common;
using A6ToolKits.Database.Enums;

namespace A6ToolKits.Database.Exceptions;

public class TypeInconsistentException(ColumnType columnType, Type type, string? details = null)
    : FrameworkExceptionBase(ErrorCode.InvalidArgument, $"Type: {type} inconsistent with ColumnType: {columnType}",
        details);