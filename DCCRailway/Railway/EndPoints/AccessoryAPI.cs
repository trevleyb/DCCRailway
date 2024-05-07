using DCCRailway.Layout.Layout.Collection;
using DCCRailway.Layout.Layout.Entities;

namespace DCCRailway.Railway.EndPoints;

public static class AccessoryApi {
    public static void Configure(WebApplication app, ILayoutRepository<Accessory> entities) {

        app.MapGet("/accessories/{id}", async (string id) => {
            var accessory = await entities.GetByIDAsync(id);
            return accessory == null ? Results.NotFound() : Results.Ok(accessory);
        });

        app.MapGet("/accessories", async () => await Task.FromResult(Results.Ok(entities.GetAllAsync())));
        app.MapPost("/accessories", async (Accessory accessory) => Results.Ok(await entities.AddAsync(accessory)));
        app.MapPut("/accessories/{id}", async (string id, Accessory accessory) => Results.Ok(await entities.UpdateAsync(accessory)));
        app.MapDelete("/accessories/{id}", async (string id) => Results.Ok(await entities.DeleteAsync(id)));
    }
}