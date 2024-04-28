using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.APIEndPoints;

public static class TurnoutAPI {
    public static void Configure(WebApplication app, IRailwayConfig config) {

        app.MapGet("/turnouts", async () => Results.Ok(await config.TurnoutRepository.GetAllAsync()));

        app.MapGet("/turnouts/{id}", async (string id) => {
            var turnout = await config.TurnoutRepository.GetByIDAsync(id);
            return turnout == null ? Results.NotFound() : Results.Ok(turnout);
        });

        app.MapPost("/turnouts", async (Turnout turnout) => Results.Ok(await config.TurnoutRepository.AddAsync(turnout)));

        app.MapPut("/turnouts/{id}", async (string id, Turnout turnout) => Results.Ok(await config.TurnoutRepository.UpdateAsync(turnout)));

        app.MapDelete("/turnouts/{id}", async (string id) => Results.Ok(await config.TurnoutRepository.DeleteAsync(id)));

    }

}