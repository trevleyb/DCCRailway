using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.APIEndPoints;

public static class LocomotiveAPI {
    public static void Configure(WebApplication app, IRailwayConfig config) {

        app.MapGet("/locomotives", async () => {
            // Code to fetch and return all locomotives
        });

        app.MapGet("/locomotives/{id}", async (int id) => {
            // Code to fetch and return a specific locomotive by id
        });

        app.MapPost("/locomotives", async (Locomotive locomotive) => {
            // Code to add a new locomotive
        });

        app.MapPut("/locomotives/{id}", async (int id, Locomotive locomotive) => {
            // Code to update a specific locomotive
        });

        app.MapDelete("/locomotives/{id}", async (int id) => {
            // Code to delete a specific locomotive
        });
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