using DCCRailway.Common.Utilities.Results;
using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.Service.APIEndPoints;

public static class AccessoryAPI {
    public static void Configure(WebApplication app, IRailwayConfig config) {

        app.MapGet("/accessorys", async () => {
            // Code to fetch and return all accessories
            Result.Success();
        });

        app.MapGet("/accessorys/{id}", async (int id) => {
            // Code to fetch and return a specific accessory by id
        });

        app.MapPost("/accessorys", async (Accessory accessory) => {
            // Code to add a new accessory
        });

        app.MapPut("/accessorys/{id}", async (int id, Accessory accessory) => {
            // Code to update a specific accessory
        });

        app.MapDelete("/accessorys/{id}", async (int id) => {
            // Code to delete a specific accessory
        });
    }

}