using DCCRailway.Layout.Collection;
using DCCRailway.Layout.Entities;

namespace DCCRailway.Railway.EndPoints;

public static class AccessoryApi {
    public static void Configure(WebApplication app, ILayoutRepository<Accessory> entities) {

        app.MapGet("/layout/accessories/{id}", async (string id) => {
            var accessory = await entities.GetByIDAsync(id);
            return accessory == null ? Results.NotFound() : Results.Ok(accessory);
        });

        app.MapGet("/layout/accessories", async () => await Task.FromResult(Results.Ok(entities.GetAllAsync())));
        app.MapPost("/layout/accessories", async (Accessory accessory) => Results.Ok(await entities.AddAsync(accessory)));
        app.MapPut("/layout/accessories/{id}", async (string id, Accessory accessory) => Results.Ok(await entities.UpdateAsync(accessory)));
        app.MapDelete("/layout/accessories/{id}", async (string id) => Results.Ok(await entities.DeleteAsync(id)));
    }
}