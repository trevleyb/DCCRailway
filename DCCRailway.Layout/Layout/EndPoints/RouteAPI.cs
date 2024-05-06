using DCCRailway.Layout.Layout.Collection;
using Route = DCCRailway.Layout.Layout.Entities.Route;

namespace DCCRailway.Layout.Layout.EndPoints;

public static class RouteAPI {
    public static void Configure(WebApplication app, ILayoutRepository<Route> entities) {

        app.MapGet("/routes/{id}", async (string id) => {
            var route = await entities.GetByIDAsync(id);
            return route == null ? Results.NotFound() : Results.Ok(route);
        });

        app.MapGet("/routes", async () => await Task.FromResult(Results.Ok(entities.GetAllAsync())));
        app.MapPost("/routes", async (Route route) => Results.Ok(await entities.AddAsync(route)));
        app.MapPut("/routes/{id}", async (string id, Route route) => Results.Ok(await entities.UpdateAsync(route)));
        app.MapDelete("/routes/{id}", async (string id) => Results.Ok(await entities.DeleteAsync(id)));

    }

}