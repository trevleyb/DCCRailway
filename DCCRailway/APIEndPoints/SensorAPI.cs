using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.APIEndPoints;

public static class SensorAPI {
    public static void Configure(WebApplication app, IRailwayConfig config) {

        app.MapGet("/sensors", async () => Results.Ok(await config.SensorRepository.GetAllAsync()));

        app.MapGet("/sensors/{id}", async (Guid id) => {
            var sensor = await config.SensorRepository.GetByIDAsync(id);
            return sensor == null ? Results.NotFound() : Results.Ok(sensor);
        });

        app.MapPost("/sensors", async (Sensor sensor) => {
            if (sensor.Id == Guid.Empty) sensor.Id = Guid.NewGuid();
            return Results.Ok(await config.SensorRepository.AddAsync(sensor));
        });

        app.MapPut("/sensors/{id}", async (Guid id, Sensor sensor) => Results.Ok(await config.SensorRepository.UpdateAsync(sensor)));

        app.MapDelete("/sensors/{id}", async (Guid id) => Results.Ok(await config.SensorRepository.DeleteAsync(id)));

    }

}