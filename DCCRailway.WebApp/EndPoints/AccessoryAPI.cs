using DCCRailway.Layout.Entities;
using DCCRailway.Layout.Entities.Collection;

namespace DCCRailway.WebApp.EndPoints;

public static class AccessoryApi {
    public static void Configure(WebApplication app, ILayoutRepository<Accessory> entities) {
        app.MapGet("/layout/accessories/{id}", async (string id) => {
            var accessory = entities.GetByID(id);
            return await Task.FromResult(accessory == null ? Results.NotFound() : Results.Ok(accessory));
        });

        app.MapGet("/layout/accessories", async () => await Task.FromResult(Results.Ok(entities.GetAll())));
        app.MapPost("/layout/accessories",
                    async (Accessory accessory) => await Task.FromResult(Results.Ok(entities.Add(accessory))));
        app.MapPut("/layout/accessories/{id}",
                   async (string id, Accessory accessory) =>
                       await Task.FromResult(Results.Ok(entities.Update(accessory))));
        app.MapDelete("/layout/accessories/{id}",
                      async (string id) => await Task.FromResult(Results.Ok(entities.Delete(id))));
    }
}