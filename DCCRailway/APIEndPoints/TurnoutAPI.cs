using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.APIEndPoints;

public static class TurnoutAPI {
    public static void Configure(WebApplication app, IRailwayConfig config) {

        app.MapGet("/turnouts", async () => Results.Ok(await config.TurnoutRepository.GetAllAsync()));

        app.MapGet("/turnouts/{id}", async (Guid id) => {
            var turnout = await config.TurnoutRepository.GetByIDAsync(id);
            return turnout == null ? Results.NotFound() : Results.Ok(turnout);
        });

        app.MapPost("/turnouts", async (Turnout turnout) => {
            if (turnout.Id == Guid.Empty) turnout.Id = Guid.NewGuid();
            return Results.Ok(await config.TurnoutRepository.AddAsync(turnout));
        });

        app.MapPut("/turnouts/{id}", async (Guid id, Turnout turnout) => Results.Ok(await config.TurnoutRepository.UpdateAsync(turnout)));

        app.MapDelete("/turnouts/{id}", async (Guid id) => Results.Ok(await config.TurnoutRepository.DeleteAsync(id)));

    }

}