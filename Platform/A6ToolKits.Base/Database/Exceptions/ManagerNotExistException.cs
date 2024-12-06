﻿using A6ToolKits.Common;

namespace A6ToolKits.Database.Exceptions;

/// <summary>
///     抛出异常，无法找到指定数据库管理器
/// </summary>
public class ManagerNotExistException(string managerId, string? details = null) : 
    FrameworkExceptionBase(ErrorCode.InvalidArgument, $"Can not fount [{managerId}] database manager" ,details);