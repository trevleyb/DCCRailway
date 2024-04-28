using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.APIEndPoints;

public static class AccessoryApi {
    public static void Configure(WebApplication app, IRailwayConfig config) {

        app.MapGet("/accessories", async () => Results.Ok(await config.AccessoryRepository.GetAllAsync()));

        app.MapGet("/accessories/{id}", async (string id) => {
            var accessory = await config.AccessoryRepository.GetByIDAsync(id);
            return accessory == null ? Results.NotFound() : Results.Ok(accessory);
        });

        app.MapPost("/accessories", async (Accessory accessory) => Results.Ok(await config.AccessoryRepository.AddAsync(accessory)));

        app.MapPut("/accessories/{id}", async (string id, Accessory accessory) => Results.Ok(await config.AccessoryRepository.UpdateAsync(accessory)));

        app.MapDelete("/accessories/{id}", async (string id) => Results.Ok(await config.AccessoryRepository.DeleteAsync(id)));
    }
}