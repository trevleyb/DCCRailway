using DCCRailway.Layout.Layout.Collection;
using DCCRailway.Layout.Layout.Entities;

namespace DCCRailway.Railway.EndPoints;

public static class SensorAPI {
    public static void Configure(WebApplication app, ILayoutRepository<Sensor> entities) {

        app.MapGet("/sensors/{id}", async (string id) => {
            var sensor = await entities.GetByIDAsync(id);
            return sensor == null ? Results.NotFound() : Results.Ok(sensor);
        });

        app.MapGet("/sensors", async () => await Task.FromResult(Results.Ok(entities.GetAllAsync())));
        app.MapPost("/sensors", async (Sensor sensor) => Results.Ok(await entities.AddAsync(sensor)));
        app.MapPut("/sensors/{id}", async (string id, Sensor sensor) => Results.Ok(await entities.UpdateAsync(sensor)));
        app.MapDelete("/sensors/{id}", async (string id) => Results.Ok(await entities.DeleteAsync(id)));

    }

}