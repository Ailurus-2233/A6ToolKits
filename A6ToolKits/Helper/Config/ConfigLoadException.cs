﻿using System;

namespace A6ToolKits.Helper.Config;

/// <summary>
///     配置加载异常
/// </summary>
/// <param name="message">
///     异常消息
/// </param>
public class ConfigLoadException(string message) : Exception(message);