using DCCRailway.Common.Entities.Base;
using DCCRailway.Common.Entities.Collection;

namespace DCCRailway.Helpers;

public static class ApiHelper {
    public static void MapEntity<TCollection, TEntity>(WebApplication app, string endPointName) where TEntity : ILayoutEntity where TCollection : ILayoutRepository<TEntity> {
        app.MapGet(GetEndPointName(endPointName), (TCollection entities) => Results.Ok(entities.GetAll()));

        app.MapGet(GetEndPointName(endPointName, "id"), (TCollection entities, string id) => {
            var entity = entities.GetByID(id);
            if (entity is null) return Results.NotFound($"{endPointName} with ID {id} not found");
            return Results.Ok(entity);
        });

        app.MapPost(GetEndPointName(endPointName), async (TCollection entities, TEntity entity) => { entities.Update(entity); });

        app.MapPut(GetEndPointName(endPointName, "id"), async (TCollection entities, string id, TEntity updatedEntity) => { entities.Update(updatedEntity); });

        app.MapDelete(GetEndPointName(endPointName, "id"), async (TCollection entities, string id) => { entities.Delete(id); });
    }

    private static string GetEndPointName(string endPointname, string? qualifier = null) {
        var baseName                                   = $"/{endPointname}";
        if (!string.IsNullOrEmpty(qualifier)) baseName += "/{" + qualifier + "}";
        return baseName;
    }
}