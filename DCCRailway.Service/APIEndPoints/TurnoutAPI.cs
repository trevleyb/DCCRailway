using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.Service.APIEndPoints;

public static class TurnoutAPI {
    public static void Configure(WebApplication app, IRailwayConfig config) {

            app.MapGet("/turnouts", async () => {
                // Code to fetch and return all turnouts
            });

            app.MapGet("/turnouts/{id}", async (int id) => {
                // Code to fetch and return a specific turnout by id
            });

            app.MapPost("/turnouts", async (Turnout turnout) => {
                // Code to add a new turnout
            });

            app.MapPut("/turnouts/{id}", async (int id, Turnout turnout) => {
                // Code to update a specific turnout
            });

            app.MapDelete("/turnouts/{id}", async (int id) => {
                // Code to delete a specific turnout

            });
    }

}