using DCCRailway.Layout.Layout.Collection;
using DCCRailway.Layout.Layout.Entities;

namespace DCCRailway.Layout.Layout.EndPoints;

public static class SignalAPI {
    public static void Configure(WebApplication app, ILayoutRepository<Signal> entities) {

        app.MapGet("/signals/{id}", async (string id) => {
            var signal = await entities.GetByIDAsync(id);
            return signal == null ? Results.NotFound() : Results.Ok(signal);
        });

        app.MapGet("/signals", async () => await Task.FromResult(Results.Ok(entities.GetAllAsync())));
        app.MapPost("/signals", async (Signal signal) => Results.Ok(await entities.AddAsync(signal)));
        app.MapPut("/signals/{id}", async (string id, Signal signal) => Results.Ok(await entities.UpdateAsync(signal)));
        app.MapDelete("/signals/{id}", async (string id) => Results.Ok(await entities.DeleteAsync(id)));
    }
}