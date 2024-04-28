using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.APIEndPoints;

public static class BlockAPI {
    public static void Configure(WebApplication app, IRailwayConfig config) {

        app.MapGet("/blocks", async () => Results.Ok(await config.BlockRepository.GetAllAsync()));

        app.MapGet("/blocks/{id}", async (string id) => {
            var accessory = await config.BlockRepository.GetByIDAsync(id);
            return accessory == null ? Results.NotFound() : Results.Ok(accessory);
        });

        app.MapPost("/blocks", async (Block block) => Results.Ok(await config.BlockRepository.AddAsync(block)));

        app.MapPut("/blocks/{id}", async (string id, Block block) => Results.Ok(await config.BlockRepository.UpdateAsync(block)));

        app.MapDelete("/blocks/{id}", async (string id) => Results.Ok(await config.BlockRepository.DeleteAsync(id)));
    }
}