namespace DCCRailway.Railway.Configuration.Entities;

[Serializable]
public class Tasks : List<Task> {

    public Task? this[string name] => Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)) ?? null;

    public void Delete(string name) {
        ArgumentNullException.ThrowIfNull(name);
        var task = Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        if (task != null) Remove(task);
    }

    public void Add(string name, string taskType, TimeSpan frequency) {
        ArgumentNullException.ThrowIfNull(taskType);
        ArgumentNullException.ThrowIfNull(name);

        if (Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)) != null) throw new ArgumentException($"Parameter '{name}' already exists");
        Add(new Task(name,taskType,frequency));
    }
}