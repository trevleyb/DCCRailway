using DCCRailway.Common.Entities.Base;
using DCCRailway.Common.Entities.Collection;

namespace DCCRailway.Helpers;

public static class ApiHelper {
    public static void MapEntity<TCollection, TEntity>(WebApplication app, string endPointName) where TEntity : ILayoutEntity where TCollection : ILayoutRepository<TEntity> {
        // Register services
        // Make sure your DataContext/Repository is registered so it's injectable

        app.MapGet(GetEndPointName(endPointName), (TCollection entities) => Results.Ok((TCollection?)entities));

        app.MapGet(GetEndPointName(endPointName, "id"), (TCollection entities, string id) => {
            // Your code for fetching a specific item here.
            var entity = entities.Find(x => x.Id == id);
            if (entity is null) return Results.NotFound($"{endPointName} with ID {id} not found");

            return Results.Ok(entity);
        });

        app.MapPost(GetEndPointName(endPointName), async (TCollection entities, TEntity entity) => {
            // Your code for creating a new accessory using injected repository
            entities.Add(entity);
        });

        app.MapPut(GetEndPointName(endPointName, "id"), async (TCollection entities, string id, TEntity updatedEntity) => {
            // Your code for updating an existing accessory using injected repository
            entities.Update(updatedEntity);
        });

        app.MapDelete(GetEndPointName(endPointName, "id"), async (TCollection entities, string id) => {
            // Your code for deleting an accessory by id using injected repository
            entities.Delete(id);
        });
    }

    private static string GetEndPointName(string endPointname, string? qualifier = null) {
        var baseName                                   = $"/{endPointname}";
        if (!string.IsNullOrEmpty(qualifier)) baseName += "/{" + qualifier + "}";
        return baseName;
    }
}