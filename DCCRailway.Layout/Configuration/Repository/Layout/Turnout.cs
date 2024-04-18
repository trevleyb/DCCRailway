using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.Layout.Configuration.Repository.Layout;

public class TurnoutRepository(Dictionary<Guid, Turnout> collection) : Repository<Turnout>(collection) {
    public override async Task<IEnumerable<Turnout>> GetAllAsync() {
        return await Task.FromResult(entities.Values.ToList());
    }

    public override async Task<Turnout?> GetAsync(Guid id) {
        return await Task.FromResult(entities[id] ?? null);
    }

    public override async Task<Turnout?> AddAsync(Turnout entity) {
        if (entity.Id == Guid.Empty) entity.Id = Guid.NewGuid();
        if (!entities.Keys.Contains(entity.Id)) {
            entities.Add(entity.Id, entity);
        }
        return await Task.FromResult(entities[entity.Id] ?? null);
    }

    public override async Task<Turnout?> UpdateAsync(Turnout entity) {
        if (entities.Keys.Contains(entity.Id)) {
            entities[entity.Id] = entity;
        }
        return await Task.FromResult(entities[entity.Id] ?? null);
    }

    public override async Task<Task> DeleteAsync(Guid id) {
        if (entities.Keys.Contains(id)) {
            entities.Remove(id);
        }
        return await Task.FromResult(Task.CompletedTask);
    }

    public override async Task<Turnout?> Find(string name) {
        return await Task.FromResult(entities.Values.FirstOrDefault(x => x.Name.Equals(name,StringComparison.InvariantCultureIgnoreCase)) ?? null);
    }

}