using DCCRailway.System.Adapters.Events;

namespace DCCRailway.System.Adapters;

public abstract class Adapter {
    public event EventHandler<DataRecvArgs> DataReceived;
    public event EventHandler<DataSentArgs> DataSent;
    public event EventHandler<DataErrorArgs>    ErrorOccurred;

    #region Events Delegates
    /// <summary>
    ///     When data is recieved by the Adapater, raise this event with the
    ///     underlying data so other systems can listen for this information
    /// </summary>
    /// <param name="e">Args containing the data recieved</param>
    protected virtual void OnDataRecieved(DataRecvArgs e) => DataReceived?.Invoke(this, e);

    /// <summary>
    ///     When we send data, raise this event so other systems can listen
    ///     and react to the information that was sent. Ie: we might send a
    ///     switch command but want other systems to know about this change.
    /// </summary>
    /// <param name="e">Args with what data was sent</param>
    protected virtual void OnDataSent(DataSentArgs e) => DataSent?.Invoke(this, e);

    /// <summary>
    ///     If an error occurs, then raise an event to notify listeners
    ///     that there was an error and what the error was.
    /// </summary>
    /// <param name="e">Args containing information on the error</param>
    protected virtual void OnErrorOccurred(DataErrorArgs e) => ErrorOccurred?.Invoke(this, e);
    #endregion
}