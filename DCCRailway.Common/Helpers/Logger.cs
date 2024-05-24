using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Serilog.Sinks.TestCorrelator;

// Lazy initialization of logger.
namespace DCCRailway.Common.Helpers;

public static class LoggerHelper {
    public static ILogger ConsoleLogger => new LoggerConfiguration().WriteTo.Console().CreateLogger();

    private static ILogger CreateLogger() {
        ILogger logger = new LoggerConfiguration().MinimumLevel.Verbose().Enrich.FromLogContext().Enrich.WithAssemblyName().Enrich.WithProcessId().Enrich.WithThreadName().WriteTo.Sink(new TestCorrelatorSink()).WriteTo.Debug(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}|{AssemblyName}.{SourceContext}] {Message:lj}|{Properties:lj}|{Exception}{NewLine}").WriteTo.Console(theme: AnsiConsoleTheme.Literate, outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}|{AssemblyName}.{SourceContext}] {Message:lj}|{Properties:lj}|{Exception}{NewLine}").WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day).CreateLogger();

        logger.Information("Logger initialised");
        return logger;
    }
}