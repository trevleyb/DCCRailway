using DCCRailway.Layout.Layout.Collection;
using DCCRailway.Layout.Layout.Entities;

namespace DCCRailway.Railway.EndPoints;

public static class LocomotiveAPI {
    public static void Configure(WebApplication app, ILayoutRepository<Locomotive> entities) {

        app.MapGet("/layout/locomotives/{id}", async (string id) => {
            var locomotive = await entities.GetByIDAsync(id);
            return locomotive == null ? Results.NotFound() : Results.Ok(locomotive);
        });

        app.MapGet("/layout/locomotives", async () => await Task.FromResult(Results.Ok(entities.GetAllAsync())));
        app.MapPost("/layout/locomotives", async (Locomotive locomotive) => Results.Ok(await entities.AddAsync(locomotive)));
        app.MapPut("/layout/locomotives/{id}", async(string id, Locomotive locomotive) => Results.Ok(await entities.UpdateAsync(locomotive)));
        app.MapDelete("/layout/locomotives/{id}", async (string id) => Results.Ok(await entities.DeleteAsync(id)));
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