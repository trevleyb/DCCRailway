using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.APIEndPoints;

public static class SensorAPI {
    public static void Configure(WebApplication app, IRailwayConfig config) {

            app.MapGet("/sensors", async () => {
                // Code to fetch and return all sensors
            });

            app.MapGet("/sensors/{id}", async (int id) => {
                // Code to fetch and return a specific sensor by id
            });

            app.MapPost("/sensors", async (Sensor sensor) => {
                // Code to add a new sensor
            });

            app.MapPut("/sensors/{id}", async (int id, Sensor sensor) => {
                // Code to update a specific sensor
            });

            app.MapDelete("/sensors/{id}", async (int id) => {
                // Code to delete a specific sensor
            });
    }

}