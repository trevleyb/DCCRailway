using DCCRailway.Layout.Entities;
using DCCRailway.Layout.Entities.Collection;

namespace DCCRailway.WebApp.EndPoints;

public static class SensorAPI {
    public static void Configure(WebApplication app, ILayoutRepository<Sensor> entities) {
        app.MapGet("/layout/sensors/{id}", async (string id) => {
            var sensor = entities.GetByID(id);
            return await Task.FromResult(sensor == null ? Results.NotFound() : Results.Ok(sensor));
        });

        app.MapGet("/layout/sensors", async () => await Task.FromResult(Results.Ok(entities.GetAll())));
        app.MapPost("/layout/sensors",
                    async (Sensor sensor) => await Task.FromResult(Results.Ok(entities.Add(sensor))));
        app.MapPut("/layout/sensors/{id}",
                   async (string id, Sensor sensor) => await Task.FromResult(Results.Ok(entities.Update(sensor))));
        app.MapDelete("/layout/sensors/{id}",
                      async (string id) => await Task.FromResult(Results.Ok(entities.Delete(id))));
    }
}