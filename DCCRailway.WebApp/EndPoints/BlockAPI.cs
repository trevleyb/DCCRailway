using DCCRailway.Layout.Collection;
using DCCRailway.Layout.Entities;

namespace DCCRailway.WebApp.EndPoints;

public static class BlockAPI {
    public static void Configure(WebApplication app, ILayoutRepository<Block> entities) {
        app.MapGet("/layout/blocks/{id}", async (string id) => {
            var accessory = await entities.GetByIDAsync(id);
            return accessory == null ? Results.NotFound() : Results.Ok(accessory);
        });

        app.MapGet("/layout/blocks", async () => await Task.FromResult(entities));

        //app.MapGet("/layout/blocks", async () => await Task.FromResult(entities.GetAllAsync()));
        app.MapPost("/layout/blocks", async (Block block) => Results.Ok(await entities.AddAsync(block)));
        app.MapPut("/layout/blocks/{id}", async (string id, Block block) => Results.Ok(await entities.UpdateAsync(block)));
        app.MapDelete("/layout/blocks/{id}", async (string id) => Results.Ok(await entities.DeleteAsync(id)));
    }
}