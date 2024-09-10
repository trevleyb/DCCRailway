using System.Diagnostics;
using CommandLine;
using DCCRailway;
using DCCRailway.Common.Entities;
using DCCRailway.Components;
using DCCRailway.Helpers;
using DCCRailway.Managers;
using MudBlazor.Services;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using ILogger = Serilog.ILogger;

var log = ConfigureLogger();
LogStartupOptions(log, args);
var builder = WebApplication.CreateBuilder(args);

// Create the DCCRailway Manager
// ----------------------------------------------------------
var railway = GetRailwayManager(log, args);
if (railway == null) return;

// Add MudBlazor services
builder.Services.AddMudServices();
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton(railway.Settings.Accessories);
builder.Services.AddSingleton(railway.Settings.Blocks);
builder.Services.AddSingleton(railway.Settings.Locomotives);
builder.Services.AddSingleton(railway.Settings.TrackRoutes);
builder.Services.AddSingleton(railway.Settings.Sensors);
builder.Services.AddSingleton(railway.Settings.Signals);
builder.Services.AddSingleton(railway.Settings.Turnouts);

var app = builder.Build();
ApiHelper.MapEntity<Accessories, Accessory>(app, "api/accessories");
ApiHelper.MapEntity<Blocks, Block>(app, "api/blocks");
ApiHelper.MapEntity<Locomotives, Locomotive>(app, "api/locomotives");
ApiHelper.MapEntity<TrackRoutes, TrackRoute>(app, "api/routes");
ApiHelper.MapEntity<Sensors, Sensor>(app, "api/sensors");
ApiHelper.MapEntity<Signals, Signal>(app, "api/signals");
ApiHelper.MapEntity<Turnouts, Turnout>(app, "api/turnouts");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

var urls = builder.Configuration["urls"] ?? "http://localhost:5000"; // default fallback

log.Information("Starting DCCRailway services");
railway.Start(urls);
log.Information("Starting DCCRailway WebServer");
app.Run();
log.Information("Stopping DCCRailway services");
railway.Stop();
log.Information("Finished.");

// Setup the Logger
// -------------------------------------------------------------
ILogger ConfigureLogger() {
    var loggerConfig = new LoggerConfiguration().Enrich.FromLogContext().Enrich.WithAssemblyName().WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day);

    // If we are running in Debugger mode,then output to the Debug window
    if (Debugger.IsAttached) loggerConfig.WriteTo.Console();

    // Set the minimum logging level
    var levelSwitch = new LoggingLevelSwitch(LogEventLevel.Verbose);
    loggerConfig.MinimumLevel.ControlledBy(levelSwitch);
    return loggerConfig.CreateLogger();
}

// Log the startup options and parameters
// -------------------------------------------------------------
void LogStartupOptions(ILogger logger, string[] args) {
    logger.Information("Starting DCCRailway");
    if (args is { Length: > 0 }) {
        foreach (var arg in args) {
            logger.Information($"arg: {arg}");
        }
    }
}

// get the Railway Configuration Data
// -------------------------------------------------------------
RailwayManager? GetRailwayManager(ILogger logger, string[] args) {
    RailwayManager? railwayManager = null;
    Parser.Default.ParseArguments<Options>(args).WithParsed(options => {
        // Validate the options provided and Run The Railway
        // --------------------------------------------------------------------
        try {
            railwayManager = new RailwayManager(logger);
            var path  = railwayManager.ValidatePath(options.Path);
            var name  = railwayManager.ValidateName(path, options.Name);
            var clean = options.Clean;
            if (clean) {
                railwayManager.New(path, name);
            } else {
                railwayManager.Load(path, name);
            }
        } catch (Exception ex) {
            Console.WriteLine(ex);
            railwayManager = null;
        }
    });

    return railwayManager;
}