using DCCRailway.Layout.Entities;
using DCCRailway.Layout.Entities.Collection;

namespace DCCRailway.WebApp.EndPoints;

public static class SignalAPI {
    public static void Configure(WebApplication app, ILayoutRepository<Signal> entities) {
        app.MapGet("/layout/signals/{id}", async (string id) => {
            var signal = entities.GetByID(id);
            return await Task.FromResult(signal == null ? Results.NotFound() : Results.Ok(signal));
        });

        app.MapGet("/layout/signals", async () => await Task.FromResult(Results.Ok(entities.GetAll())));
        app.MapPost("/layout/signals",
                    async (Signal signal) => await Task.FromResult(Results.Ok(entities.Add(signal))));
        app.MapPut("/layout/signals/{id}",
                   async (string id, Signal signal) => await Task.FromResult(Results.Ok(entities.Update(signal))));
        app.MapDelete("/layout/signals/{id}",
                      async (string id) => await Task.FromResult(Results.Ok(entities.Delete(id))));
    }
}