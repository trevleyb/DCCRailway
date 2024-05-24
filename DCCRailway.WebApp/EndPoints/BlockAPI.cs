using DCCRailway.Layout.Entities;
using DCCRailway.Layout.Entities.Collection;

namespace DCCRailway.WebApp.EndPoints;

public static class BlockAPI {
    public static void Configure(WebApplication app, ILayoutRepository<Block> entities) {
        app.MapGet("/layout/blocks/{id}", async (string id) => {
            var accessory = entities.GetByID(id);
            return await Task.FromResult(accessory == null ? Results.NotFound() : Results.Ok(accessory));
        });

        app.MapGet("/layout/blocks", async () => await Task.FromResult(entities));
        app.MapPost("/layout/blocks", async (Block block) => await Task.FromResult(Results.Ok(entities.Add(block))));
        app.MapPut("/layout/blocks/{id}", async (string id, Block block) => await Task.FromResult(Results.Ok(entities.Update(block))));
        app.MapDelete("/layout/blocks/{id}", async (string id) => await Task.FromResult(Results.Ok(entities.Delete(id))));
    }
}