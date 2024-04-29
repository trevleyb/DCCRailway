using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.APIEndPoints;

public static class SignalAPI {
    public static void Configure(WebApplication app, IRailwayConfig config) {

        app.MapGet("/signals", async () => Results.Ok(await config.Signals.GetAllAsync()));

        app.MapGet("/signals/{id}", async (string id) => {
            var signal = await config.Signals.GetByIDAsync(id);
            return signal == null ? Results.NotFound() : Results.Ok(signal);
        });

        app.MapPost("/signals", async (Signal signal) => Results.Ok(await config.Signals.AddAsync(signal)));

        app.MapPut("/signals/{id}", async (string id, Signal signal) => Results.Ok(await config.Signals.UpdateAsync(signal)));

        app.MapDelete("/signals/{id}", async (string id) => Results.Ok(await config.Signals.DeleteAsync(id)));

    }

}