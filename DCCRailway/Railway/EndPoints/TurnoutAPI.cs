using DCCRailway.Layout.Layout.Collection;
using DCCRailway.Layout.Layout.Entities;

namespace DCCRailway.Railway.EndPoints;

public static class TurnoutAPI {
    public static void Configure(WebApplication app, ILayoutRepository<Turnout> entities) {

        app.MapGet("/turnouts/{id}", async (string id) => {
            var turnout = await entities.GetByIDAsync(id);
            return turnout == null ? Results.NotFound() : Results.Ok(turnout);
        });

        app.MapGet("/turnouts", async () => await Task.FromResult(Results.Ok(entities.GetAllAsync())));
        app.MapPost("/turnouts", async (Turnout turnout) => Results.Ok(await entities.AddAsync(turnout)));
        app.MapPut("/turnouts/{id}", async (string id, Turnout turnout) => Results.Ok(await entities.UpdateAsync(turnout)));
        app.MapDelete("/turnouts/{id}", async (string id) => Results.Ok(await entities.DeleteAsync(id)));

    }
}