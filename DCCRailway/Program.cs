using CommandLine;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using DCCRailway;
using DCCRailway.Railway;
using DCCRailway.WebApp.Components;
using Serilog.Configuration;
using Serilog.Core;

    const string consoleOutputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}|{AssemblyName}.{SourceContext}] {Message:lj} {Exception}{NewLine}";
    Parser.Default.ParseArguments<Options>(args)
          .WithParsed(options => {
               var loggerConfig =
                   new LoggerConfiguration()
                      .MinimumLevel.Warning()
                      .Enrich.FromLogContext()
                      .Enrich.WithAssemblyName()
                      .Enrich.WithProcessId()
                      .Enrich.WithThreadName()
                      .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day);

               // If option to write to the console is enabled, then add console logging
               if (options.Console) {
                   loggerConfig.WriteTo.Console(theme: AnsiConsoleTheme.Literate, outputTemplate: consoleOutputTemplate);
               }

               // If we are running in Debugger mode,then output to the Debug window
               if (System.Diagnostics.Debugger.IsAttached) {
                   loggerConfig.WriteTo.Debug();
               }

               // Set the minimum logging level
               var levelSwitch = new LoggingLevelSwitch(options.LogLevel);
               loggerConfig.MinimumLevel.ControlledBy(levelSwitch);
               var logger = loggerConfig.CreateLogger();

               // Validate the options provided
               try {
                   var path          = ValidatePath(options.Path);
                   var name          = ValidateName(path, options.Name);
                   var runWiThrottle = options.RunWiThrottle;
                   var clean         = options.Clean;

                   RunRailway(logger, path, name, clean, runWiThrottle);
               } catch (Exception ex) {
                   logger.Error("DCCRailway existing with a fatal error. : {0}",ex);
               }
           });

//
//  Launch and run the railway and run until it terminates.
//  ---------------------------------------------------------------------------------
    static void RunRailway(ILogger logger, string path, string name, bool clean, bool runWiThrottle) {
        var railway = new RailwayManager(logger, name, path, !clean, runWiThrottle);
        logger.Information("Starting the DCCRailway Manager.");
        railway.Start();
        logger.Information("WebApp finished. Closing down other services. ");
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