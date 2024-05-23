using System.Net;
using System.Net.Sockets;
using DCCRailway.Common.Types;
using DCCRailway.Controller.Controllers;
using DCCRailway.Layout;
using DCCRailway.Layout.Configuration;
using DCCRailway.WiThrottle.Messages;

namespace DCCRailway.WiThrottle;

public class Connection {
    private readonly  Connections      _listReference;
    private readonly  MessageQueue     _serverMessages = [];
    internal readonly ICommandStation  CommandStation;
    internal readonly WiThrottlePrefs  Prefs;
    internal readonly IRailwaySettings RailwaySettings;
    private           int              _heartbeatSeconds = 15;

    /// <summary>
    ///     A connection AttributeInfo Entry stores information about a particular entry in the throttle
    ///     roster and allows us to ensure we are tracking the connections and can
    ///     send "STOP" messages if we need to if we do not hear from the throttle.
    /// </summary>
    public Connection(TcpClient client, IRailwaySettings railwaySettings, Connections connections,
        ICommandStation commandStation) {
        Client           = client;
        Prefs            = railwaySettings.Settings.WiThrottle;
        RailwaySettings  = railwaySettings;
        HeartbeatSeconds = Prefs.HeartbeatSeconds;
        HeartbeatState   = HeartbeatStateEnum.Off;
        LastHeartbeat    = DateTime.MinValue;
        CommandStation   = commandStation;
        _listReference   = connections;
    }

    public TcpClient Client       { get; set; }
    public string    ThrottleName { get; set; }  = "";
    public string    HardwareID   { get; set; }  = "";
    public Guid      ConnectionID { get; init; } = Guid.NewGuid();

    public IPAddress? ConnectionAddress =>
        Client.Client.RemoteEndPoint is IPEndPoint endpoint ? endpoint.Address : new IPAddress(0);

    public ulong ConnectionHandle => (ulong)(Client?.Client?.Handle ?? 0);

    public int HeartbeatSeconds {
        get => _heartbeatSeconds;
        init => _heartbeatSeconds = value <= 0 ? 0 : value >= 60 ? 60 : value;
    }

    public bool               IsActive => !string.IsNullOrEmpty(HardwareID) && (Client?.Client?.Connected ?? false);
    public DateTime           LastHeartbeat { get; set; }
    public HeartbeatStateEnum HeartbeatState { get; set; }

    /// <summary>
    ///     Returns TRUE if we have recieved a HeartBeat command within the HeartBeat timeout
    ///     duration. CommandStation expected the 'LastHeartbeat' to be updated by a Heartbeat command
    ///     every x seconds or it will issue a E-STOP command on the current Loco attached to
    ///     this commandStation.
    /// </summary>
    public bool IsHeartbeatOk => HeartbeatState == HeartbeatStateEnum.Off ||
                                 (DateTime.Now - LastHeartbeat).TotalSeconds < HeartbeatSeconds;

    public IThrottleMsg? NextMsg => _serverMessages.HasMessages ? _serverMessages.Dequeue() : null;
    public bool          HasMsg  => _serverMessages.HasMessages;

    public void UpdateHeartbeat() => LastHeartbeat = DateTime.Now;

    // Assignment Helpers
    // -------------------------------------------------------------------------------------
    public bool IsAddressInUse(DCCAddress address) => _listReference.Assignments.IsAssigned(address);
    public bool Acquire(char group, DCCAddress address) => _listReference.Assignments.Acquire(group, address, this);
    public Connection? Release(DCCAddress address) => _listReference.Assignments.Release(address);

    public List<DCCAddress> ReleaseAllInGroup(char dataGroup) =>
        _listReference.Assignments.ReleaseAllInGroup(dataGroup, this);

    // Queue Helpers
    // -------------------------------------------------------------------------------------
    public void QueueMsg(IThrottleMsg message) => _serverMessages.Add(message);

    public void QueueMsg(IThrottleMsg[] messages) {
        foreach (var msg in messages) QueueMsg(msg);
    }

    /// <summary>
    /// Used particularly for changes in Turnout, Power and Routes, send the status update
    /// message to all connected devices.
    /// </summary>
    /// <param name="message"></param>
    public void QueueMsgToAll(IThrottleMsg message) {
        foreach (var connection in _listReference.ActiveConnections) connection.QueueMsg(message);
    }

    public void QueueMsgToAll(IThrottleMsg[] messages) {
        foreach (var connection in _listReference.ActiveConnections) {
            foreach (var msg in messages) connection.QueueMsg(msg);
        }
    }

    // Find Helpers
    // -------------------------------------------------------------------------------------
    public Connection? GetByHardwareID(string hardwareID) =>
        _listReference.GetByHardwareID(hardwareID, ConnectionHandle);

    public void RemoveDuplicateID(string hardwareID) => _listReference.RemoveDuplicateID(hardwareID, ConnectionHandle);
    public bool HasDuplicateID(string hardwareID)    => _listReference.HasDuplicateID(hardwareID, ConnectionHandle);

    /// <summary>
    ///     Close this connection. If there are any "InUse" locos, then ensure that are stopped
    ///     and released and then close the connection and remove this entry from the list of active
    ///     collections
    /// </summary>
    public void Close() {
        if (_listReference.Assignments.Count > 0) {
            foreach (var address in _listReference.Assignments.AssignedAddresses) {
                // TODO: var layoutCmd = new LayoutCmdHelper(CommandStationManager.CommandStation!, address);
                // layoutCmd.Release();
                // layoutCmd.Stop();
            }
        }

        // Turn off the Heartbeat so we don't check this connection.
        // -----------------------------------------------------------------------------
        HeartbeatState = HeartbeatStateEnum.Off;
        Client.Dispose();

        // if there was no hardwareID (we didn't connect to a throttle) then
        // we can safely remove this connection. However, if there is a hardwareID then
        // we want to retain the data as we might use it if this throttle reconnects.
        // -----------------------------------------------------------------------------
        if (string.IsNullOrEmpty(HardwareID)) _listReference.Remove(this);
    }

    public override string ToString() => $"{ThrottleName} @{ConnectionAddress}/{ConnectionHandle}|";
}

public enum HeartbeatStateEnum {
    On,
    Off
}