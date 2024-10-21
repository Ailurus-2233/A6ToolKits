using System;
namespace A6ToolKits.Common.Exceptions;

/// <summary>
///     TypeNotFoundException is thrown when a type is not found.
/// </summary>
public class TypeNotFoundException : Exception
{
    /// <summary>
    ///     Initializes a new instance of the TypeNotFoundException class.
    /// </summary>
    public TypeNotFoundException() : base()
    {
    }

    /// <summary>
    ///    Initializes a new instance of the TypeNotFoundException class.
    /// </summary>
    /// <param name="message">
    ///     The message that describes the error.
    /// </param>
    public TypeNotFoundException(string message) : base(message)
    {
    }
}