using DCCRailway.Layout.Entities;
using DCCRailway.Layout.Entities.Collection;

namespace DCCRailway.WebApp.EndPoints;

public static class LocomotiveAPI {
    public static void Configure(WebApplication app, ILayoutRepository<Locomotive> entities) {
        app.MapGet("/layout/locomotives/{id}", async (string id) => {
            var locomotive = entities.GetByID(id);
            return await Task.FromResult(locomotive == null ? Results.NotFound() : Results.Ok(locomotive));
        });

        app.MapGet("/layout/locomotives", async () => await Task.FromResult(entities));
        app.MapPost("/layout/locomotives",
                    async (Locomotive locomotive) => await Task.FromResult(Results.Ok(entities.Add(locomotive))));
        app.MapPut("/layout/locomotives/{id}",
                   async (string id, Locomotive locomotive) =>
                       await Task.FromResult(Results.Ok(entities.Update(locomotive))));
        app.MapDelete("/layout/locomotives/{id}",
                      async (string id) => await Task.FromResult(Results.Ok(entities.Delete(id))));
    }
}

/* Use this for search of Locomotives

app.MapGet("/manufacturers", async ([FromQuery] Dictionary<string, string> queryParameters) => {
       var manufacturers = config.SystemEntities.Manufacturers.AsQueryable();

       foreach (var param in queryParameters)
       {
           var propertyInfo = typeof(Manufacturer).GetProperty(param.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
           if (propertyInfo != null)
           {
               manufacturers = manufacturers.Where(m => propertyInfo.GetValue(m, null)?.ToString().Contains(param.Value, StringComparison.InvariantCultureIgnoreCase) == true);
           }
       }

       var results = await manufacturers.ToListAsync();
       return results.Count == 0 ? Results.NotFound("No manufacturers found matching the provided criteria.") : Results.Ok(results);
   });

*/