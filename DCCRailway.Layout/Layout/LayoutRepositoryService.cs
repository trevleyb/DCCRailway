using DCCRailway.LayoutService.Layout.EndPoints;

namespace DCCRailway.LayoutService.Layout;

public class LayoutRepositoryService {

    public LayoutRepositoryService(string? filename = "") {
        ServiceUrl = $"https://localhost:5001";
        _cfg = filename ?? "DCCRailway.Layout.json";
    }

    public LayoutRepositoryService(string? url = null, int? port = null, string? filename = "") {
        ServiceUrl = $"{url ?? "https://localhost"}:{port ?? 5001}";
        _cfg = filename ?? "DCCRailway.Layout.json";
    }

    private string _cfg;
    private WebApplication _app;
    private readonly CancellationToken _cts = new CancellationToken();

    public string ServiceUrl { get; set; }

    public void Start() {
        var layoutManager = LayoutRepositoryManager.Load(_cfg) ?? LayoutRepositoryManager.New();
        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseUrls(ServiceUrl);
        _app = builder.Build();

        // Attach the APIs to the WebService
        // --------------------------------------------------------------
        BlockAPI.Configure(_app,layoutManager.Blocks);
        SignalAPI.Configure(_app,layoutManager.Signals);
        SensorAPI.Configure(_app,layoutManager.Sensors);
        AccessoryApi.Configure(_app,layoutManager.Accessories);
        TurnoutAPI.Configure(_app,layoutManager.Turnouts);
        LocomotiveAPI.Configure(_app,layoutManager.Locomotives);
        RouteAPI.Configure(_app,layoutManager.Routes);

        // Configure the HTTP request pipeline.
        if (!_app.Environment.IsDevelopment()) {
            _app.UseExceptionHandler("/Error", createScopeForErrors: true);
            _app.UseHsts();
        }

        _app.UseHttpsRedirection();
        _app.UseStaticFiles();
        _app.StartAsync(_cts);
    }

    public void Stop() {
        _app.StopAsync(_cts);
    }
}