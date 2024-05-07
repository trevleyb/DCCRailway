using System.Collections.Concurrent;
using DCCRailway.Common.Helpers;
using DCCRailway.Common.State;

namespace DCCRailway.Layout.StateManager;

/// <summary>
/// A state manager simply tracks the state of given objects. An object must have
/// an identifier and an object that represents its state and the state can be
/// returned as part of a call to the state manager.
/// </summary>
public class LayoutStateManager {

    private readonly ConcurrentDictionary<string, StateObject> _states = new();

    public List<StateObject> GetAll() {
        return _states.Values.ToList();
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

    public void DeleteState(string id) {
        _states.TryRemove(id, out var states);
    }

}