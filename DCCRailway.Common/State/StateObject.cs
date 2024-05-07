using System.Net.Sockets;

namespace DCCRailway.Common.State;

[Serializable]
public class StateObject(string id) {
    public string Id { get; set; } = id;
    public Dictionary<string, object> Data { get; set; } = new();
    public void Add<T>(string name, T value) => Data.TryAdd(name, value!);
    public T? Get<T>(string name) =>  (T)Data[name] ?? default(T);
}