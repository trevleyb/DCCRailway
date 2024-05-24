using DCCRailway.Layout.Entities.Collection;
using Route = DCCRailway.Layout.Entities.Route;

namespace DCCRailway.WebApp.EndPoints;

public static class RouteAPI {
    public static void Configure(WebApplication app, ILayoutRepository<Route> entities) {
        app.MapGet("/layout/routes/{id}", async (string id) => {
            var route = entities.GetByID(id);
            return await Task.FromResult(route == null ? Results.NotFound() : Results.Ok(route));
        });

        app.MapGet("/layout/routes", async () => await Task.FromResult(Results.Ok(entities.GetAll())));
        app.MapPost("/layout/routes", async (Route route) => await Task.FromResult(Results.Ok(entities.Add(route))));
        app.MapPut("/layout/routes/{id}", async (string id, Route route) => await Task.FromResult(Results.Ok(entities.Update(route))));
        app.MapDelete("/layout/routes/{id}", async (string id) => await Task.FromResult(Results.Ok(entities.Delete(id))));
    }
}