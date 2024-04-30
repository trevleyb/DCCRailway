using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.APIEndPoints;

public static class TurnoutAPI {
    public static void Configure(WebApplication app, IRailwayConfig config) {

        app.MapGet("/turnouts", async () => await Task.FromResult(Results.Ok(config.Turnouts.GetAllAsync())));

        app.MapGet("/turnouts/{id}", async (string id) => {
            var turnout = await config.Turnouts.GetByIDAsync(id);
            return turnout == null ? Results.NotFound() : Results.Ok(turnout);
        });

        app.MapPost("/turnouts", async (Turnout turnout) => Results.Ok(await config.Turnouts.AddAsync(turnout)));

        app.MapPut("/turnouts/{id}", async (string id, Turnout turnout) => Results.Ok(await config.Turnouts.UpdateAsync(turnout)));

        app.MapDelete("/turnouts/{id}", async (string id) => Results.Ok(await config.Turnouts.DeleteAsync(id)));

    }

}