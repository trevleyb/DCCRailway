using DCCRailway.Layout.Collection;
using DCCRailway.Layout.Entities;

namespace DCCRailway.WebApp.EndPoints;

public static class SignalAPI {
    public static void Configure(WebApplication app, ILayoutRepository<Signal> entities) {
        app.MapGet("/layout/signals/{id}", async (string id) => {
            var signal = await entities.GetByIDAsync(id);
            return signal == null ? Results.NotFound() : Results.Ok(signal);
        });

        app.MapGet("/layout/signals", async () => await Task.FromResult(Results.Ok(entities.GetAllAsync())));
        app.MapPost("/layout/signals", async (Signal signal) => Results.Ok(await entities.AddAsync(signal)));
        app.MapPut("/layout/signals/{id}", async (string id, Signal signal) => Results.Ok(await entities.UpdateAsync(signal)));
        app.MapDelete("/layout/signals/{id}", async (string id) => Results.Ok(await entities.DeleteAsync(id)));
    }
}