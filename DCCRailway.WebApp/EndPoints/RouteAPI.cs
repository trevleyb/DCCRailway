using DCCRailway.Layout.Collection;
using Route = DCCRailway.Layout.Entities.Route;

namespace DCCRailway.WebApp.EndPoints;

public static class RouteAPI {
    public static void Configure(WebApplication app, ILayoutRepository<Route> entities) {

        app.MapGet("/layout/routes/{id}", async (string id) => {
            var route = await entities.GetByIDAsync(id);
            return route == null ? Results.NotFound() : Results.Ok(route);
        });

        app.MapGet("/layout/routes", async () => await Task.FromResult(Results.Ok(entities.GetAllAsync())));
        app.MapPost("/layout/routes", async (Route route) => Results.Ok(await entities.AddAsync(route)));
        app.MapPut("/layout/routes/{id}", async (string id, Route route) => Results.Ok(await entities.UpdateAsync(route)));
        app.MapDelete("/layout/routes/{id}", async (string id) => Results.Ok(await entities.DeleteAsync(id)));

    }

}