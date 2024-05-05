using DCCRailway.LayoutService.Layout.Collection;
using DCCRailway.LayoutService.Layout.Entities;

namespace DCCRailway.LayoutService.Layout.EndPoints;

public static class BlockAPI {
    public static void Configure(WebApplication app, ILayoutRepository<Block> entities) {

        app.MapGet("/blocks/{id}", async (string id) => {
            var accessory = await entities.GetByIDAsync(id);
            return accessory == null ? Results.NotFound() : Results.Ok(accessory);
        });

        app.MapGet("/blocks", async () => await Task.FromResult(Results.Ok(entities.GetAllAsync())));
        app.MapPost("/blocks", async (Block block) => Results.Ok(await entities.AddAsync(block)));
        app.MapPut("/blocks/{id}", async (string id, Block block) => Results.Ok(await entities.UpdateAsync(block)));
        app.MapDelete("/blocks/{id}", async (string id) => Results.Ok(await entities.DeleteAsync(id)));
    }
}