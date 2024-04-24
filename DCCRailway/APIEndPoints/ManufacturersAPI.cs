using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.System;
using Microsoft.AspNetCore.Mvc;

namespace DCCRailway.APIEndPoints;

public static class ManufacturersAPI {
    public static void Configure(WebApplication app, IRailwayConfig config) {

        //app.MapGet("/manufacturers", async () => Results.Ok(await Task.FromResult(config.SystemEntities.Manufacturers)));
        app.MapGet("/manufacturers/{id}", async (int id) => {
            var manufacturer = await config.ManufacturerRepository.Find(m => m.Id == id);
            return (manufacturer is null ||  manufacturer.Id == 00) ? Results.NotFound("The specific manufacturer identified is not valid.") : Results.Ok(manufacturer);
        });

        app.MapGet("/manufacturers", async ([FromQuery] string name="") => {
            IEnumerable<Manufacturer> manufacturers;
            if (!string.IsNullOrWhiteSpace(name)) {
                manufacturers = await config.ManufacturerRepository.GetAllAsync(m => m.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase));
            } else {
                manufacturers = await config.ManufacturerRepository.GetAllAsync();
            }
            return Results.Ok(Task.FromResult(manufacturers));
        });
   }
}