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
using _Imports = DCCRailway.Client._Imports;
using ILogger = Serilog.ILogger;
using Route = DCCRailway.Common.Entities.Route;

var log = ConfigureLogger();
LogStartupOptions(log, args);
var builder = WebApplication.CreateBuilder(args);
var railway = GetRailwayManager(log, args);
if (railway == null) return;

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents().AddInteractiveWebAssemblyComponents();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton(railway.Settings.Accessories);
builder.Services.AddSingleton(railway.Settings.Blocks);
builder.Services.AddSingleton(railway.Settings.Locomotives);
builder.Services.AddSingleton(railway.Settings.Routes);
builder.Services.AddSingleton(railway.Settings.Sensors);
builder.Services.AddSingleton(railway.Settings.Signals);
builder.Services.AddSingleton(railway.Settings.Turnouts);

var app = builder.Build();
ApiHelper.MapEntity<Accessories, Accessory>(app, "api/accessories");
ApiHelper.MapEntity<Blocks, Block>(app, "api/blocks");
ApiHelper.MapEntity<Locomotives, Locomotive>(app, "api/locomotives");
ApiHelper.MapEntity<Routes, Route>(app, "api/routes");
ApiHelper.MapEntity<Sensors, Sensor>(app, "api/sensors");
ApiHelper.MapEntity<Signals, Signal>(app, "api/signals");
ApiHelper.MapEntity<Turnouts, Turnout>(app, "api/turnouts");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseWebAssemblyDebugging();
} else {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode().AddInteractiveWebAssemblyRenderMode().AddAdditionalAssemblies(typeof(_Imports).Assembly);

log.Information("Starting DCCRailway services");
railway.Start();
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
    if (Debugger.IsAttached) loggerConfig.WriteTo.Debug();

    // Set the minimum logging level
    var levelSwitch = new LoggingLevelSwitch(LogEventLevel.Verbose);
    loggerConfig.MinimumLevel.ControlledBy(levelSwitch);
    return loggerConfig.CreateLogger();
}

// Log the startup options and parameters
// -------------------------------------------------------------
void LogStartupOptions(ILogger log, string[] args) {
    log.Information("Starting DCCRailway");
    if (args is { Length: > 0 }) {
        foreach (var arg in args) {
            log.Information($"arg: {arg}");
        }
    }
}

// get the Railway Configuration Data
// -------------------------------------------------------------
RailwayManager? GetRailwayManager(ILogger log, string[] args) {
    RailwayManager? railway = null;
    Parser.Default.ParseArguments<Options>(args).WithParsed(options => {
        // Validate the options provided and Run The Railway
        // --------------------------------------------------------------------
        try {
            railway = new RailwayManager(log);
            var path  = railway.ValidatePath(options.Path);
            var name  = railway.ValidateName(path, options.Name);
            var clean = options.Clean;
            if (clean) {
                railway.New(path, name);
            } else {
                railway.Load(path, name);
            }
        } catch (Exception ex) {
            Console.WriteLine(ex);
            railway = null;
        }
    });

    return railway;
}