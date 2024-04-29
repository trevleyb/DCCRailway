using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.System;
using DCCRailway.Layout.Configuration.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DCCRailway.APIEndPoints;

public static class ManufacturersAPI {
    public static void Configure(WebApplication app, IRailwayConfig config) {

        //app.MapGet("/manufacturers", async () => Results.Ok(await Task.FromResult(config.SystemEntities.Manufacturers)));
        app.MapGet("/manufacturers/{id}", (byte id) => {
            var manufacturer = config.Manufacturers[id];
            return (manufacturer?.Id == 00) ? Results.NotFound("The specific manufacturer identified is not valid.") : Results.Ok(manufacturer);
        });

        app.MapGet("/manufacturers", ([FromQuery] string name="") => {
            var manufacturers = !string.IsNullOrWhiteSpace(name) ? config.Manufacturers.FindAll(m => m.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase)) : config.Manufacturers.Values;
            return Results.Ok(Task.FromResult(manufacturers));
        });
   }
}