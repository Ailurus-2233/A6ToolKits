using System;

namespace A6ToolKits.Helper.Exceptions;

public class ConfigLoadException(string message) : Exception(message);