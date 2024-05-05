using Serilog;

namespace DCCRailway.LayoutService.StateManager;

public class LayoutStateService() {

    private          WebApplication    _app;
    private const    string            _defaultUrl  = "https://localhost";
    private const    int               _defaultPort = 5002;
    private readonly CancellationToken _cts         = new CancellationToken();

    public void Start(string? url, int? port) {

        var serviceUrl = $"{url ?? _defaultUrl}:{port ?? _defaultPort}";
        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseUrls(serviceUrl);
        _app = builder.Build();

        // Attach the APIs to the WebService
        // --------------------------------------------------------------

        // Configure the HTTP request pipeline.
        if (!_app.Environment.IsDevelopment()) {
            _app.UseExceptionHandler("/Error", createScopeForErrors: true);
            _app.UseHsts();
        }

        _app.UseHttpsRedirection();
        _app.UseSerilogRequestLogging();
        _app.UseStaticFiles();
        _app.UseAntiforgery();
        _app.StartAsync(_cts);
    }

    public void Stop() {
        _app.StopAsync(_cts);
    }
}