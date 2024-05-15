using System.Collections.Concurrent;
using DCCRailway.Common.Types;

namespace DCCRailway.Railway.Layout.State;

/// <summary>
/// A state manager simply tracks the state of given objects. An object must have
/// an identifier and an object that represents its state and the state can be
/// returned as part of a call to the state manager.
/// </summary>
public class StateManager : IStateManager {

    private readonly ConcurrentDictionary<string, StateObject> _states = new();

    public List<StateObject> GetAll() {
        return _states.Values.ToList();
    }

    public StateObject SetState(DCCAddress address, StateType key, object value) => SetState(address.ToString(), key, value);
    public StateObject SetState(string id, StateType key, object value) {
        if (!_states.ContainsKey(id)) _states.TryAdd(id, new StateObject(id));
        var stateObject = _states[id];
        if (!stateObject.Data.ContainsKey(key)) stateObject.Data.TryAdd(key, value);
        stateObject.Data[key] = value;
        return stateObject;
    }

    public StateObject SetState<T>(DCCAddress address, StateType key, T value) => SetState<T>(address.ToString(), key, value);
    public StateObject SetState<T>(string id, StateType key, T value) {
        if (!_states.ContainsKey(id)) _states.TryAdd(id, new StateObject(id));
        var stateObject = _states[id];
        if (value is not null) {
            if (!stateObject.Data.ContainsKey(key)) stateObject.Data.TryAdd(key, value);
            stateObject.Data[key] = value;
        }
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

    public StateObject? GetState(DCCAddress address) => GetState(address.ToString());
    public StateObject? GetState(string id) {
        if (!_states.TryGetValue(id, out var idStates)) return null;
        return _states[id];
    }

    public T? GetState<T>(DCCAddress address, StateType key) => GetState<T>(address.ToString(), key);
    public T? GetState<T>(string id, StateType key) {
        if (!_states.TryGetValue(id, out var idStates)) return default(T);
        if (!idStates.Data.TryGetValue(key, out var keyValue)) return default(T);
        return (T)idStates.Data[key];
    }

    public T GetState<T>(DCCAddress address, StateType key, T ifNotExist) => GetState<T>(address.ToString(), key, ifNotExist);
    public T GetState<T>(string id, StateType key, T ifNotExist) {
        if (!_states.TryGetValue(id, out var idStates)) return ifNotExist;
        if (!idStates.Data.TryGetValue(key, out var keyValue)) return ifNotExist;
        return (T)idStates.Data[key];
    }

    public object? GetState(DCCAddress address, StateType key) => GetState(address.ToString(), key);
    public object? GetState(string id, StateType key) {
        if (!_states.TryGetValue(id, out var idStates)) return null;
        if (!idStates.Data.TryGetValue(key, out var keyValue)) return null;
        return idStates.Data[key];
    }

    public object GetState(DCCAddress address, StateType key, object ifNotExist) => GetState(address.ToString(), key, ifNotExist);
    public object GetState(string id, StateType key, object ifNotExist) {
        if (!_states.TryGetValue(id, out var idStates)) return ifNotExist;
        if (!idStates.Data.TryGetValue(key, out var keyValue)) return ifNotExist;
        return idStates.Data[key];
    }

    public void CopyState(DCCAddress address, StateType firstKey, StateType secondKey, object ifNotExist) => CopyState(address.ToString(), firstKey, secondKey, ifNotExist);
    public void CopyState(string id, StateType firstKey, StateType secondKey, object ifNotExist) {
        var firstState = GetState(id, firstKey);
        if (firstState is not null) {
            SetState(id, secondKey, firstState);
        }
        else {
            SetState(id, secondKey, ifNotExist);
        }
    }

    public void DeleteState(DCCAddress address) => DeleteState(address.ToString());
    public void DeleteState(string id) {
        _states.TryRemove(id, out var states);
    }

    public void DeleteState(DCCAddress address, StateType key) => DeleteState(address.ToString(), key);
    public void DeleteState(string id, StateType key) {
        if (_states.TryGetValue(id, out var idStates)) {
            if (idStates.Data.ContainsKey(key)) idStates.Data.Remove(key);
        }
    }
}