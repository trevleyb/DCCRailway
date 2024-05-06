using DCCRailway.Common.Configuration;
using DCCRailway.WebApp.Components;

namespace DCCRailway.WebApp;

public class LayoutWebApp {

    private const           string            defaultFile = "DCCRailway.WebApp.json";
    private const           string            baseUrl     = "https://localhost";
    private const           int               basePort    = 5000;
    private static          WebApplication    _app;

    public void Stop() { }

    public Task Start(string filename) => Start(baseUrl, basePort, filename);
    public Task Start(string url, int port) => Start(url, port, defaultFile);
    public Task Start(string url, int port, string filename) => Start($"{url ?? "https://localhost"}:{port}", filename);
    public Task Start(ServiceSetting settings) => Start(settings.ServiceURL, settings.ConfigFile ?? defaultFile);
    public Task Start(string serviceUrl, string configFile) {

        var builder = WebApplication.CreateBuilder();

        // Add services to the container.
        builder.Services.AddRazorComponents()
               .AddInteractiveServerComponents();

        builder.WebHost.UseUrls(serviceUrl);
        _app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!_app.Environment.IsDevelopment()) {
            _app.UseExceptionHandler("/Error", createScopeForErrors: true);
            _app.UseHsts();
        }

        _app.UseHttpsRedirection();
        _app.UseStaticFiles();
        _app.UseAntiforgery();
        _app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        _app.Run();
        return Task.CompletedTask;
    }
}