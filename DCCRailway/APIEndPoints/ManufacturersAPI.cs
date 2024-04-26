using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.System;
using Microsoft.AspNetCore.Mvc;

namespace DCCRailway.APIEndPoints;

public static class ManufacturersAPI {
    public static void Configure(WebApplication app, IRailwayConfig config) {

        //app.MapGet("/manufacturers", async () => Results.Ok(await Task.FromResult(config.SystemEntities.Manufacturers)));
        app.MapGet("/manufacturers/{id}", async (byte id) => {
            var manufacturer = config.Manufacturers[id];
            return (manufacturer?.Id == 00) ? Results.NotFound("The specific manufacturer identified is not valid.") : Results.Ok(manufacturer);
        });

        app.MapGet("/manufacturers", async ([FromQuery] string name="") => {
            IEnumerable<Manufacturer>? manufacturers;
            if (!string.IsNullOrWhiteSpace(name)) {
                manufacturers = config.Manufacturers.FindAll(m => m.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase));
            } else {
                manufacturers = config.Manufacturers.Values;
            }
            return Results.Ok(Task.FromResult(manufacturers));
        });
   }
}