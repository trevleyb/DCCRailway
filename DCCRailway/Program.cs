using CommandLine;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using DCCRailway;
using DCCRailway.Railway;
using DCCRailway.WebApp.Components;
using Serilog.Core;

    const string consoleOutputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}|{AssemblyName}.{SourceContext}] {Message:lj}|{Properties:lj}|{Exception}{NewLine}";
    Parser.Default.ParseArguments<Options>(args)
          .WithParsed(options => {
               var loggerConfig =
                   new LoggerConfiguration()
                      .MinimumLevel.Warning()
                      .Enrich.FromLogContext()
                      .Enrich.WithAssemblyName()
                      .Enrich.WithProcessId()
                      .Enrich.WithThreadName()
                      .WriteTo.Debug()
                      .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day);

               if (options.Console) {
                   loggerConfig.WriteTo.Console(theme: AnsiConsoleTheme.Literate, outputTemplate: consoleOutputTemplate);
               }
               if (options.Verbose) {
                   loggerConfig.MinimumLevel.Debug();
               }
               var logger = loggerConfig.CreateLogger();

               var path          = ValidatePath(options.Path);
               var name          = ValidateName(path, options.Name);
               var runWiThrottle = options.RunWiThrottle;
               var clean         = options.Clean;
               RunRailway(logger, path, name, runWiThrottle);
           });

//
//  Launch and run the railway and run until it terminates.
//  ---------------------------------------------------------------------------------
    static void RunRailway(ILogger logger, string path, string name, bool runWiThrottle, bool clean = false) {
        logger.Information("Starting the DCCRailway Manager.");
        var railway = new RailwayManager(logger, name, path, runWiThrottle, !clean);
        railway.Start();
        railway.Stop();
        logger.Information("DCCRailway Manager finished.");
    }

//
//  Validate that the name of the layout exists in the path, or if not, then we
//  look for it in the provided path, and if it still does not exist then return
//  a default name or set the layout to this name.
//  ---------------------------------------------------------------------------------
    static string ValidateName(string path, string? name) {
        return name ?? FindConfigFile(path) ?? "DCCRailway";
    }

//
//  Look for the config file in the specified folder.
//  ---------------------------------------------------------------------------------
    static string? FindConfigFile(string path) {
        foreach (var file in Directory.GetFiles(path)) {
            if (file.EndsWith(".Settings.json", StringComparison.InvariantCultureIgnoreCase)) return file;
        }
        return null;
    }

//
//  Validate that the Path provided is a Valid Path. If it does not exist
//  then create the path if we can.
//  -----------------------------------------------------------------------
    static string ValidatePath(string path) {
        if (Directory.Exists(path)) return path;
        try {
            var dirInfo = Directory.CreateDirectory(path);
            if (dirInfo.Exists) return path;
            throw new Exception("Failed to find configuration directory or create directory.");
        } catch (Exception ex) {
            throw new Exception("Failed to find configuration directory or create directory.", ex);
        }
    }