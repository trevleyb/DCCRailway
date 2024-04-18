using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.Layout.Configuration.Repository.Layout;

public class SignalRepository(Dictionary<Guid, Signal> collection) : Repository<Signal>(collection) {
    public override async Task<IEnumerable<Signal>> GetAllAsync() {
        return await Task.FromResult(entities.Values.ToList());
    }

    public override async Task<Signal?> GetAsync(Guid id) {
        return await Task.FromResult(entities[id] ?? null);
    }

    public override async Task<Signal?> AddAsync(Signal entity) {
        if (entity.Id == Guid.Empty) entity.Id = Guid.NewGuid();
        if (!entities.Keys.Contains(entity.Id)) {
            entities.Add(entity.Id, entity);
        }
        return await Task.FromResult(entities[entity.Id] ?? null);
    }

    public override async Task<Signal?> UpdateAsync(Signal entity) {
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

    public override async Task<Signal?> Find(string name) {
        return await Task.FromResult(entities.Values.FirstOrDefault(x => x.Name.Equals(name,StringComparison.InvariantCultureIgnoreCase)) ?? null);
    }

}