using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.APIEndPoints;

public static class LocomotiveAPI {
    public static void Configure(WebApplication app, IRailwayConfig config) {

        app.MapGet("/locomotives", async () => Results.Ok(await config.LocomotiveRepository.GetAllAsync()));

        app.MapGet("/locomotives/{id}", async (Guid id) => {
            var locomotive = await config.LocomotiveRepository.GetByIDAsync(id);
            return locomotive == null ? Results.NotFound() : Results.Ok(locomotive);
        });

        app.MapPost("/locomotives", async (Locomotive locomotive) => {
            if (locomotive.Id == Guid.Empty) locomotive.Id = Guid.NewGuid();
            return Results.Ok(await config.LocomotiveRepository.AddAsync(locomotive));
        });

        app.MapPut("/locomotives/{id}", async(Guid id, Locomotive locomotive) => Results.Ok(await config.LocomotiveRepository.UpdateAsync(locomotive)));

        app.MapDelete("/locomotives/{id}", async (Guid id) => Results.Ok(await config.LocomotiveRepository.DeleteAsync(id)));


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