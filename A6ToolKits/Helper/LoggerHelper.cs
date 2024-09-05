using Serilog;
using Serilog.Events;

namespace A6ToolKits.Helper;

public static class LoggerHelper
{
    public static void InitializeConsoleLogger(LogEventLevel level = LogEventLevel.Information)
    {
        Log.Logger = level switch
        {
            LogEventLevel.Verbose => new LoggerConfiguration().WriteTo.Console().MinimumLevel.Verbose().CreateLogger(),
            LogEventLevel.Debug => new LoggerConfiguration().WriteTo.Console().MinimumLevel.Debug().CreateLogger(),
            LogEventLevel.Information => new LoggerConfiguration().WriteTo.Console().MinimumLevel.Information().CreateLogger(),
            LogEventLevel.Warning => new LoggerConfiguration().WriteTo.Console().MinimumLevel.Warning().CreateLogger(),
            LogEventLevel.Error => new LoggerConfiguration().WriteTo.Console().MinimumLevel.Error().CreateLogger(),
            LogEventLevel.Fatal => new LoggerConfiguration().WriteTo.Console().MinimumLevel.Fatal().CreateLogger(),
            _ => new LoggerConfiguration().WriteTo.Console().MinimumLevel.Information().CreateLogger()
        };
    }
}