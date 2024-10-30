using Serilog;
using Serilog.Events;

namespace A6ToolKits.Logger;

/// <summary>
///     日志帮助类，用于初始化控制台日志
/// </summary>
public static class LoggerHelper
{
    /// <summary>
    ///     初始化控制台日志
    /// </summary>
    /// <param name="level">
    ///     日志级别，如果不指定则默认为 Information
    ///     可以通过调高日志级别来输出更多的日志信息
    /// </param>
    public static void InitializeConsoleLogger(LogEventLevel level = LogEventLevel.Information)
    {
        Log.Logger = level switch
        {
            LogEventLevel.Verbose => new LoggerConfiguration().WriteTo.Console().MinimumLevel.Verbose().CreateLogger(),
            LogEventLevel.Debug => new LoggerConfiguration().WriteTo.Console().MinimumLevel.Debug().CreateLogger(),
            LogEventLevel.Information => new LoggerConfiguration().WriteTo.Console().MinimumLevel.Information()
                .CreateLogger(),
            LogEventLevel.Warning => new LoggerConfiguration().WriteTo.Console().MinimumLevel.Warning().CreateLogger(),
            LogEventLevel.Error => new LoggerConfiguration().WriteTo.Console().MinimumLevel.Error().CreateLogger(),
            LogEventLevel.Fatal => new LoggerConfiguration().WriteTo.Console().MinimumLevel.Fatal().CreateLogger(),
            _ => new LoggerConfiguration().WriteTo.Console().MinimumLevel.Information().CreateLogger()
        };
    }
}