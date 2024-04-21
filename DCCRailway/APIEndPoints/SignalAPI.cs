using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.Service.APIEndPoints;

public static class SignalAPI {
    public static void Configure(WebApplication app, IRailwayConfig config) {

            app.MapGet("/signals", async () => {
                // Code to fetch and return all signals
            });

            app.MapGet("/signals/{id}", async (int id) => {
                // Code to fetch and return a specific signal by id
            });

            app.MapPost("/signals", async (Signal signal) => {
                // Code to add a new signal
            });

            app.MapPut("/signals/{id}", async (int id, Signal signal) => {
                // Code to update a specific signal
            });

            app.MapDelete("/signals/{id}", async (int id) => {
                // Code to delete a specific signal
            });
    }

}