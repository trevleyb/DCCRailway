using System.Collections.Concurrent;

namespace DCCRailway.Railway.Layout.State;

/// <summary>
/// A state manager simply tracks the state of given objects. An object must have
/// an identifier and an object that represents its state and the state can be
/// returned as part of a call to the state manager.
/// </summary>
public class StateManager {

    private readonly ConcurrentDictionary<string, StateObject> _states = new();

    public List<StateObject> GetAll() {
        return _states.Values.ToList();
    }

    public StateObject SetState(string id, string key, object value) {
        if (!_states.ContainsKey(id)) _states.TryAdd(id, new StateObject(id));
        var stateObject = _states[id];
        if (!stateObject.Data.ContainsKey(key)) stateObject.Data.TryAdd(key, value);
        stateObject.Data[key] = value;
        return stateObject;
    }

    public StateObject SetState(StateObject state) {
        if (!_states.ContainsKey(state.Id)) {
            if (!_states.TryAdd(state.Id, state)) {
                throw new Exception("Error creating a State Object");
            }
        }
        // Should always get one of these because we would have just added it.
        // --------------------------------------------------------------------
        _states[state.Id] = state;
        return state;
    }

    public StateObject? GetState(string id) {
        if (!_states.TryGetValue(id, out var idStates)) return null;
        return _states[id];
    }

    public T? GetState<T>(string id, string key) {
        if (!_states.TryGetValue(id, out var idStates)) return default(T);
        if (!idStates.Data.TryGetValue(key, out var keyValue)) return default(T);
        return (T)idStates.Data[key];
    }

    public T GetState<T>(string id, string key, T ifNotExist) {
        if (!_states.TryGetValue(id, out var idStates)) return ifNotExist;
        if (!idStates.Data.TryGetValue(key, out var keyValue)) return ifNotExist;
        return (T)idStates.Data[key];
    }

    public object? GetState(string id, string key) {
        if (!_states.TryGetValue(id, out var idStates)) return null;
        if (!idStates.Data.TryGetValue(key, out var keyValue)) return null;
        return idStates.Data[key];
    }

    public object GetState(string id, string key, object ifNotExist) {
        if (!_states.TryGetValue(id, out var idStates)) return ifNotExist;
        if (!idStates.Data.TryGetValue(key, out var keyValue)) return ifNotExist;
        return idStates.Data[key];
    }

    public void DeleteState(string id) {
        _states.TryRemove(id, out var states);
    }

    public void DeleteState(string id, string key) {
        if (_states.TryGetValue(id, out var idStates)) {
            if (idStates.Data.ContainsKey(key)) idStates.Data.Remove(key);
        }
    }
}