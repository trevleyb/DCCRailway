using DCCRailway.Common.Types;

namespace DCCRailway.StateManager;

public interface IStateTracker {
    List<StateObject> GetAll();
    StateObject       SetState(DCCAddress address, StateType key, object value);
    StateObject       SetState(string id, StateType key, object value);
    StateObject       SetState<T>(DCCAddress address, StateType key, T value);
    StateObject       SetState<T>(string id, StateType key, T value);
    StateObject       SetState(StateObject state);
    StateObject?      GetState(DCCAddress address);
    StateObject?      GetState(string id);
    T?                GetState<T>(DCCAddress address, StateType key);
    T?                GetState<T>(string id, StateType key);
    T                 GetState<T>(DCCAddress address, StateType key, T ifNotExist);
    T                 GetState<T>(string id, StateType key, T ifNotExist);
    object?           GetState(DCCAddress address, StateType key);
    object?           GetState(string id, StateType key);
    object            GetState(DCCAddress address, StateType key, object ifNotExist);
    object            GetState(string id, StateType key, object ifNotExist);
    void              CopyState(DCCAddress address, StateType firstKey, StateType secondKey, object ifNotExist);
    void              CopyState(string id, StateType firstKey, StateType secondKey, object ifNotExist);
    void              DeleteState(DCCAddress address);
    void              DeleteState(string id);
    void              DeleteState(DCCAddress address, StateType key);
    void              DeleteState(string id, StateType key);
}