using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.APIEndPoints;

public static class SensorAPI {
    public static void Configure(WebApplication app, IRailwayConfig config) {

        app.MapGet("/sensors", async () => Results.Ok(await config.Sensors.GetAllAsync()));

        app.MapGet("/sensors/{id}", async (string id) => {
            var sensor = await config.Sensors.GetByIDAsync(id);
            return sensor == null ? Results.NotFound() : Results.Ok(sensor);
        });

        app.MapPost("/sensors", async (Sensor sensor) => Results.Ok(await config.Sensors.AddAsync(sensor)));

        app.MapPut("/sensors/{id}", async (string id, Sensor sensor) => Results.Ok(await config.Sensors.UpdateAsync(sensor)));

        app.MapDelete("/sensors/{id}", async (string id) => Results.Ok(await config.Sensors.DeleteAsync(id)));

    }

}