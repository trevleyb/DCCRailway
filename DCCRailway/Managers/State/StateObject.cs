namespace DCCRailway.Managers.State;

[Serializable]
public class StateObject(string id) {
    public string Id { get; set; } = id;

    public Dictionary<StateType, object> Data { get; set; } = new();
    public object? this[StateType key] => Data[key] ?? null;
    public void Add<T>(StateType name, T value) => Data.TryAdd(name, value!);
    public T?   Get<T>(StateType name)          => (T)Data[name] ?? default(T);
}