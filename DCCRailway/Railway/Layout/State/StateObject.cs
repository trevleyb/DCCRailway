using DCCRailway.Common.Types;

namespace DCCRailway.Railway.Layout.State;

[Serializable]
public class StateObject(string id) {
    public string Id { get; set; } = id;

    public Dictionary<StateType, object> Data                            { get; set; } = new();
    public void                          Add<T>(StateType name, T value) => Data.TryAdd(name, value!);
    public T?                            Get<T>(StateType name)          => (T)Data[name] ?? default(T);
    public object? this[StateType key] => Data[key] ?? null;
}