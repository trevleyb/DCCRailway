using System;
using DCCRailway.Core.Systems.Adapters.Events;

namespace DCCRailway.Core.Systems.Adapters; 

public abstract class BaseAdapter {
    public abstract string Description { get; }

    public event EventHandler<StateChangedArgs> ConnectionStatusChanged;
    public event EventHandler<DataRecvArgs> DataReceived;
    public event EventHandler<DataSentArgs> DataSent;
    public event EventHandler<ErrorArgs> ErrorOccurred;

    #region Event Delegates

    /// <summary>
    ///     When the state of an Adapters Connection changes, then raise an event
    ///     so that the systems using the Adapter know that a state change has
    ///     occurred and can react to it
    /// </summary>
    /// <param name="e">The event arguments and details of the change</param>
    protected virtual void OnConnectionChangedState(StateChangedArgs e) {
        ConnectionStatusChanged?.Invoke(this, e);
    }

    /// <summary>
    ///     When data is recieved by the Adapater, raise this event with the
    ///     underlying data so other systems can listen for this information
    /// </summary>
    /// <param name="e">Args containing the data recieved</param>
    protected virtual void OnDataRecieved(DataRecvArgs e) {
        DataReceived?.Invoke(this, e);
    }

    /// <summary>
    ///     When we send data, raise this event so other systems can listen
    ///     and react to the information that was sent. Ie: we might send a
    ///     switch command but want other systems to know about this change.
    /// </summary>
    /// <param name="e">Args with what data was sent</param>
    protected virtual void OnDataSent(DataSentArgs e) {
        DataSent?.Invoke(this, e);
    }

    /// <summary>
    ///     If an error occurs, then raise an event to notify listeners
    ///     that there was an error and what the error was.
    /// </summary>
    /// <param name="e">Args containing information on the error</param>
    protected virtual void OnErrorOccurred(ErrorArgs e) {
        ErrorOccurred?.Invoke(this, e);
    }

    #endregion
}