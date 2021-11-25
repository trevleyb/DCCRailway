using System.Runtime.CompilerServices;
using Serilog;

namespace DCCRailway.Core.Utilities {
	public static class Logger {
		private static ILogger _logger;
		private static readonly object? Lock = new object();

		public static ILogger Log {
			get {
				if (Lock != null && _logger == null) {
					lock (Lock) {
						_logger = new LoggerConfiguration()
							.MinimumLevel.Debug()
							.Enrich.FromLogContext()
							.Enrich.WithThreadId()
							.Enrich.WithAssemblyName()
							.Enrich.WithProcessId()
							.Enrich.WithThreadName()
							.WriteTo.Console(outputTemplate: $"[{{Timestamp:HH:mm:ss}} {{Level:u3}}|{{AssemblyName}}.{{SourceContext}}] {{Message:lj}}|{{Properties:lj}}|{{Exception}}{{NewLine}}")
							.WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
							.CreateLogger();
					}
					_logger.Information($"Logger initialised.");
				}
				return _logger;
			}
		}
	}
}