using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.Layout;
using Microsoft.AspNetCore.Mvc;

namespace DCCRailway.Service.APIEndPoints;

public static class ManufacturersAPI {
    public static void Configure(WebApplication app, IRailwayConfig config) {

        //app.MapGet("/manufacturers", async () => Results.Ok(await Task.FromResult(config.SystemEntities.Manufacturers)));
        app.MapGet("/manufacturers/{id}", async (int id) => {
            var manufacturer = config.SystemEntities.Manufacturers.FirstOrDefault(m => m.Identifier == id);
            return manufacturer == null ? Results.NotFound("The specific manufacturer identified is not valid.") : Results.Ok(manufacturer);
        });

        app.MapGet("/manufacturers", async ([FromQuery] string name) => {
            if (!string.IsNullOrWhiteSpace(name)) {
                var manufacturer = config.SystemEntities.Manufacturers.FirstOrDefault(m => m.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase));
                return manufacturer == null ? Results.NotFound("The specific manufacturer identified is not valid.") : Results.Ok(manufacturer);
            } else {
                return Results.Ok(await Task.FromResult(config.SystemEntities.Manufacturers));
            }
        });
   }
}