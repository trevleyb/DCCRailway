using DCCRailway.Common.Configuration;
using DCCRailway.Layout.Layout;
using DCCRailway.Layout.Layout.EndPoints;
using DCCRailway.Layout.StateManager;
using DCCRailway.Layout.StateManager.EndPoints;
using Microsoft.AspNetCore.Components.Web;

namespace DCCRailway.Layout;

public class LayoutService {

    private const string    defaultFile = "DCCRailway.Layout.json";
    private const string    baseUrl = "https://localhost";
    private const int       basePort = 5001;
    private static WebApplication  _app;
    private static readonly CancellationToken _cts = new CancellationToken();

    public string ServiceUrl { get; set; }

    public Task Start(string filename) => Start(baseUrl, basePort, filename);
    public Task Start(string url, int port) => Start(url, port, defaultFile);
    public Task Start(string url, int port, string filename) => Start($"{url ?? "https://localhost"}:{port}", filename);
    public Task Start(ServiceSetting settings) => Start(settings.ServiceURL, settings.ConfigFile ?? defaultFile);
    public Task Start(string serviceUrl, string configFile) {
        var layoutRepositoryManager = LayoutRepositoryManager.Load(configFile) ?? LayoutRepositoryManager.New();
        var layoutStateManager = new LayoutStateManager();

        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseUrls(serviceUrl);
        _app = builder.Build();

        // Attach the APIs to the WebService
        // --------------------------------------------------------------
        ServiceAPI.Configure(_app,_cts);
        BlockAPI.Configure(_app,layoutRepositoryManager.Blocks);
        SignalAPI.Configure(_app,layoutRepositoryManager.Signals);
        SensorAPI.Configure(_app,layoutRepositoryManager.Sensors);
        AccessoryApi.Configure(_app,layoutRepositoryManager.Accessories);
        TurnoutAPI.Configure(_app,layoutRepositoryManager.Turnouts);
        LocomotiveAPI.Configure(_app,layoutRepositoryManager.Locomotives);
        RouteAPI.Configure(_app,layoutRepositoryManager.Routes);
        StateAPI.Configure(_app,layoutStateManager);

        // Configure the HTTP request pipeline.
        if (!_app.Environment.IsDevelopment()) {
            _app.UseExceptionHandler("/Error", createScopeForErrors: true);
            _app.UseHsts();
        }

        _app.UseHttpsRedirection();
        _app.UseStaticFiles();
        return _app.StartAsync(_cts);
    }

    public void Stop() {
        _app.StopAsync(_cts);
    }
}