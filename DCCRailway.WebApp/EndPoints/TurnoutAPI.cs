using DCCRailway.Layout.Entities;
using DCCRailway.Layout.Entities.Collection;

namespace DCCRailway.WebApp.EndPoints;

public static class TurnoutAPI {
    public static void Configure(WebApplication app, ILayoutRepository<Turnout> entities) {
        app.MapGet("/layout/turnouts/{id}", async (string id) => {
            var turnout = entities.GetByID(id);
            return await Task.FromResult(turnout == null ? Results.NotFound() : Results.Ok(turnout));
        });

        app.MapGet("/layout/turnouts", async () => await Task.FromResult(Results.Ok(entities.GetAll())));
        app.MapPost("/layout/turnouts", async (Turnout turnout) => await Task.FromResult(Results.Ok(entities.Add(turnout))));
        app.MapPut("/layout/turnouts/{id}", async (string id, Turnout turnout) => await Task.FromResult(Results.Ok(entities.Update(turnout))));
        app.MapDelete("/layout/turnouts/{id}", async (string id) => await Task.FromResult(Results.Ok(entities.Delete(id))));
    }
}