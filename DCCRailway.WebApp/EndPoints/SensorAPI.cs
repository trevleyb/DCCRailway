using DCCRailway.Layout.Entities;
using DCCRailway.Layout.Entities.Collection;

namespace DCCRailway.WebApp.EndPoints;

public static class SensorAPI {
    public static void Configure(WebApplication app, ILayoutRepository<Sensor> entities) {
        app.MapGet("/layout/sensors/{id}", async (string id) => {
            var sensor = await entities.GetByIDAsync(id);
            return sensor == null ? Results.NotFound() : Results.Ok(sensor);
        });

        app.MapGet("/layout/sensors", async () => await Task.FromResult(Results.Ok(entities.GetAllAsync())));
        app.MapPost("/layout/sensors", async (Sensor sensor) => Results.Ok(await entities.AddAsync(sensor)));
        app.MapPut("/layout/sensors/{id}", async (string id, Sensor sensor) => Results.Ok(await entities.UpdateAsync(sensor)));
        app.MapDelete("/layout/sensors/{id}", async (string id) => Results.Ok(await entities.DeleteAsync(id)));
    }
}