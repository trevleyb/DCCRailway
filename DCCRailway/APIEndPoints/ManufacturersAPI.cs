using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.Layout;
using DCCRailway.Layout.Configuration.Entities.System;
using Microsoft.AspNetCore.Mvc;

namespace DCCRailway.Service.APIEndPoints;

public static class ManufacturersAPI {
    public static void Configure(WebApplication app, IRailwayConfig config) {

        //app.MapGet("/manufacturers", async () => Results.Ok(await Task.FromResult(config.SystemEntities.Manufacturers)));
        app.MapGet("/manufacturers/{id}", async (int id) => {
            var manufacturer = config.ManufacturerRepository.Find(m => m.Id == id);
            return manufacturer.Id == 00 ? Results.NotFound("The specific manufacturer identified is not valid.") : Results.Ok(manufacturer);
        });

        app.MapGet("/manufacturers", async ([FromQuery] string name) => {
            Manufacturer? manufacturer = null;
            if (!string.IsNullOrWhiteSpace(name)) {
                manufacturer = await config.ManufacturerRepository.Find(m => m.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase));
            }
            return manufacturer == null ? Results.NotFound("The specific manufacturer identified is not valid") : Results.Ok(manufacturer);
        });
   }
}