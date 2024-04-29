using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.Layout;
using Route = DCCRailway.Layout.Configuration.Entities.Layout.Route;

namespace DCCRailway.APIEndPoints;

public static class RouteAPI {
    public static void Configure(WebApplication app, IRailwayConfig config) {

        app.MapGet("/routes", async () => Results.Ok(await config.Routes.GetAllAsync()));

        app.MapGet("/routes/{id}", async (string id) => {
            var route = await config.Routes.GetByIDAsync(id);
            return route == null ? Results.NotFound() : Results.Ok(route);
        });

        app.MapPost("/routes", async (Route route) => Results.Ok(await config.Routes.AddAsync(route)));

        app.MapPut("/routes/{id}", async (string id, Route route) => Results.Ok(await config.Routes.UpdateAsync(route)));

        app.MapDelete("/routes/{id}", async (string id) => Results.Ok(await config.Routes.DeleteAsync(id)));

    }

}