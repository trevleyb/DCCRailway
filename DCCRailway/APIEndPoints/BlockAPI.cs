using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.APIEndPoints;

public static class BlockAPI {
    public static void Configure(WebApplication app, IRailwayConfig config) {

            app.MapGet("/blocks", async () => {
                // Code to fetch and return all blocks
            });

            app.MapGet("/blocks/{id}", async (int id) => {
                // Code to fetch and return a specific block by id
            });

            app.MapPost("/blocks", async (Block block) => {
                // Code to add a new block
            });

            app.MapPut("/blocks/{id}", async (int id, Block block) => {
                // Code to update a specific block
            });

            app.MapDelete("/blocks/{id}", async (int id) => {
                // Code to delete a specific block
            });
    }

}