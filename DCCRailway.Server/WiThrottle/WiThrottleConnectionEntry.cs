using System;
using DCCRailway.Server.WiThrottle;
using DCCRailway.Server.WiThrottle.Commands;

namespace DCCRailway.Server;

public class WiThrottleConnectionEntry {
    private int _heartbeatSeconds = 15;

    internal WiThrottleConnectionList listReference;

    /// <summary>
    ///     A connection Info Entry stores information about a particular entry in the throttle
    ///     roster and allows us to ensure we are tracking the connections and can
    ///     send "STOP" messages if we need to if we do not hear from the throttle.
    /// </summary>
    public WiThrottleConnectionEntry(ulong connectionID, WiThrottleConnectionList connectionList, string throttleName = "", string hardwareID = "") {
        ConnectionID     = connectionID;
        ThrottleName     = throttleName;
        HardwareID       = hardwareID;
        HeartbeatSeconds = 15;
        HeartbeatState   = HeartbeatStateEnum.Off;
        LastHeartbeat    = DateTime.Now;
        LastCommand      = null;
        listReference    = connectionList;
    }

    public ulong  ConnectionID { get; set; }
    public string ThrottleName { get; set; }
    public string HardwareID   { get; set; }

    public int HeartbeatSeconds {
        get => _heartbeatSeconds;
        set {
            if (value <= 0) {
                _heartbeatSeconds = 0;
            } else if (value >= 99) {
                _heartbeatSeconds = 99;
            } else {
                _heartbeatSeconds = value;
            }
        }
    }

    public DateTime           LastHeartbeat  { get; set; }
    public HeartbeatStateEnum HeartbeatState { get; set; }
    public IThrottleCmd?      LastCommand    { get; set; }

    /// <summary>
    ///     Returns TRUE if we have recieved a HeartBeat command within the HeartBeat timeout
    ///     duration. System expected the 'LastHeartbeat' to be updated by a Heartbeat command
    ///     every x seconds or it will issue a E-STOP command on the current Loco attached to
    ///     this controller.
    /// </summary>
    public bool IsHeartbeatOK {
        get {
            if (HeartbeatState == HeartbeatStateEnum.Off) return true;
            if ((DateTime.Now - LastHeartbeat).TotalSeconds < HeartbeatSeconds) return true;

            return false;
        }
    }

    public void UpdateHeartbeat() => LastHeartbeat = DateTime.Now;
}

public enum HeartbeatStateEnum {
    On,
    Off
}