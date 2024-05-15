using DCCRailway.Layout.Collection;
using DCCRailway.Layout.Entities;

namespace DCCRailway.WebApp.EndPoints;

public static class TurnoutAPI {
    public static void Configure(WebApplication app, ILayoutRepository<Turnout> entities) {
        app.MapGet("/layout/turnouts/{id}", async (string id) => {
            var turnout = await entities.GetByIDAsync(id);
            return turnout == null ? Results.NotFound() : Results.Ok(turnout);
        });

        app.MapGet("/layout/turnouts", async () => await Task.FromResult(Results.Ok(entities.GetAllAsync())));
        app.MapPost("/layout/turnouts", async (Turnout turnout) => Results.Ok(await entities.AddAsync(turnout)));
        app.MapPut("/layout/turnouts/{id}", async (string id, Turnout turnout) => Results.Ok(await entities.UpdateAsync(turnout)));
        app.MapDelete("/layout/turnouts/{id}", async (string id) => Results.Ok(await entities.DeleteAsync(id)));
    }
}