using DCCRailway.Application.WiThrottle.Messages;
using DCCRailway.Common.Helpers;
using DCCRailway.Common.Types;
using DCCRailway.Layout.Configuration;
using DCCRailway.Station.Controllers;

namespace DCCRailway.Application.WiThrottle;

public class WiThrottleConnection {

    private           int                     _heartbeatSeconds = 15;
    private readonly  WiThrottleMsgQueue      _serverMessages   = [];
    private readonly  WiThrottleConnections   _listReference;
    private readonly  WiThrottleAssignedLocos _assignedLocos = new();
    internal readonly WiThrottlePreferences   Preferences;
    internal readonly IRailwayConfig          RailwayConfig;
    internal readonly IController             ActiveController;

    /// <summary>
    ///     A connection AttributeInfo Entry stores information about a particular entry in the throttle
    ///     roster and allows us to ensure we are tracking the connections and can
    ///     send "STOP" messages if we need to if we do not hear from the throttle.
    /// </summary>
    public WiThrottleConnection(ulong connectionID, WiThrottlePreferences preferences, WiThrottleConnections connections, IRailwayConfig railwayConfig, IController commandStation, string throttleName = "", string hardwareID = "") {
        ConnectionID = connectionID;
        Preferences = preferences;
        RailwayConfig = railwayConfig;
        ActiveController = commandStation;
        ThrottleName = throttleName;
        HardwareID = hardwareID;
        HeartbeatSeconds = Preferences.HeartbeatSeconds;
        HeartbeatState = HeartbeatStateEnum.Off;
        LastHeartbeat = DateTime.Now;
        _listReference = connections;
    }

    public ulong ConnectionID { get; set; }
    public string ThrottleName { get; set; }
    public string HardwareID { get; set; }

    public int HeartbeatSeconds {
        get => _heartbeatSeconds;
        set => _heartbeatSeconds = value <= 0 ? 0 : value >= 60 ? 60 : value;
    }

    public DateTime LastHeartbeat { get; set; }
    public HeartbeatStateEnum HeartbeatState { get; set; }

    /// <summary>
    ///     Returns TRUE if we have recieved a HeartBeat command within the HeartBeat timeout
    ///     duration. Controller expected the 'LastHeartbeat' to be updated by a Heartbeat command
    ///     every x seconds or it will issue a E-STOP command on the current Loco attached to
    ///     this controller.
    /// </summary>
    public bool IsHeartbeatOk => HeartbeatState == HeartbeatStateEnum.Off || ((DateTime.Now - LastHeartbeat).TotalSeconds < HeartbeatSeconds);

    public void UpdateHeartbeat() => LastHeartbeat = DateTime.Now;

    public bool IsLocoAssigned(DCCAddress address) => _assignedLocos.IsAssigned(address);
    public bool Release(DCCAddress address) => _assignedLocos.Release(address);
    public bool Assign(char group, DCCAddress address) => _assignedLocos.Assign(group, address);

    public IThrottleMsg? NextMsg => _serverMessages.HasMessages ? _serverMessages.Dequeue() : null;
    public void QueueMsg(IThrottleMsg message) => _serverMessages.Add(message);
    public bool HasMsg => _serverMessages.HasMessages;

    public void RemoveDuplicateID(string hardwareID) {
        for (var i = _listReference.Count - 1; i >= 0; i--) {
            if (_listReference[i].HardwareID.Equals(hardwareID) &&
                _listReference[i].ConnectionID != ConnectionID) {
                _listReference.RemoveAt(i);
            }
        }
    }

    public bool HasDuplicateID(string hardwareID) {
        return _listReference.Any(x => x.HardwareID.Equals(hardwareID) && x.ConnectionID != ConnectionID);
    }

    /// <summary>
    /// Close this connection. If there are any "InUse" locos, then ensure that are stopped
    /// and released and then close the connection and remove this entry from the list of active
    /// collections
    /// </summary>
    public void Close() {
        Logger.Log.Information("Closing the '{0}' connection.", ConnectionID);
        if (_assignedLocos.Count > 0) {
            foreach (var address in _assignedLocos.AssignedLocos) {
                var layoutCmd = new WitThrottleLayoutCmd(ActiveController, address);
                layoutCmd.Release();
                layoutCmd.Stop();
            }
        }
        _assignedLocos.Clear();
    }
}

public enum HeartbeatStateEnum {
    On,
    Off
}