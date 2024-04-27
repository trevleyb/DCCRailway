using Serilog;

// Lazy initialization of logger.
namespace DCCRailway.Common.Helpers;
public static class Logger {
    private static readonly Lazy<ILogger> Lazy = new(() => CreateLogger());
    public static ILogger Instance => Lazy.Value;
    public static ILogger Log      => Instance;
    public static ILogger Context<T>() => Instance.ForContext<T>();

    private static ILogger CreateLogger() {
        ILogger logger;

        // Same configuration as before.
        logger = new LoggerConfiguration()
                 .MinimumLevel.Debug()
                 .Enrich.FromLogContext()
                 .Enrich.WithAssemblyName()
                 .Enrich.WithProcessId()
                 .Enrich.WithThreadName()
                 .WriteTo.Debug()
                 .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}|{AssemblyName}.{SourceContext}] {Message:lj}|{Properties:lj}|{Exception}{NewLine}")
                 .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day).CreateLogger();

        logger.Information("Logger initialised");
        return logger;
    }
}