using Serilog;

namespace DCCRailway.Common.Utilities;

public static class Logger {
    private static ILogger _logger = CreateLogger();
    private static readonly object? _lock = new();

    public static ILogger Instance => _logger;
    public static ILogger Log      => Instance;

    public static ILogger CreateLogger() {
        if (_lock != null) {
            lock (_lock) {
                _logger = new LoggerConfiguration()
                         .MinimumLevel.Debug()
                         .Enrich.FromLogContext()
                         .Enrich.WithAssemblyName()
                         .Enrich.WithProcessId()
                         .Enrich.WithThreadName()
                         .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}|{AssemblyName}.{SourceContext}] {Message:lj}|{Properties:lj}|{Exception}{NewLine}")
                         .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day).CreateLogger();
            }
            _logger.Information("Logger initialised");
        }
        return _logger;
    }
}