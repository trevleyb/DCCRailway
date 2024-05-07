namespace DCCRailway.Railway.EndPoints;

public static class ServiceAPI {
    public static void Configure(WebApplication app, CancellationToken cts) {
        app.MapDelete("/service", async () => Results.Ok(app.StopAsync(cts)));
    }
}