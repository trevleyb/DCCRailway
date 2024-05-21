using System.Net;
using System.Net.Sockets;
using DCCRailway.Common.Types;
using DCCRailway.Controller.Controllers;
using DCCRailway.Layout;
using DCCRailway.Layout.Configuration;
using DCCRailway.WiThrottle.Messages;

namespace DCCRailway.WiThrottle;

public class WiThrottleConnection {
    private readonly  WiThrottleAssignedLocos _assignedLocos = new();
    private readonly  WiThrottleConnections   _listReference;
    private readonly  WiThrottleMsgQueue      _serverMessages = [];
    internal readonly ICommandStation          CommandStation;
    internal readonly WiThrottlePrefs          Prefs;
    internal readonly IRailwaySettings         RailwaySettings;
    private           int                     _heartbeatSeconds = 15;

    /// <summary>
    ///     A connection AttributeInfo Entry stores information about a particular entry in the throttle
    ///     roster and allows us to ensure we are tracking the connections and can
    ///     send "STOP" messages if we need to if we do not hear from the throttle.
    /// </summary>
    public WiThrottleConnection(TcpClient client, IRailwaySettings railwaySettings, WiThrottleConnections connections, ICommandStation commandStation) {
        Client                = client;
        Prefs                 = railwaySettings.Settings.WiThrottle;
        RailwaySettings       = railwaySettings;
        HeartbeatSeconds      = Prefs.HeartbeatSeconds;
        HeartbeatState        = HeartbeatStateEnum.Off;
        LastHeartbeat         = DateTime.Now;
        CommandStation        = commandStation;
        _listReference        = connections;
    }

    public TcpClient Client       { get; set; }
    public string    ThrottleName { get; set; }
    public string    HardwareID   { get; set; }
    public Guid      ConnectionID { get; init; } = Guid.NewGuid();

    public IPAddress? ConnectionAddress => Client.Client.RemoteEndPoint is IPEndPoint endpoint ? endpoint.Address : new IPAddress(0);
    public ulong      ConnectionHandle  => (ulong)(Client?.Client?.Handle ?? 0);

    public int HeartbeatSeconds {
        get => _heartbeatSeconds;
        init => _heartbeatSeconds = value <= 0 ? 0 : value >= 60 ? 60 : value;
    }

    public DateTime           LastHeartbeat  { get; set; }
    public HeartbeatStateEnum HeartbeatState { get; set; }

    /// <summary>
    ///     Returns TRUE if we have recieved a HeartBeat command within the HeartBeat timeout
    ///     duration. CommandStation expected the 'LastHeartbeat' to be updated by a Heartbeat command
    ///     every x seconds or it will issue a E-STOP command on the current Loco attached to
    ///     this commandStation.
    /// </summary>
    public bool IsHeartbeatOk => HeartbeatState == HeartbeatStateEnum.Off || (DateTime.Now - LastHeartbeat).TotalSeconds < HeartbeatSeconds;

    public IThrottleMsg? NextMsg => _serverMessages.HasMessages ? _serverMessages.Dequeue() : null;
    public bool          HasMsg  => _serverMessages.HasMessages;

    public void UpdateHeartbeat() => LastHeartbeat = DateTime.Now;

    public bool IsLocoAssigned(DCCAddress address)     => _assignedLocos.IsAssigned(address);
    public bool Release(DCCAddress address)            => _assignedLocos.Release(address);
    public bool Assign(char group, DCCAddress address) => _assignedLocos.Assign(group, address);
    public void QueueMsg(IThrottleMsg message)         => _serverMessages.Add(message);

    public WiThrottleConnection? GetByHardwareID(string hardwareID) {
        return _listReference.GetByHardwareID(hardwareID,ConnectionHandle);
    }

    public void RemoveDuplicateID(string hardwareID) {
        _listReference.RemoveDuplicateID(hardwareID,ConnectionHandle);
    }

    public bool HasDuplicateID(string hardwareID) {
        return _listReference.HasDuplicateID(hardwareID,ConnectionHandle);
    }

    /// <summary>
    ///     Close this connection. If there are any "InUse" locos, then ensure that are stopped
    ///     and released and then close the connection and remove this entry from the list of active
    ///     collections
    /// </summary>
    public void Close() {
        if (_assignedLocos.Count > 0) {
            foreach (var address in _assignedLocos.AssignedLocos) {
                // TODO: var layoutCmd = new WitThrottleLayoutCmd(CommandStationManager.CommandStation!, address);
                // layoutCmd.Release();
                // layoutCmd.Stop();
            }
        }

        // Turn off the Heartbeat so we don't check this connection.
        // -----------------------------------------------------------------------------
        HeartbeatState = HeartbeatStateEnum.Off;
        _assignedLocos.Clear();
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